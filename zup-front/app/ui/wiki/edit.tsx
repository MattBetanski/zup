'use client';

import { createWikiPage, CreateWikiState } from "@/app/lib/wiki/action";
import MDEditor from "@uiw/react-md-editor";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { useFormState } from "react-dom";
import { Button } from "../button";

export default function CreateWiki({id}: {id: number}) {
    const router = useRouter();
    const [value, setValue] = useState("**Hello world!!!**");
    
    function cancel() {
        router.back();
    }
    
    const initialState: CreateWikiState = {message: null, errors: {}};
    const [state, dispatch] = useFormState(createWikiPage, initialState);
    return (
        <form className="h-full" action={dispatch}>
            <input hidden value={value} name="content" readOnly/>
            <input hidden value={id} name="departmentId" readOnly/>
            {/* Page Name */}
            <div className="mb-4">
                <label htmlFor="name" className="mb-2 block text-sm font-medium">
                    Department Name
                </label>
                <div className="relative">
                    <input
                        id="name"
                        name="title"
                        type="text"
                        placeholder="Enter page name"
                        className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                        aria-describedby='name-error'
                    />
                </div>
                <div id="name-error" aria-live="polite" aria-atomic="true">
                    {state.errors?.title &&
                    state.errors.title.map((error: string) => (
                        <p className='mt-2 text-sm text-red-500' key={error}>
                        {error}
                        </p>
                    ))}
                </div>
            </div>
            <MDEditor value={value} onChange={setValue} height={750}/>
            <div id="error-message" aria-atomic="true">
                {state.message && (
                    <p className="mt-2 text-sm text-red-500" key={state.message}>{state.message}</p>
                )}
            </div>
            <div className="flex flex-row justify-end mt-3">
                <Button type="submit">Create Page</Button>
            </div>
        </form>
    );
}