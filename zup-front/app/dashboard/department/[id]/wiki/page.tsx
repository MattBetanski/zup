'use server';

import { getWikiPageById, getWikiPagesForDepartment } from "@/app/lib/wiki/action";
import { lusitana } from "@/app/ui/fonts";
import { WikiTableBreadcrumb } from "@/app/ui/wiki/breadcrumbs";
import WikiTable from "@/app/ui/wiki/table";
import Link from "next/link";

export default async function Page({params}: {params: {id: number}}) {
    let pages = await getWikiPagesForDepartment(params.id);
    return (
        <div className="h-full">
            <div className="h-[95%]">
                <WikiTableBreadcrumb departmentId={params.id}/>
                <h1 className={`${lusitana.className} text-3xl`}>Wiki Pages</h1>
                <hr className="mb-3"/>
                <WikiTable pages={pages}/>

            </div>
            <div className="flex flex-row justify-end">
                <Link href={`/dashboard/department/${params.id}/wiki/create`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Create Wiki Page</Link>
            </div>
        </div>
    );
}