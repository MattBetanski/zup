'use server';

import { cookies, headers } from "next/headers";
import { z } from "zod";
import { WikiListing, WikiPage } from "../definitions";
import { redirect } from "next/navigation";

const WikiSchema = z.object({
    departmentId: z.coerce.number(),
    title: z.string({invalid_type_error: "Please enter a page name"})
        .min(1, {message: "You must enter a page name"}),
    content: z.string({invalid_type_error: "Enter markdown contents"}),
    wikiPageId: z.coerce.number({invalid_type_error: "Please enter a wiki page id"}),
    createdDate: z.coerce.date({invalid_type_error: "Please enter a creation date"})
});

export type CreateWikiState = {
    errors?: {
        title?: string[];
        content?: string[];
        departmentId?: string[];
    },
    message?: string | null;
}

const CreateWiki = WikiSchema.omit({wikiPageId: true, createdDate: true});

export async function createWikiPage(prevState: CreateWikiState, formData: FormData) {
    try {
        const validatedFields = CreateWiki.safeParse({
            title: formData.get("title"),
            content: formData.get("content"),
            departmentId: formData.get("departmentId")
        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to create Department"
            };
        }

        let token = cookies().get("token")?.value ?? '';
        const {title, content, departmentId} = validatedFields.data;
        console.log(content);
        var dept_id = departmentId;
        const params = new URLSearchParams();
        params.set("department_id", departmentId.toString());
        const response = await fetch(`http://localhost:5001/wiki?${params.toString()}`, {
            method: "POST",
            body: JSON.stringify({
                "title": title,
                "content": content
            }),
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        });
        
        if (response.status == 204) {
            console.log("created page");
        } else {
            console.log(response);
            const body = await response.text();
            return {message: body};
        }
    } catch (err) {
        console.error(err);
        throw new Error("Failed to create wiki page")
    }
    redirect(`/dashboard/department/${dept_id}/wiki`)
}

export type EditWikiState = {
    errors?: {
        wikiPageId?: string[];
        title?: string[];
        content?: string[];
    },
    message?: string | null;
}

const EditWiki = WikiSchema.omit({createdDate: true, departmentId: true});

export async function updateWikiPage(prevState: EditWikiState, formData: FormData) {
    try {
        const validatedFields = EditWiki.safeParse({
            title: formData.get("title"),
            content: formData.get("content"),
            departmentId: formData.get("departmentId"),
            wikiPageId: formData.get("wikiPageId")
        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to create Department"
            };
        }

        let token = cookies().get("token")?.value ?? '';
        const {title, content, wikiPageId} = validatedFields.data;

        const params = new URLSearchParams();
        params.set("wiki_id", wikiPageId.toString());
        const response = await fetch(`http://localhost:5001/wiki?${params.toString()}`, {
            method: "POST",
            body: JSON.stringify({
                "title": title,
                "content": content
            }),
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        });
        
        if (response.status == 200) {
            console.log("updated page");
        } else {
            console.log(response);
            const body = await response.text();
            return {message: body};
        }
    } catch (err) {
        console.error(err);
        throw new Error("Failed to create wiki page")
    }
}

export async function getWikiPagesForDepartment(departmentId: number) {
    try {
        const token = cookies().get("token")?.value ?? '';
        const params = new URLSearchParams();
        params.set("department_id", departmentId.toString());
        const response = await fetch(`http://localhost:5001/wiki/all?${params.toString()}`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        if (response.status == 200) {
            const wikiItems: WikiListing[] = await response.json();
            return wikiItems;
        } else {
            console.log(response);
        }
    } catch (err) {
        console.error(err);
        throw Error("Failed to fetch wiki pages for department")
    }
    return [];
}

export async function getWikiPageById(wikiPageId: number) {
    try {
        const token = cookies().get("token")?.value ?? '';
        const params = new URLSearchParams();
        params.set("wiki_id", wikiPageId.toString());
        const response = await fetch(`http://localhost:5001/wiki?${params.toString()}`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 200) {
            const wikiItems: WikiPage = await response.json();
            return wikiItems;
        } else {
            //console.log(response);
        }
    } catch (err) {
        console.error(err);
        throw Error("Failed to fetch wiki pages for department")
    }
    return null;
}
