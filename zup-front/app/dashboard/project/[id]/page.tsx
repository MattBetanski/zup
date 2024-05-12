import { getProjectById } from "@/app/lib/project/action";
import { lusitana } from "@/app/ui/fonts";
import { notFound } from "next/navigation";

//TODO: Page for viewing project information
export default async function Page({params}: {params: {id: number}}) {
    const project = await getProjectById(params.id);
    if (!project) {
        notFound();
    }
    return (
        <main>
            <div className="flex flex-col overflow-hidden">
                <div className="h-[1/8] p-3">
                    <h1 className={`${lusitana.className} text-3xl`}>{project.name}</h1>
                    <hr />
                    <p className="mt-5 text-md">{project.description}</p>
                </div>
            </div>
        </main>
    )
}
