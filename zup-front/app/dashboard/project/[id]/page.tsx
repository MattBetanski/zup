import { lusitana } from "@/app/ui/fonts";

//TODO: Page for viewing project information
export default async function Page({params}: {params: {id: number}}) {

    return (
        <main>
            <div className="flex flex-col overflow-hidden">
                <div className="h-[1/8] p-3">
                    <h1 className={`${lusitana.className} text-3xl`}></h1>
                </div>
            </div>
        </main>
    )
}
