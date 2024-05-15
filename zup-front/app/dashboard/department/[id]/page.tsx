'use server';

import { fetchDepartmentbyId, fetchProjectsForDepartment, fetchWikiPagesForDepartment } from "@/app/lib/data";
import { lusitana } from "@/app/ui/fonts";
import { notFound, redirect } from "next/navigation";
import {Stack} from '@primer/react/experimental';
import { useState } from "react";
import PaginatedList from "@/app/ui/PaginatedLIst";
import { RecentWikiPages } from "@/app/ui/department/wikiPages";
import { Button } from "@/app/ui/button";
import { UpdateDepartment } from "@/app/ui/department/buttons";
import Link from "next/link";
import { getDepartmentById } from "@/app/lib/department/action";
import { getProjectsForDepartment } from "@/app/lib/project/action";
import { DepartmentIdBreadcrumb } from "@/app/ui/department/breadcrumbs";
export default async function Page({params}: {params: {id: number}}) {
    const id = params.id;
    const department = await getDepartmentById(id);
    const wikiPages = await fetchWikiPagesForDepartment(id);
    const pages = wikiPages?.slice(0, 3);
    console.log(department);
    if (!department) {
        notFound();
    }

    function createProject() {
        redirect(`/dashboard/project/create`)
    }
    function editDepartment() {

        redirect(`/dashboard/department/${id}/edit`)
    }
    //const items = await fetchProjectsForDepartment(id);
    const items = await getProjectsForDepartment(id);

    return (
        <main className="h-full">
            <DepartmentIdBreadcrumb departmentId={params.id} />
            <div className="overflow-hidden h-full">
                <div className="h-[1/8] p-3">
                    <h1 className={`${lusitana.className} text-3xl`}>{department.name}</h1>
                    <hr></hr>
                    <p className={` mt-5 text-md`}>{department.description}</p>
                </div>
                <div className="h-[80%] mt-5 overflow-hidden">
                    <div className="flex-auto w-1/2 p-3 overflow-hidden h-full">
                        <h1 className="text-3xl mb-5">Projects</h1>
                        <div className="h-full">
                            {(items && items.length > 0) ? (<PaginatedList itemsPerPage={3} items={items}/>): (
                                <p>There are no projects yet. Try creating some!</p>
                            )}
                            
                        </div>
                    </div>
                </div>
                <div className="flex flex-row justify-end">
                    <Link href={`/dashboard/department/${id}/edit`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Edit</Link>
                    <Link href={`/dashboard/project/create?departmentId=${id}`} className="flex mr-5 h-10 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Create Project</Link>
                    <Link href={`/dashboard/department/${id}/invite`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Invite</Link>
                    <Link href={`/dashboard/department/${id}/roles`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Roles</Link>
                    <Link href={`/dashboard/department/${id}/wiki`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Wiki Pages</Link>
                    <Link href={`/dashboard/department/${id}/notes`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Notes</Link>
                    
                </div>
            </div>
        </main>
    )
}