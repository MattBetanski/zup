'use server';
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { z } from "zod";
import { Project } from "../definitions";

const ProjectSchema = z.object({
    projectId: z.number(),
    departmentId: z.coerce.number(),
    name: z.string({invalid_type_error: "Please enter a project name"})
        .min(1, {message: "Please enter a project name"}),
    description: z.string({invalid_type_error: "Please enter a project description"})
        .min(1, {message: "Please enter a project description"}),
    creationDate: z.date()
});

const CreateProject = ProjectSchema.omit({ projectId: true, creationDate: true});

export type CreateProjectState = {
    errors?: {
        departmentId?: string[],
        name?: string[],
        description?: string[]
    },
    message?: string | null;
};

export async function createProject(prevState: CreateProjectState, formData: FormData) {
    try {
        const validatedFields = CreateProject.safeParse({
            name: formData.get("name"),
            description: formData.get("description"),
            departmentId: formData.get("departmentId")
        });

        if (!validatedFields.success) {
            console.log(validatedFields.error);
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to create project"
            };
        }
        let token = cookies().get("token")?.value ?? '';
        const {name, description, departmentId} = validatedFields.data;
        var dept_id = departmentId;
        const response = await fetch(`http://localhost:5001/project`, {
            method: "POST",
            body: JSON.stringify({
                "name": name,
                "description": description,
                "departmentId": departmentId
            }),
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 204) {
            // Do nothing
        } else if (response.status == 400) {
            const body = await response.text();
            return {message: body};
        } else if (response.status == 500) {
            const body = await response.text();
            return {message: body};
        } else {
            return {message: "Internal server error occured. Try again later."};
        }
    } catch (err) {
        console.error(err);
        return {message: "Internal server error occured. Try again later."};
    }
    redirect(`/dashboard/department/${dept_id}`)
}

export async function getProjectsForDepartment(departmentId: number) {
    try {
        const token = cookies().get("token")?.value ?? '';
        const params = new URLSearchParams();
        params.set("department_id", departmentId.toString());

        const response = await fetch(`http://localhost:5001/department/projects?${params.toString()}`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.status == 200) {
            const body: Project = await response.json();
            return body;
        }
    } catch (err) {
        console.error(err);
    }
}