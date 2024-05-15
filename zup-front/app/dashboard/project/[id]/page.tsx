import { getProjectById } from "@/app/lib/project/action";
import { lusitana } from "@/app/ui/fonts";
import Link from "next/link";
import { notFound } from "next/navigation";

//TODO: Page for viewing project information
export default async function Page({params}: {params: {id: number}}) {
    const project = await getProjectById(params.id);
    if (!project) {
        notFound();
    }
    return (
        <main className="h-[95%]">
            <div className="flex-col h-full">
                <div className="p-3 flex-grow h-3/4">
                    <h1 className={`${lusitana.className} text-3xl`}>{project.name}</h1>
                    <hr />
                    <p className="mt-5 text-md h-1/5">{project.description}</p>
                </div>

            </div>
                 <div className="justify-end flex flex-row">
                    <Link href={`/dashboard/project/${params.id}/items`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Items</Link>
                    <Link href={`/dashboard/project/${params.id}/edit`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Edit</Link>
                </div>
        </main>
    )
}
