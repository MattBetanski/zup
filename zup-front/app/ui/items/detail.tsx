'use client';

import { Item, ItemState } from "@/app/lib/definitions";
import { lusitana } from "../fonts";
import Link from "next/link";
import clsx from "clsx";

export default function ItemDetails({item, parent, children, className}: {item: Item, parent: Item | null, children: Item[] | null, className: string | null | undefined}) {
    return (
        <div className={className ?? ''}>
            <div className="mb-5">
                <h1 className={`${lusitana.className} text-3xl`}>{item.name}</h1>
                <p className="">State: <span className={clsx(' italic text-blue-500', item.state.toString() == ItemState[ItemState.InProgress] && "text-green-500", item.state.toString() == ItemState[ItemState.Blocked] && "text-yellow-500", item.state.toString() == ItemState[ItemState.Completed] && "text-slate-500")}>{item.state}</span></p>
                <p className="">Type: <span className="italic">{item.type}</span></p>
                {
                    parent != null && <div>Parent: <Link href={`/dashboard/project/${parent.projectId}/items/${parent.itemId}`} className="text-blue-500 italic underline">{parent.name}</Link></div>
                }
                <hr className="mt-2" />
            </div>
            <div>
                <p>{item.description}</p>
            </div>
            {
                (children && children?.length > 0) && (
                    <div className="mt-10 border-2 rounded-lg p-2">
                        <h2 className={`${lusitana.className} text-xl`}>Children</h2>
                        <hr />
                        {children.map((child) => (
                            <p className="text-blue-500 underline italic"><Link href={`/dashboard/project/${child.projectId}/items/${child.itemId}`}>{child.name}</Link></p>
                            
                        ))}
                    </div>
                )
            }
        </div>
    );
}