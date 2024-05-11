'use client';

import { createItem } from "@/app/lib/items/action";
import { useRouter } from "next/navigation";
import { useFormState } from "react-dom";
import DateSelector from "../inputs/date";
import React, { use, useEffect, useState } from "react";
import { getItemsForType } from "@/app/lib/data";

export default function Form() {
    const [loading, setLoading] = useState(false);
    const [type, setType] = useState('epic');
    const [parentItems, setParentItems] = useState([]);
    useEffect(() => {
        const fetchStuff = async () => {
            try{
            let items = await getItemsForType(type);
            console.log(items);
            setParentItems(items);

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
        console.log(newValue);

    }

    const initialState = {message: null, errors: {}};
    const [state, dispatch] = useFormState(createItem, initialState);
    return (
        <form action={dispatch}>
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
                {/* Item Type */}
                <div className="mb-4">
                    <label htmlFor="description" className="mb-2 block text-sm font-medium">
                        Item Type
                    </label>
                    <div className="relative">
                        <select id="type" name="type" value={type} aria-describedby="state-error" onChange={handleTypeChange}
                            className="peer block w-full cursor-pointer rounded-r-3xl rounded-l-lg border border-gray-500 py-2 text-sm outline-2 placeholder:text-gray-500 text-white bg-surface-200"
                        >
                            <option value="" disabled>Select an item type</option>
                            <option value="epic" >Epic</option>
                            <option value="feature" >Feature</option>
                            <option value="requirement" >Requirement</option>
                            <option value="task" >Task</option>
                            <option value="bug" >Bug</option>
                        </select>
                    </div>
                    <div id="type-error" aria-live="polite" aria-atomic="true">
                        {state.errors?.type && state.errors.type.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                        ))}
                    </div>
                </div>
            </div>
        </form>
    );
}