import { Item, ItemState, ItemType } from "@/app/lib/definitions";
import { getItemById, getItemChildren, getItemParent } from "@/app/lib/items/action";
import { ItemDetailBreadcrumb } from "@/app/ui/items/breadcrumbs";
import ItemDetails from "@/app/ui/items/detail";
import Link from "next/link";
import { notFound } from "next/navigation";

export default async function Page({params}: {params: {id: number, itemId: number}}) {
    let item = await getItemById(params.itemId);
    if (!item) notFound();
    let parent;
    if (item.parentId) 
        parent = await getItemById(item.parentId);
    else
        parent = null; 
    let children = await getItemChildren(params.itemId);
    console.log("children = ", children);
    /*
    let item: Item = {
        itemId: 21,
        name: "Test Name",
        description: "Test Description",
        state: ItemState.Open,
        type:  ItemType.Epic,
        projectId: 3,
        parentId: null,
        ownerId: 0,
        createdDate: new Date(Date.now()),
    }
    if (!item) notFound();

    let parent: Item = {
        itemId: 1,
        name: "The parent item",
        description: "Test Description",
        state: ItemState.Open,
        type:  ItemType.Epic,
        projectId: 0,
        parentId: null,
        ownerId: 0,
        createdDate: new Date(Date.now()),
    }

    let children: Item[] = [
        {
            itemId: 2,
            name: "Child 1",
            description: "Test Description",
            state: ItemState.Open,
            type:  ItemType.Epic,
            projectId: 0,
            parentId: null,
            ownerId: 0,
            createdDate: new Date(Date.now()),
        },
        {
            itemId: 3,
            name: "Child Item 2",
            description: "Test Description",
            state: ItemState.Open,
            type:  ItemType.Epic,
            projectId: 0,
            parentId: null,
            ownerId: 0,
            createdDate: new Date(Date.now()),
        },
        {
            itemId: 4,
            name: "Another child",
            description: "Test Description",
            state: ItemState.Open,
            type:  ItemType.Epic,
            projectId: 0,
            parentId: null,
            ownerId: 0,
            createdDate: new Date(Date.now()),
        }
    ]
    */
    return (
        <main className="h-[95%]">
            <ItemDetailBreadcrumb item={item}/>
            <div className="flex flex-col h-full">
                <ItemDetails item={item} parent={parent} children={children} className="flex-grow h-[90%]"/>
            </div>
            <div className="flex flex-row justify-end">
                <Link href={`/dashboard/project/${item.projectId}/items/${item.itemId}/edit`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Edit</Link>
            </div>
        </main>
    );
}