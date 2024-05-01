'use server';

import { revalidatePath } from 'next/cache';
import { z } from 'zod';
import { redirect } from 'next/navigation';
import { AuthError } from 'next-auth';
import { signIn } from '@/auth';

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
    try {
        await signIn('credentials', formData);
    } catch (error) {
        if (error instanceof AuthError) {
            switch (error.type) {
                case 'CredentialsSignin':
                    return 'Invalid credentials.';
                default:
                    return 'Something went wrong.';
            }
        }
        throw error;
    }
}