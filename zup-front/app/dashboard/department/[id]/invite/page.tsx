'use server'

import { getInvitesForDepartment, InviteState, inviteToDepartment } from "@/app/lib/department/action";
import { DepartmentInviteBreadcrumb } from "@/app/ui/department/breadcrumbs";
import Invite from "@/app/ui/department/invite/invite";
import InviteTable from "@/app/ui/department/invite/table";
import { init } from "next/dist/compiled/webpack/webpack";
import { useRouter } from "next/navigation";
import { useFormState } from "react-dom";

export default async function Page({params}: {params: {id: number}}){
    console.log("Params = ", params);
    const invites = await getInvitesForDepartment(params.id);
    return (
        <main>
            <DepartmentInviteBreadcrumb departmentId={params.id}/>
            <div className="mb-10">
                <Invite departmentId={params.id} />           
            </div>
            <InviteTable invites={invites}/>
        </main>
    );
}