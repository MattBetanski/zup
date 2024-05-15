'use client';

import { createItem, CreateItemState } from "@/app/lib/items/action";
import { useRouter } from "next/navigation";
import { useFormState } from "react-dom";
import DateSelector from "../inputs/date";
import React, { use, useEffect, useState } from "react";
import {getItemsForType} from '../../lib/items/action';
import { ItemType, Item } from "@/app/lib/definitions";
import { Button } from "../button";
export default function Form(props: {projectId: number}) {
    const [loading, setLoading] = useState(false);
    const [type, setType] = useState('Epic');
    const [itemState, setItemState] = useState('Open');
    const [selectedParentItem, setSelectedParentItem] = useState('-1');
    let temp: Item[] = [];
    const [parentItems, setParentItems] = useState(temp);
    useEffect(() => {
        const fetchStuff = async () => {
            try{
                let items = await getItemsForType(type, props.projectId);
                console.log(items);
                if (items) {
                    console.log(items);
                    setParentItems(items);
                } else {
                    setParentItems([])
                }

            } catch (err) {
                console.error(err);
            }
        };
        fetchStuff();
    }, [type]);
    const router = useRouter();

    function cancel() {
        router.back();
    }
    function handleTypeChange(event: React.ChangeEvent<HTMLSelectElement>) {
        const newValue = event.target.value;
        setType(newValue);

    }

    function handleItemStateChange(event: React.ChangeEvent<HTMLSelectElement>) {
        const newValue = event.target.value;
        setItemState(newValue);
    }
    const initialState: CreateItemState = {message: null, errors: {}};
    const [state, dispatch] = useFormState(createItem, initialState);
    return (
        <form action={dispatch}>
            <input hidden name="projectId" type="number" value={props.projectId}/>
            <div className="rounded-md bg-surface-200 p-4 md:p-6">
                {/* Item Name */}
                <div className="mb-4">
                    <label htmlFor="name" className="mb-2 block text-sm font-medium">
                        Item Name
                    </label>
                    <div className="relative">
                        <input id="name" name="name" type="text" placeholder="Enter item name" aria-describedby="name-error"
                            className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                        />
                    </div>
                    <div id="name-error" aria-live="polite" aria-atomic="true">
                        {state.errors?.name && state.errors.name.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                        ))}
                    </div>
                </div>
                {/* Item Description */}
                <div className="mb-4">
                    <label htmlFor="description" className="mb-2 block text-sm font-medium">
                        Item Description
                    </label>
                    <div className="relative">
                        <input id="description" name="description" type="text" placeholder="Enter item description" aria-describedby="description-error"
                            className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                        />
                    </div>
                    <div id="description-error" aria-live="polite" aria-atomic="true">
                        {state.errors?.description && state.errors.description.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                        ))}
                    </div>
                </div>
                {/* 
                <div className="mb-4">
                    <DateSelector description="Date Selector" name="Deadline" />
                </div>
                */}
                {/* Item Type */}
                <div className="mb-4">
                    <label htmlFor="description" className="mb-2 block text-sm font-medium">
                        Item Type
                    </label>
                    <div className="relative">
                        <select id="type" name="type" value={type} aria-describedby="type-error" onChange={handleTypeChange}
                            className="peer block w-full cursor-pointer rounded-r-3xl rounded-l-lg border border-gray-500 py-2 text-sm outline-2 placeholder:text-gray-500 text-white bg-surface-200"
                        >
                            <option value="" disabled>Select an item type</option>
                            <option value="Epic" >Epic</option>
                            <option value="Feature" >Feature</option>
                            <option value="Requirement" >Requirement</option>
                            <option value="Task" >Task</option>
                            <option value="Bug" >Bug</option>
                        </select>
                    </div>
                    <div id="type-error" aria-live="polite" aria-atomic="true">
                        {state.errors?.type && state.errors.type.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                        ))}
                    </div>
                </div>
                {/* Parent Item */}
                <div className="mb-4">
                    <label htmlFor="description" className="mb-2 block text-sm font-medium">
                        Parent Item
                    </label>
                    <div className="relative">
                        <select id="parentId" name="parentId"  defaultValue={"-1"} aria-describedby="parent-error"
                            className="peer block w-full cursor-pointer rounded-r-3xl rounded-l-lg border border-gray-500 py-2 text-sm outline-2 placeholder:text-gray-500 text-white bg-surface-200 disabled:bg-surface-300"
                        >
                            <option value="-1">Select a parent item</option>
                            {parentItems && parentItems.map((x) => (
                                <option value={x.itemId}>{x.name}</option>
                            ))}
                        </select>
                    </div>
                    <div id="parent-error" aria-live="polite" aria-atomic="true">
                        {state.errors?.parentId && state.errors.parentId.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                        ))}
                    </div>
                </div>
                {/* Item State */}
                <div className="mb-4">
                    <label htmlFor="description" className="mb-2 block text-sm font-medium">
                        Item State
                    </label>
                    <div className="relative">
                        <select id="type" name="state" value={itemState} aria-describedby="type-error" onChange={handleItemStateChange}
                            className="peer block w-full cursor-pointer rounded-r-3xl rounded-l-lg border border-gray-500 py-2 text-sm outline-2 placeholder:text-gray-500 text-white bg-surface-200"
                        >
                            <option value="" disabled>Select an item state</option>
                            <option value="Open" >Open</option>
                            <option value="InProgress" >In Progress</option>
                            <option value="Blocked" >Blocked</option>
                            <option value="Completed" >Completed</option>
                        </select>
                    </div>
                    <div id="type-error" aria-live="polite" aria-atomic="true">
                        {state.errors?.type && state.errors.type.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                        ))}
                    </div>
                </div>
                <div id="error-message" aria-atomic="true">
                    {state.message && (
                        <p className="mt-2 text-sm text-red-500" key={state.message}>{state.message}</p>
                    )}
                </div>
            </div>
            <div className="mt-6 flex justify-end gap-4">
                <Button className="flex h-10 bg-surface-300 items-center rounded-lg px-4 text-sm font-medium hover:bg-surface-400 active:bg-surface-200" onClick={cancel}>Cancel</Button>
                <Button type="submit">Create Item</Button>
            </div>
        </form>
    );
}