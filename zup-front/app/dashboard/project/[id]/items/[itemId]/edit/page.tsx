'use server';
import { ItemState, ItemType } from "@/app/lib/definitions";
import { getItemById } from "@/app/lib/items/action";
import { EditBreadcrumb } from "@/app/ui/items/breadcrumbs";
import EditItemForm from "@/app/ui/items/edit";
import { notFound } from "next/navigation";

export default async function Page({params}: {params: {id: number, itemId: number}}) {
    let item = await getItemById(params.itemId);
    item = {
        itemId: 21,
        name: "Test Name",
        description: "Test Description",
        state: ItemState.Blocked,
        type:  ItemType.Feature,
        projectId: 3,
        parentId: null,
        ownerId: 0,
        createdDate: new Date(Date.now()),
    }
    if (!item) notFound();
    return (
        <main>
            <EditBreadcrumb item={item}/>
            <EditItemForm item={item}/>
        </main>
    );
}