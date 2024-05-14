'use server';

import { revalidatePath } from "next/cache";
import { cookies } from "next/headers";
import { UserInvitations } from "../definitions";

export async function acceptInvite(formData: FormData) {
    try {
        let departmentId = formData.get("departmentId")?.toString() ?? '';
        let status = 'Accepted';
        let token = cookies().get("token")?.value ?? '';
        const params = new URLSearchParams();
        params.set("department_id", departmentId);
        params.set("response", status);
        const response = await fetch(`http://localhost:5001/user/invitations?${params.toString()}`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.status == 200) {
            revalidatePath("/dashboard")
            revalidatePath("#")
        } else {
            console.log(response);
        }
    } catch (err) {
        console.error(err);
        return null;
    }
}

export async function declineInvite(formData: FormData) {
    try {
        let departmentId = formData.get("departmentId")?.toString() ?? '';
        let status = 'Declined';
        let token = cookies().get("token")?.value ?? '';
        const params = new URLSearchParams();
        params.set("department_id", departmentId);
        params.set("response", status);
        const response = await fetch(`http://localhost:5001/user/invitations?${params.toString()}`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (response.status == 200) {

            revalidatePath("#")
            // Do nothing
        } else {
            console.log(response);
        }
    } catch (err) {
        console.error(err);
        return null;
    }
}

export async function getInvitesForUser() {
    try {
        let token = cookies().get("token")?.value ?? '';
        const params = new URLSearchParams();
        const response = await fetch(`http://localhost:5001/user/invitations`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        console.log(response);
        if (response.status == 200) {
            const items: UserInvitations[] = await response.json();
            return items;
        } else {
            return [];
        }
    } catch (err) {
        console.error(err);
        return [];
    }
}