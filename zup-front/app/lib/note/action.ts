'use server';

import { revalidatePath } from "next/cache";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { z } from "zod";
import { Note } from "../definitions";

const NoteSchema = z.object({
    noteId: z.coerce.number(),
    ownerId: z.coerce.number(),
    title: z.string({invalid_type_error: "Please enter a note title"})
        .min(1, {message: "Please enter a note title"}),
    content: z.string({invalid_type_error: "Please enter a note body"})
        .min(1, {message: "Please enter a note body"}),
    departmentId: z.coerce.number()
});

const CreateNote = NoteSchema.omit({noteId: true, ownerId: true});

export type CreateNoteState = {
    errors?: {
        title?: string[],
        content?: string[],
        departmentId?: string[]
    },
    message?: string | null;
}


export async function createNote(prevState: CreateNoteState, formData: FormData) {
    try {
        const validatedFields = CreateNote.safeParse({
            title: formData.get("title"),
            content: formData.get("content"),
            departmentId: formData.get("departmentId")
        });

        if(!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to create item"

            }
        };

        let token = cookies().get("token")?.value ?? '';
        let {title, content, departmentId} = validatedFields.data;
        var dept_id = departmentId;
        const params = new URLSearchParams();
        const response = await fetch(`http://localhost:5001/note`, {
            method: "POST",
            body: JSON.stringify({
                "title": title,
                "content": content,
                "departmentId": departmentId
            }),
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 200) {

        } else {
            const body = await response.text();
            console.log("Error message = ", body);
            return {message: "Failed to create note"}
        }
    } catch (err) {
        console.error(err);
        throw Error("failed to create note")
    }
    revalidatePath(`/dashboard/department/${dept_id}/notes`);
    redirect(`/dashboard/department/${dept_id}/notes`);
}


const VoteNote = NoteSchema.omit({ownerId: true, title: true, content: true});

export type VoteNoteState = {
    errors?: {
        noteId?: string[];
    },
    message?: string | null;
}

export async function upvoteNote(prevState: VoteNoteState, formData: FormData) {
    try {
        const validatedFields = VoteNote.safeParse({
            noteId: formData.get("noteId"),
            departmentId: formData.get("departmentId"),
        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to create item"
            }
        };

        let token = cookies().get("token")?.value ?? '';
        let {noteId, departmentId} = validatedFields.data;
        var dept_id = departmentId;
        const params = new URLSearchParams();
        params.set("note_id", noteId.toString());
        params.set("rating", "true");
        const response = await fetch(`http://localhost:5001/note/rate?${params.toString()}`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.status == 204) {

        } else {
            return {message: "Failed to create note"}
        }
    } catch (err) {
        console.error(err);
        throw Error("failed to upvote note");
    }
    revalidatePath(`/dashboard/department/${dept_id}/note`);
    //redirect(`/dashboard/department/${dept_id}/note`);
}

export async function downvoteNote(prevState: VoteNoteState, formData: FormData) {
    try {
        const validatedFields = VoteNote.safeParse({
            noteId: formData.get("noteId"),
            departmentId: formData.get("departmentId"),
        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to create item"
            }
        };

        let token = cookies().get("token")?.value ?? '';
        let {noteId, departmentId} = validatedFields.data;
        var dept_id = departmentId;
        const params = new URLSearchParams();
        params.set("note_id", noteId.toString());
        params.set("rating", "false");
        const response = await fetch(`http://localhost:5001/note/rate?${params.toString()}`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.status == 204) {

        } else {
            return {message: "Failed to create note"}
        }
    } catch (err) {
        console.error(err);
        throw Error("failed to upvote note");
    }
    revalidatePath(`/dashboard/department/${dept_id}/note`);
    //redirect(`/dashboard/department/${dept_id}/note`);
}

export async function getNotesForDepartment(departmentId: number) {
    try {

        let token = cookies().get("token")?.value ?? '';
        const params = new URLSearchParams();
        params.set("department_id", departmentId.toString());
        const response = await fetch(`http://localhost:5001/note/all?${params.toString()}`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 200) {
            const body: Note[] = await response.json();
            return body;
        }
        console.log(response);
    } catch (err) {
        console.error(err);
        throw Error("failed getting notes for department")
    }
    return [];
}