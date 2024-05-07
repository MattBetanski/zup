'use server';

import { fetchDepartmentbyId, fetchProjectsForDepartment, fetchWikiPagesForDepartment } from "@/app/lib/data";
import { lusitana } from "@/app/ui/fonts";
import { notFound } from "next/navigation";
import {Stack} from '@primer/react/experimental';
import { useState } from "react";
import PaginatedList from "@/app/ui/PaginatedLIst";
import { RecentWikiPages } from "@/app/ui/department/wikiPages";
import { Button } from "@/app/ui/button";
export default async function Page({params}: {params: {id: string}}) {
    const id = params.id;
    const department = await fetchDepartmentbyId(id);
    const wikiPages = await fetchWikiPagesForDepartment(id);
    const pages = wikiPages?.slice(0, 3);
    console.log(department);
    if (!department) {
        notFound();
    }

    const items = await fetchProjectsForDepartment(id);

    return (
        <main>
            <div className="flex flex-col overflow-hidden">
                <div className="h-[1/8] p-3 rounded-lg bg-surface-300">
                    <h1 className={`${lusitana.className} text-3xl`}>{department.name}</h1>
                    <p className={` mt-5 text-md`}>{department.description}</p>
                </div>
                <div className="flex h-3/4 mt-5 overflow-hidden">
                    <div className="flex-auto w-1/2 p-3 h-3/4 overflow-hidden">
                        <h1 className="text-3xl mb-5">Projects</h1>
                        <div className="h-full">
                            <PaginatedList itemsPerPage={3} items={items}/>
                        </div>
                    </div>
                    <div className="flex flex-col w-1/2">
                        <div className="flex flex-col relative h-64 bg-surface-300 m-4 rounded-lg border-surface-400 border-2">
                                <RecentWikiPages pages={pages} departmentId={id} />
                                <div className="items-end absolute right-5 bottom-5">
                                    <Button className="w-48 text-center items-center justify-center">Create Wiki Page</Button>
                                </div>
                            </div>
                        <div className="flex h-64 bg-green-300">Text</div>
                        <div className="flex h-64 bg-red-300">Text</div>
                    </div>
                </div>
            </div>
        </main>
    )
}