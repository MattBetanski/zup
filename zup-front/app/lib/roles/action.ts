'use server';

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
            name: formData.get("name")
        })
    } catch (err) {
        console.error(err);
        return {message: "Failed to create role"};
    }
}