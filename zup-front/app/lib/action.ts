'use server';

import { revalidatePath } from 'next/cache';
import { z } from 'zod';
import { redirect } from 'next/navigation';
import { cookies } from 'next/headers';
import { jwtDecode } from 'jwt-decode';
import { Department } from './definitions';

const FormSchema = z.object({
    id: z.string(),
    customerId: z.string({
        invalid_type_error: 'Please select a customer.'
    }),
    amount: z.coerce
        .number()
        .gt(0, { message: "Please enter an amount greater than $0."}),
    status: z.enum(['pending', 'paid'], {
        invalid_type_error: "Please select an invoice status."
    }),
    date: z.string(),
});

export type State=  {
    errors?: {
        customerId?: string[];
        amount?: string[];
        status?: string[]
    };
    message?: string | null;
};

const CreateInvoice = FormSchema.omit({ id: true, date: true});
const UpdateInvoice = FormSchema.omit({ id: true, date: true});

export async function createInvoice(prevState: State, formData: FormData) {
    try {
        const validatedFields = CreateInvoice.safeParse({
            customerId: formData.get('customerId'),
            amount: formData.get('amount'),
            status: formData.get('status'),

        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to Create Invoice.",
            };
        }
        const { customerId, amount, status } = validatedFields.data;
        const amountInCents = amount * 100;
        const date = new Date().toISOString().split('T')[0];

        const response = await fetch('http://localhost:9090/invoices', {
            method: "POST",
            body: JSON.stringify({
                "customerId": customerId,
                "amount": amountInCents,
                "status": status,
                "date": date
            }),
            headers: {
                "Content-Type": "application/json"
            }
        });
        const text = await response.text();
        console.log(text);
        revalidatePath('/dashboard/invoices');
    } catch (err) {
        console.log(err);
        throw new Error('Failed to create invoice');
    }
    redirect('/dashboard/invoices');
}

export async function updateInvoice(id: string, formData: FormData) {
    try {
        const { customerId, amount, status }= UpdateInvoice.parse({
            customerId: formData.get('customerId'),
            amount: formData.get('amount'),
            status: formData.get('status'),
        });

        const amountInCents = amount * 100;

        const response = await fetch(`http://localhost:9090/invoices/${id}`, {
            method: "PUT",
            body: JSON.stringify({
                "customerId": customerId,
                "amount": amountInCents,
                "status": status,
            }),
            headers: {
                "Content-Type": "application/json"
            }
        });
        const text = await response.text();
        console.log(text);
    } catch (err) {
        console.log(err);
        throw new Error('Failed to update invoice');
    }
    revalidatePath('/dashboard/invoices');
    redirect('/dashboard/invoices');
}

export async function deleteInvoice(id: string) {
    try {
        const response = await fetch(`http://localhost:9090/invoices/${id}`, {
            method: "DELETE"
        });

        const text = await response.text();
        console.log(text);
    } catch (err) {
        console.log(err);
        throw new Error("Failed to delete invoice");
    }
    revalidatePath('/dashboard/invoices');
    redirect('/dashboard/invoices');
}

export async function authenticate(
    prevState: string | undefined,
    formData: FormData,
) {
    console.log(formData);
    return "test";
    try {
        //await signIn('credentials', formData);
    } catch (error) {
        /*
        if (error instanceof AuthError) {
            switch (error.type) {
                case 'CredentialsSignin':
                    return 'Invalid credentials.';
                default:
                    return 'Something went wrong.';
            }
        }
        */
        throw error;
    }
}

export async function createAccount(prevState: string | undefined, formData: FormData) {
    try {
        
    } catch (err) {
        return err;
    }
}

export async function redirectDashboard() {
    redirect("/dashboard");
}

export async function signIn(prevState: string | undefined, formData: FormData) {
  try {
  let response = await fetch('http://localhost:5001/User/Login', {
      method: "POST",
      body: JSON.stringify({
        "Username": formData.get("username")
      }),
      headers: {
        "Content-Type": "application/json"
      }
    });
    if (response.status != 200) {
      return "Invalid Username or Password supplied";
    }
    let body = await response.json();
    let token = body.token;
    cookies().set("token", token);
    redirect("/dashboard");
  } catch (error) {
    
    throw error;
  }
}

