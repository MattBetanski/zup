import { getNotesForDepartment } from "@/app/lib/note/action";
import { NoteListingBreadcrumb } from "@/app/ui/note/breadcrumbs";
import NoteTable from "@/app/ui/note/table";
import Link from "next/link";

export default async function Page({params}: {params: {id: number}}) {
    let notes = await getNotesForDepartment(params.id);
    return (
        <main className="h-full">
            <NoteListingBreadcrumb departmentId={params.id}/>
            <div className=" h-[90%]">

                <NoteTable notes={notes}/>
            </div>
            <div className="flex flex-row justify-end">
                <Link href={`/dashboard/department/${params.id}/notes/create`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Create</Link>
            </div>
        </main>
    );
}