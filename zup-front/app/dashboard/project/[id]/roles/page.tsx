'use server';

import { getUsersForDepartment } from "@/app/lib/department/action";
import { getDepartmentByProjectId } from "@/app/lib/project/action";
import { getRolesForDepartment } from "@/app/lib/roles/action";
import RoleAssignmentTable, { RoleAssignmentBreadcrumb } from "@/app/ui/project/roles";
import { notFound, useParams } from "next/navigation";
import { Breadcrumb, Breadcrumbs, Link } from "react-aria-components";

//TODO: Page for assigning users to roles for project
export default async function Page({params}: {params: {id: number}}) {
    console.log(params.id);
    //TODO: get roles for project
    const departmentId = 1;
    const department = await getDepartmentByProjectId(params.id);
    const roles = await getRolesForDepartment(department.departmentId);
    const users = await getUsersForDepartment(department.departmentId);
    console.log(users);
    console.log(roles);
    console.log(department);
    //const roles = ...
    return (
        <main>
            <RoleAssignmentBreadcrumb projectId={params.id}/>
            <RoleAssignmentTable roles={roles} users={users} projectId={params.id}/>
        </main>
    );
}