export async function getToken() {
    const token = cookies().get("token")?.value;
    if (token == null) {
        return null;
    }
    let decoded = jwtDecode(token);
    return decoded;
}

export async function checkAuthenticated() {

    const token = cookies().get("token")?.value;
    if (token == null){
        redirect("/login");
    }
    let decoded = jwtDecode(token);
    console.log(decoded);
    if (decoded.exp == null) {
        redirect("/login");
    }
    if (Date.now() >= decoded.exp * 1000) {
        redirect("/login");
    }
    return decoded;
}

export async function fetchDepartmentsForUser() {
    let data: Department[] =  [
        {
            "departmentId": 0,
            "name": "string",
            "description": "string",
            "creationDate": new Date("2024-05-05T23:38:15.388Z"),
            "visibility": true
        },
        {
            "departmentId": 1,
            "name": "Test Department",
            "description": "This is department two",
            "creationDate": new Date("2024-05-05T23:38:15.388Z"),
            "visibility": true
        },
        {
            "departmentId": 2,
            "name": "Another Department",
            "description": "New Department",
            "creationDate": new Date("2024-05-05T23:38:15.388Z"),
            "visibility": true
        },
        {
            "departmentId": 3,
            "name": "My Department",
            "description": "Yet another department you have. But this one appears to have a really long description. I wonder if it cant fit properly onto your pages? I guess time will tell.",
            "creationDate": new Date("2024-05-05T23:38:15.388Z"),
            "visibility": true
        }
    ];
    return data;
}

const DepartmentSchema = z.object({
    id: z.string(),
    name: z.string({invalid_type_error: "Please enter a Department Name"})
        .min(1, {message: "You must enter a Department Name"}),
    description: z.string({invalid_type_error: "Please enter a description"})
        .min(1, {message: "You must enter a description"}),
    creationDate: z.date(),
    visibility: z.enum(['public', 'private'], {invalid_type_error: "Please select a visibility"})
});

const CreateDepartment = DepartmentSchema.omit({ id: true, creationDate: true});

export type CreateDepartmentState = {
    errors?: {
        name?: string[];
        description: string[];
        visibility: string[];
    },
    message?: string | null;
}
export async function createDepartment(prevState: CreateDepartmentState, formData: FormData) {
    try {
        const validatedFields = CreateDepartment.safeParse({
            name: formData.get("name"),
            description: formData.get("description"),
            visibility: formData.get("visibility")
        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to Create Department"
            };
        }
        let token = cookies().get("token");
        console.log(token);
        const {name, description, visibility} = validatedFields.data;
        const booleanVisibility = visibility == "public";
        const response = await fetch('http://localhost:5001/department', {
            method: "POST",
            body: JSON.stringify({
                "name": name,
                "description": description,
                "visibility": booleanVisibility
            }),
            headers: {
                "Content-Type": "application/json"
            }
        });
        revalidatePath("/dashboard");
    } catch (err) {
        console.error(err);
        throw new Error("Failed to create department");
    }
    redirect("/dashboard");
}

export async function editDepartment(prevState: CreateDepartmentState, formData: FormData) {
    try {
        const validatedFields = CreateDepartment.safeParse({
            name: formData.get("name"),
            description: formData.get("description"),
            visibility: formData.get("visibility")
        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to Create Department"
            };
        }
        let token = cookies().get("token");
        console.log(token);
        const {name, description, visibility} = validatedFields.data;
        const booleanVisibility = visibility == "public";
        const response = await fetch('http://localhost:5001/department', {
            method: "POST",
            body: JSON.stringify({
                "name": name,
                "description": description,
                "visibility": booleanVisibility
            }),
            headers: {
                "Content-Type": "application/json"
            }
        });
        revalidatePath("/dashboard");
    } catch (err) {
        console.error(err);
        throw new Error("Failed to create department");
    }
    redirect("/dashboard");
}