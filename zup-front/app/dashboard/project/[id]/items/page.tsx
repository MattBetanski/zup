'use client';

import { UserCircleIcon } from '@heroicons/react/20/solid';
import {TreeView} from '@primer/react';
import { BugIcon, CodeIcon, FileIcon, PackageIcon, RepoIcon } from '@primer/octicons-react';
import Link from 'next/link';
import { getItemsForProject } from '@/app/lib/data';
import { Item, ItemType } from '@/app/lib/definitions';
import React from 'react';
export default async function Page() {

    type ItemTuple = {
        item: Item;
        children: ItemTuple[];
    };
    let items = await getItemsForProject("hi");
    const itemMap = new Map<number, ItemTuple>();
    const itemList: ItemTuple[] = [];
    items?.forEach(item => {
        const itemTuple: ItemTuple = {
            item: item,
            children: []
        }
        itemMap.set(item.itemId, itemTuple);
    })

    items?.forEach(item => {
        const {itemId, parentId} = item;
        const itemTuple = itemMap.get(itemId);
        if (!itemTuple) return;
        if (parentId === null) {

            itemList.push(itemTuple);
        } else {
            const parentItemTuple = itemMap.get(parentId);
            if (parentItemTuple) {
                parentItemTuple.children.push(itemTuple);
            }
        }
    });

    const generateLeadingVisual = (type: ItemType) => {
        switch (type) {
            case ItemType.Epic:
                return <TreeView.LeadingVisual><PackageIcon /></TreeView.LeadingVisual>
            case ItemType.Feature:
                return <TreeView.LeadingVisual><RepoIcon /></TreeView.LeadingVisual>
            case ItemType.Requirement:
                return <TreeView.LeadingVisual><CodeIcon /></TreeView.LeadingVisual>
            case ItemType.Task:
                return <TreeView.LeadingVisual><FileIcon /></TreeView.LeadingVisual>
            case ItemType.Bug:
                return <TreeView.LeadingVisual><BugIcon /></TreeView.LeadingVisual>


        }
    };

    const renderTree = (itemTuple: ItemTuple) => {
        const {item, children} = itemTuple;
        return (
            <React.Fragment key={item.itemId}>
                <TreeView.Item id={item.itemId.toString()} onSelect={() => onSelect(item.itemId.toString())}>
                    {generateLeadingVisual(item.type)}
                    <Link href="/dashboard" className='underline text-blue-500'>{item.name}</Link>
                    {children.length > 0 && (
                        <TreeView.SubTree>
                            {children.map(childrenItemTuple => renderTree(childrenItemTuple))}
                        </TreeView.SubTree>
                    )}
                    
                </TreeView.Item>
            </React.Fragment>
        );
    };

    console.log(itemList);
    function onSelect(id: string) {
        console.log(id);
    }
    return (
        <main className='bg-surface-200'>
            <TreeView>
                {itemList.map(itemTuple => renderTree(itemTuple))}
            </TreeView>
            <TreeView>
                <TreeView.Item id="src" defaultExpanded className='text-blue-500'>
                    <TreeView.LeadingVisual>
                        <PackageIcon />
                    </TreeView.LeadingVisual>
                    Zup
                    <TreeView.SubTree>
                        <TreeView.Item id="item1" onSelect={() => {onSelect("3")}}>
                            Help
                        </TreeView.Item>
                    </TreeView.SubTree>
                </TreeView.Item>
            </TreeView>
        </main>
    )
}