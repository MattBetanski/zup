import { revalidatePath } from "next/cache";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { UserInvitations } from "../definitions";

export async function acceptInvite(formData: FormData) {
    try {
        let departmentId = formData.get("departmentId")?.toString() ?? '';
        let token = cookies().get("token")?.value ?? '';
        const params = new URLSearchParams();
        params.set("department_id", departmentId);
        params.set("response", "true");
        const response = await fetch(`http://localhost:5001/department/invitations?${params.toString}`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        if (response.status == 200) {
            var success = true;
        } else {
            success = false;
        }
    } catch (err) {
        console.error(err);
        return null;
    }
    if (success) {
        revalidatePath("/dashboard");
        redirect("/dashboard");
    }
    return null;
}

export async function declineInvite(formData: FormData) {

}

export async function getInvitesForUser() {
    try {
        let token = cookies().get("token")?.value ?? '';
        const response = await fetch('http://localhost:5001/user/invitations', {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        if (response.status == 200){
            const items: UserInvitations[] = await response.json();
            return items;
        } else {
            return [];
        }
    } catch (err) {
        console.error(err);
        return null;
    }
}