'use client'
import { Item, ItemState, ItemType } from "@/app/lib/definitions";
import { Bug } from "@phosphor-icons/react";
import { BugIcon, CodeIcon, FileIcon, PackageIcon, RepoIcon } from "@primer/octicons-react";
import { TreeView } from "@primer/react";
import clsx from "clsx";
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
    console.log(itemList);
    const generateLeadingVisual = (type: ItemType) => {
        if (type.toString() == ItemType[ItemType.Epic]) {
            return <TreeView.LeadingVisual><PackageIcon /></TreeView.LeadingVisual>;
        } else if(type.toString() == ItemType[ItemType.Feature]) {
            return <TreeView.LeadingVisual><RepoIcon /></TreeView.LeadingVisual>;
        } else if(type.toString() == ItemType[ItemType.Requirement]) {
            return <TreeView.LeadingVisual><CodeIcon /></TreeView.LeadingVisual>;
        } else if(type.toString() == ItemType[ItemType.Task]) {
            return <TreeView.LeadingVisual><FileIcon /></TreeView.LeadingVisual>;
        } else {
            return <TreeView.LeadingVisual><BugIcon /></TreeView.LeadingVisual>;
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
                    <Link href={`/dashboard/project/${item.projectId}/items/${item.itemId}`} className={clsx('underline text-blue-500', item.state.toString() == ItemState[ItemState.InProgress] && "text-green-500", item.state.toString() == ItemState[ItemState.Blocked] && "text-yellow-500", item.state.toString() == ItemState[ItemState.Completed] && "text-slate-500")}>{item.name}</Link>
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
            <p className="">No items, try creating one</p>
        }
        </main>
    );
}