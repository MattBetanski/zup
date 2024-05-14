'use server';

import { revalidatePath } from "next/cache";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { z } from "zod";

const RoleSchema = z.object({
    departmentId: z.coerce.number(),
    name: z.string({invalid_type_error: "Please enter a role name"})
        .min(1, {message: "Please enter a role name"}),
    description: z.string({invalid_type_error: "Please enter a description"}),
    itemLevel: z.enum(['NoAccess', 'Read', 'Modify'], {invalid_type_error: "Please select an item permission level"}),
    wikiLevel: z.enum(['NoAccess', 'Read', 'Modify'], {invalid_type_error: "Please select an wiki permission level"}),
    wikiDelete: z.boolean({invalid_type_error: "Please choose whether the user can delete a wiki page"}),
    roleId: z.coerce.number(),
});

const CreateRole = RoleSchema.omit({roleId: true});

export type CreateRoleState = {
    errors?: {
        name?: string[];
        description?: string[];
        itemLevel?: string[];
        wikiLevel?: string[];
        wikiDelete?: string[];
    }
};

export async function createRole(prevState: CreateRoleState, formData: FormData) {
    try {
        const validatedFields = CreateRole.safeParse({
            name: formData.get("name"),
            description: formData.get("description"),
            itemLevel: formData.get("itemLevel"),
            wikiLevel: formData.get("wikiLevel"),
            wikiDelete: formData.get("wikiDelete"),
            departmentId: formData.get("departmentId")
        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to create role"
            };
        }
        let token = cookies().get("token")?.value ?? '';
        const {name, description, wikiLevel, itemLevel, wikiDelete, departmentId} = validatedFields.data;
        var dept_id = departmentId;
        const response = await fetch('http://localhost:5001/role', {
            method: "POST",
            body: JSON.stringify({
                "name": name,
                "description": description,
                "wikiLevel": wikiLevel,
                "itemLevel": itemLevel,
                "wikiDelete": wikiDelete
            }),
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.status == 204) {
            // Do nothing, redirect later
        } else if (response.status == 400) {
            let body = await response.text();
            return {message: body};
        } else if (response.status == 401) {
            let body = await response.text();
            return {message: body};
        } else if (response.status == 403) {
            let body = await response.text();
            return {message: body};
        } else if (response.status == 500) {
            let body = await response.text();
            return {message: body};
        } else {
            return {message: "Internal server error occurred"};
        }
    } catch (err) {
        console.error(err);
        return {message: "Failed to create role"};
    }
    revalidatePath(`/dashboard/department/${dept_id}/roles`)
    redirect(`/dashboard/department/${dept_id}/roles`)
}