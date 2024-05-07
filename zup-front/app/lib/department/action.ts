'use server';
import { revalidatePath } from "next/cache";
import { redirect } from "next/navigation";

export async function deleteDepartment(id: string) {
    try {
        console.log("test")
    } catch (err) {
        console.error(err);
        throw new Error("Failed to delete department");
    }
    revalidatePath("/dashboard/department");
    redirect("/dashboard/department");
}