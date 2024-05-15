'use server';

import { getWikiPageById } from "@/app/lib/wiki/action";
import { lusitana } from "@/app/ui/fonts";
import { WikiPageBreadcrumb } from "@/app/ui/wiki/breadcrumbs";
import ViewWiki from "@/app/ui/wiki/view";
import { notFound } from "next/navigation";

export default async function Page({params}: {params: {id: number, wikiId: number}}) {
    const page = await getWikiPageById(params.wikiId);
    if (!page) notFound();
    const contents = `
    # This is my markdown content

    ## This is a sub heading

    > todo: This is a list

    [link](blink)
    `;
    return (
        <main className="h-full">
            <WikiPageBreadcrumb departmentId={params.id} wikiPageId={params.wikiId}/>
            <h1 className={`${lusitana.className} text-3xl`}>{page.title}</h1>
            <hr className="mb-2"/>
            <ViewWiki contents={page.content}/>

        </main>
    );
}