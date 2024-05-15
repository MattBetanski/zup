'use client'
import { BookIcon } from "@primer/octicons-react";
import { ActionList, Button } from "@primer/react";
import { Lustria } from "next/font/google";
import { lusitana } from "../fonts";
import Link from "next/link";

export function RecentWikiPages({pages, departmentId}: {pages: any[], departmentId: string}) {

    return (
        <div className="w-full h-full">
            <ActionList.Heading as="h1">
                <Link href={`/dashboard/department/${departmentId}/wiki`}>
                    <h1 className={`${lusitana.className} text-blue-400 underline`}>
                        Wiki Pages
                    </h1>
                </Link>
                {pages.map(x => <ActionList.LinkItem href={`/dashboard/wiki/${x.wikiPageId}`}>
                    <div className="hover:bg-surface-400 rounded-md h-6 w-full flex flex-row">
                        <ActionList.LeadingVisual>
                            <BookIcon/>
                        </ActionList.LeadingVisual>
                        {x.title}
                    </div>
                </ActionList.LinkItem>)}
            </ActionList.Heading>
        </div>
    );
}