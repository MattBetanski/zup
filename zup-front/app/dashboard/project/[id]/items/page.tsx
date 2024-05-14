'use server';

import { UserCircleIcon } from '@heroicons/react/20/solid';
import {TreeView} from '@primer/react';
import { BugIcon, CodeIcon, FileIcon, PackageIcon, RepoIcon } from '@primer/octicons-react';
import Link from 'next/link';
import { Item, ItemType } from '@/app/lib/definitions';
import React from 'react';
import { getItemsForProject } from '@/app/lib/items/action';
import ItemListing from '@/app/ui/items/listing';
export default async function Page({params}: {params: {id: number}}) {
   
    let items = await getItemsForProject(params.id);
    console.log(items);


    return (
        <main className='bg-surface-200'>
            <ItemListing items={items}/>
        </main>
    )
}