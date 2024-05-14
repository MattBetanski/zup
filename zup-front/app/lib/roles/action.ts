'use server';

import { revalidatePath } from "next/cache";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { z } from "zod";
import { Role } from "../definitions";

const RoleSchema = z.object({
    departmentId: z.coerce.number(),
    name: z.string({invalid_type_error: "Please enter a role name"})
        .min(1, {message: "Please enter a role name"}),
    description: z.string({invalid_type_error: "Please enter a description"}),
    itemLevel: z.enum(['NoAccess', 'Read', 'Modify'], {invalid_type_error: "Please select an item permission level"}),
    wikiLevel: z.enum(['NoAccess', 'Read', 'Modify'], {invalid_type_error: "Please select an wiki permission level"}),
    wikiDelete: z.coerce.boolean({invalid_type_error: "Please choose whether the user can delete a wiki page"}),
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
    },
    message?: string | null;
};

export async function createRole(prevState: CreateRoleState, formData: FormData) {
    try {
        console.log(formData.get("wikiDelete"));
        console.log(formData.get("departmentId"));
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
                "wikiDelete": wikiDelete,
                "departmentId": departmentId
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
export type EditRoleState = {
    errors?: {
        name?: string[];
        description?: string[];
        itemLevel?: string[];
        wikiLevel?: string[];
        wikiDelete?: string[];
        roleId?: string[],
    },
    message?: string | null;
};
export async function editRole(prevState: EditRoleState, formData: FormData) {
    try {
        console.log(formData.get("wikiDelete"));
        console.log(formData.get("departmentId"));
        const validatedFields = RoleSchema.safeParse({
            name: formData.get("name"),
            description: formData.get("description"),
            itemLevel: formData.get("itemLevel"),
            wikiLevel: formData.get("wikiLevel"),
            wikiDelete: formData.get("wikiDelete"),
            departmentId: formData.get("departmentId"),
            roleId: formData.get("roleId")
        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing fields. Failed to update role"
            };
        }
        let token = cookies().get("token")?.value ?? '';
        const {name, description, wikiLevel, itemLevel, wikiDelete, departmentId, roleId} = validatedFields.data;
        var dept_id = departmentId;
        const response = await fetch('http://localhost:5001/role', {
            method: "PUT",
            body: JSON.stringify({
                "name": name,
                "description": description,
                "wikiLevel": wikiLevel,
                "itemLevel": itemLevel,
                "wikiDelete": wikiDelete,
                "departmentId": departmentId,
                "roleId": roleId
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
        return {message: "Failed to update role"};
    }
    revalidatePath(`/dashboard/department/${dept_id}/roles`)
    redirect(`/dashboard/department/${dept_id}/roles`)
}

export async function getRolesForDepartment(departmentId: number) {
    try {
        const token = cookies().get("token")?.value;
        const params = new URLSearchParams();
        params.set("department_id", departmentId.toString());
        const response = await fetch(`http://localhost:5001/department/roles?${params.toString()}`, {
            "method": "GET",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 200) {
            const item: Role[] = await response.json();
            return item;
        } else if (response.status == 400) {
            console.error("Department does not exist");
            return [];
        } else if (response.status == 500) {
            console.error("Internal error occurred");
            return [];
        }
    } catch (err) {
        console.error(err);
        return [];
    }
    return [];
}

export async function deleteRole(roleId: number, departmentId: number) {
    try {
        console.log("attempting to delete role");
        const token = cookies().get("token")?.value;
        const params = new URLSearchParams();
        params.set("role_id", roleId.toString());
        console.log(params.toString());
        const response = await fetch(`http://localhost:5001/role${params.toString()}`, {
            "method": "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 204) {
            revalidatePath("#");
        } else {
            console.log(response);
        }
    } catch (err) {
        console.error(err);
        throw new Error("Failed to delete role");
    }
    revalidatePath(`/dashboard/department/${departmentId}/roles`)
    redirect(`/dashboard/department/${departmentId}/roles`)
}

export async function setRole(roleId: number, userId: number, projectId: number) {
    try {
        const token = cookies().get("token")?.value;
        const params = new URLSearchParams();
        params.set("project_id", projectId.toString());
        params.set("user_id", userId.toString());
        params.set("role_id", roleId.toString());
        console.log(params.toString());
        const response = await fetch(`http://localhost:5001/project/roles?${params.toString()}`, {
            "method": "PUT",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 200) {
            return null;
        } else if (response.status == 400) {
            console.error("Department does not exist");
            return null;
        } else if (response.status == 500) {
            console.error("Internal error occurred");
            return null;
        }
        console.log("got here assigning role");
    } catch (err) {
        console.error(err);
        return null;
    }
}