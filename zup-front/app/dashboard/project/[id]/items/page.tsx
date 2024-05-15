'use server';

import { UserCircleIcon } from '@heroicons/react/20/solid';
import {TreeView} from '@primer/react';
import { BugIcon, CodeIcon, FileIcon, PackageIcon, ProjectTemplateIcon, RepoIcon } from '@primer/octicons-react';
import Link from 'next/link';
import { Item, ItemType } from '@/app/lib/definitions';
import React from 'react';
import { getItemsForProject } from '@/app/lib/items/action';
import ItemListing from '@/app/ui/items/listing';
import { getProjectById } from '@/app/lib/project/action';
import { notFound } from 'next/navigation';
import { ProjectItemBreadcrumb } from '@/app/ui/project/breadcrumbs';
export default async function Page({params}: {params: {id: number}}) {
    let project = await getProjectById(params.id);
    if (!project) notFound();
    let items = await getItemsForProject(params.id);
    console.log(items);


    return (
        <main className='h-[95%]'>
            <ProjectItemBreadcrumb projectId={project.projectId}/>
            <div className='flex flex-col h-full'>
                <div className='flex-grow h-[90%]'>
                    <h1>{project.name}</h1>
                    <hr className='mb-5'/>
                    <ItemListing items={items}/>
                </div>
                <div className='flex flex-row justify-end'>

                    <Link href={`/dashboard/project/${params.id}/items/create`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Create Item</Link>
                </div>

            </div>
        </main>
    )
}