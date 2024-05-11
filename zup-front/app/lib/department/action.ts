'use server';
import { revalidatePath } from "next/cache";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { z } from "zod";
import { Department } from "../definitions";

const DepartmentSchema = z.object({
    id: z.number(),
    name: z.string({invalid_type_error: "Please enter a Department Name"})
        .min(1, {message: "You must enter a Department Name"}),
    description: z.string({invalid_type_error: "Please enter a description"})
        .min(1, {message: "You must enter a description"}),
    creationDate: z.date(),
    visibility: z.enum(['public', 'private'], {invalid_type_error: "Please select a visibility"})
});

const CreateDepartment = DepartmentSchema.omit({ id: true, creationDate: true});
const EditDepartment = DepartmentSchema.omit({creationDate: true});

export type EditDepartmentState = {
    errors?: {
        id?: string[]
        name?: string[];
        description: string[];
        visibility: string[];
    },
    message?: string | null;
}

export type CreateDepartmentState = {
    errors?: {
        name?: string[];
        description?: string[];
        visibility?: string[];
    },
    message?: string | null;
}

export async function deleteDepartment(id: string) {
    try {
        console.log("test")
    } catch (err) {
        console.error(err);
        throw new Error("Failed to delete department");
    }
    revalidatePath("/dashboard/department");
    redirect("/dashboard/department");
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
                message: "Missing fields. Failed to create Department"
            };
        }
        let token = cookies().get("token")?.value ?? '';
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
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 204) {
            // Do nothing
        } else if (response.status == 500) {
            const body = await response.text();
            return {message: body};
        } else if (response.status == 401) {

        } else {
            return {message: "Internal server error occured. Try again later."};
        }
        revalidatePath("/dashboard");
    } catch (err) {
        console.error(err);
        throw new Error("Failed to create department");
    }
    redirect("/dashboard");
}

export async function editDepartment(prevState: EditDepartmentState, formData: FormData) {
    try {
        const validatedFields = EditDepartment.safeParse({
            id: formData.get("id"),
            name: formData.get("name"),
            description: formData.get("description"),
            visibility: formData.get("visibility")
        });

        if (!validatedFields.success) {
            console.log(validatedFields);
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to Create Department"
            };
        }

        let token = cookies().get("token")?.value ?? '';
        const {name, description, visibility, id} = validatedFields.data;
        var departmentId = id;
        const booleanVisibility = visibility == "public";
        const response = await fetch('http://localhost:5001/department', {
            method: "POST",
            body: JSON.stringify({
                "name": name,
                "description": description,
                "visibility": booleanVisibility
            }),
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        revalidatePath(`/dashboard/department/${id}`);
    } catch (err) {
        console.error(err);
        throw new Error("Failed to create department");
    }
    redirect(`/dashboard/department/${departmentId}`);
}

export async function getDepartmentsForUser() {
    try {
        const token = cookies().get("token")?.value;
        if (token == null) {
            return;
        }
        const response = await fetch('http://localhost:5001/department/all', {
            "method": "GET",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.status == 200) {
            const items: Department[] = await response.json();
            return items;
        } else if (response.status == 401) {
            return null;
        } else if (response.status == 404) {
            return [];
        } else if (response.status == 500) {
            return null;
        } else {
            return null;
        }
    } catch (err) {
        console.error(err);
        return null;
    }
}

export async function getDepartmentById(id: number) {
    try {
        const token = cookies().get("token")?.value;
        if (token == null) {
            return;
        }
        const response = await fetch(`http://localhost:5001/department?department_id=${id.toString()}`, {
            "method": "GET",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 200) {
            const item: Department = await response.json();
            return item;
        } else if (response.status == 400) {
            console.error("Department does not exist");
            return null;
        } else if (response.status == 500) {
            console.error("Internal error occurred");
            return null;
        }
    } catch (err) {
        console.error(err);
        return null;
    }
}