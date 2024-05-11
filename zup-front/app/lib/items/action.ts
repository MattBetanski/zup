'use server';
import { Tire } from "@phosphor-icons/react";
import { InvalidTokenError } from "jwt-decode";
import { cookies } from "next/headers";
import { z } from "zod";

const ItemSchema = z.object({
    itemId: z.string(),
    name: z.string({invalid_type_error: "Please enter an item name"})
        .min(1, {message: "Please enter an item name"}),
    projectId: z.string(),
    ownerId: z.string(),
    parentId: z.string(),
    description: z.string({invalid_type_error: "Please enter an item description"}),
    deadline: z.date({invalid_type_error: "Please enter an item deadline"}),
    type: z.enum(['Epic', 'Feature', 'Requirement', 'Task', 'Bug'], {invalid_type_error: "Please select an item type"}),
    state: z.enum(['Open', 'InProgress', 'Closed'], {invalid_type_error: "Please select an item state"})
});

const CreateItem = ItemSchema.omit({ itemId: true, ownerId: true, state: true});

export type CreateItemState = {
    errors?: {
        name?: string[],
        description?: string[],
        deadline?: string[],
        type?: string[],
        parentId?: string[]
        projectId?: string[]
    },
    message?: string | null;
};

export async function createItem(prevState: CreateItemState, formData: FormData) {
    try {
        const validatedFields = CreateItem.safeParse({
            name: formData.get("name"),
            description: formData.get("description"),
            deadline: formData.get("deadline"),
            type: formData.get("type"),
            parentId: formData.get("parentId"),
            projectId: formData.get("projectId")
        });
        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to create item"
            };
        }
        let token = cookies().get("token")?.value ?? '';
        const {name, description, deadline, type, parentId, projectId} = validatedFields.data;

    } catch (err) {

    }
}