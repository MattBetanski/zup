'use server';

import RoleAssignmentTable from "@/app/ui/project/roles";
import { notFound, useParams } from "next/navigation";

//TODO: Page for assigning users to roles for project
export default async function Page({params}: {params: {id: number}}) {
    console.log(params.id);
    //TODO: get roles for project
    //const roles = ...
    return (
        <main>
            <RoleAssignmentTable />
        </main>
    );
}