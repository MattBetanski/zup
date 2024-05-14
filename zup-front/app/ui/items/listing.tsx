'use client'
import { Item, ItemType } from "@/app/lib/definitions";
import { BugIcon, CodeIcon, FileIcon, PackageIcon, RepoIcon } from "@primer/octicons-react";
import { TreeView } from "@primer/react";
import Link from "next/link";
import React from "react";

export default function ItemListing({items}: {items: Item[]}) {
    type ItemTuple = {
        item: Item;
        children: ItemTuple[];
    };
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

    function onSelect(id: string) {
        console.log(id);
    }
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
    return (
        <main>
            {items.length > 0 ? 
            <TreeView>
                {itemList.map(itemTuple => renderTree(itemTuple))}
            </TreeView> : 
            <p className="bg-surface-100">No items, try creating one</p>
        }
        </main>
    );
}