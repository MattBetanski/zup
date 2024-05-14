'use server';
import { Tire } from "@phosphor-icons/react";
import { InvalidTokenError } from "jwt-decode";
import { cookies } from "next/headers";
import { z } from "zod";
import { Item, ItemType } from "../definitions";
import { json } from "stream/consumers";
import { revalidatePath } from "next/cache";
import { redirect } from "next/navigation";

const ItemSchema = z.object({
    itemId: z.string(),
    name: z.string({invalid_type_error: "Please enter an item name"})
        .min(1, {message: "Please enter an item name"}),
    projectId: z.coerce.number(),
    ownerId: z.string(),
    parentId: z.coerce.number().nullable(),
    description: z.string({invalid_type_error: "Please enter an item description"}),
    type: z.enum(['Epic', 'Feature', 'Requirement', 'Task', 'Bug'], {invalid_type_error: "Please select an item type"}),
    state: z.enum(['Open', 'InProgress', 'Completed', "Blocked"], {invalid_type_error: "Please select an item state"})
});

const CreateItem = ItemSchema.omit({ itemId: true, ownerId: true});

export type CreateItemState = {
    errors?: {
        name?: string[],
        description?: string[],
        type?: string[],
        parentId?: string[],
        projectId?: string[],
        state?: string[]
    },
    message?: string | null;
};

export async function createItem(prevState: CreateItemState, formData: FormData) {
    console.log(formData);
    try {
        const validatedFields = CreateItem.safeParse({
            name: formData.get("name"),
            description: formData.get("description"),
            type: formData.get("type"),
            parentId: formData.get("parentId"),
            projectId: formData.get("projectId"),
            state: formData.get("state")
        });
        console.log("got here");
        if (!validatedFields.success) {
            const errors = validatedFields.error.flatten().fieldErrors;
            console.log(errors);
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to create item"
            };
        }
        let token = cookies().get("token")?.value ?? '';
        let {name, description, type, parentId, projectId, state} = validatedFields.data;
        if (parentId == -1) parentId = null;
        var project_id = projectId;
        console.log(parentId);
        const response = await fetch('http://localhost:5001/item', {
            method: "POST",
            body: JSON.stringify({
                "name": name,
                "parentId": parentId,
                "description": description,
                "type": type,
                "state": state,
                "projectId": projectId
            }),
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 204) {
            // Do nothing
        } else if(response.status == 400) {
            const body = await response.text();
            return {message: body};
        } else if(response.status == 401) {
            const body = await response.text();
            return {message: body};
        } else if(response.status == 403) {
            const body = await response.text();
            return {message: body};
        } else if(response.status == 500) {
            const body = await response.text();
            return {message: body};
        } else {
            return {message: "Internal server error occured. Try again later."}
        }
        revalidatePath(`/dashboard/project/${project_id}/items`);
    } catch (err) {
        console.error(err);
        return {message: "Internal sever error occured. Try again later."}
    }
    redirect(`/dashboard/project/${project_id}/items`)
}

export async function getItemsForType(type: string, projectId: number) {
    console.log(type);
    try {
        const token = cookies().get("token")?.value;
        const params = new URLSearchParams();
        params.set("type", type);
        params.set("project_id", projectId.toString());
        console.log(params.toString());
        const response = await fetch(`http://localhost:5001/item/filter?${params.toString()}`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 200) {
            const body: Item[] = await response.json();
            return body;
        }
    } catch (err) {
        console.error(err);
        return null;
    }
    return null;
}

export async function getItemsForProject(projectId: number) {
    try {
        let token = cookies().get("token")?.value ?? '';
        const params = new URLSearchParams();
        params.set("project_id", projectId.toString());
        const response = await fetch(`http://localhost:5001/item/filter?${params.toString()}`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        if (response.status == 200) {
            const body: Item[] = await response.json();
            return body;
        } else {
            console.log(response);
            const body = await response.text();
            console.log(body);
            return [];
        }
    } catch (err) {
        console.error(err);
        return [];
    }
}