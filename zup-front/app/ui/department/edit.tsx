//Broke AFTER PULLING
'use client';
import { EyeIcon, EyeSlashIcon } from "@heroicons/react/24/solid";
import { stat } from "fs";
import { useState } from "react";
import { useFormState } from 'react-dom';
import { Button } from "../button";
import { editDepartment } from "@/app/lib/department/action";
import { useRouter } from "next/navigation";

export default function EditDepartmentForm({params}: {params: {id: string}}) {
    const router = useRouter();
    function cancel() {
        router.back();
    }
    const initialState = {message: null, errors: {}};
    const [state, dispatch] = useFormState(editDepartment, initialState);
    const id = params.id;
    return (
        <form action={dispatch}>
            <input type="hidden" name="id" value={id}/>
            <div className="rounded-md bg-surface-200 p-4 md:p-6">
                {/* Department Name */}
                <div className="mb-4">
                    <label htmlFor="name" className="mb-2 block text-sm font-medium">
                        Department Name
                    </label>
                    <div className="relative">
                        <input type="text" id="name" name="name" placeholder="Enter Department name" className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] text-sm outline-2 placeholder:text-gray-500 bg-surface-200" aria-describedby="name-error" />
                    </div>
                    <div id="name-error" aria-live="polite" aria-atomic="true">
                        {state.errors?.name && state.errors.name.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>
                                {error}
                            </p>
                        ))}
                    </div>                           
                </div>
                {/* Description */}
                <div className="mb-4">
                    <label htmlFor="description" className="mb-2 block text-sm font-medium">Description</label>
                    <div className="relative mt-2 rounded-md">
                        <div className="relative">
                            <input id="description" name="description" type="text" placeholder="Enter description ..." className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] text-sm outline-2 placeholder:text-gray-500 bg-surface-200" aria-describedby="description-error"/>
                        </div>
                        <div id="description-error" aria-live="polite" aria-atomic="true">
                            {state.errors?.description && state.errors.description.map((error: string) => (
                                <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                            ))}
                        </div>
                    </div>
                </div>
                {/* Visibility */}
                <fieldset>
                    <legend className="mb-2 block text-sm font-medium">Visibility</legend>
                    <div className="rounded-r-3xl rounded-l-lg border border-gray-500 bg-surface-200 px-[14px] py-3">
                        <div className="flex gap-4">
                            <div className="flex items-center">
                                <input id="private" name="visibility" type="radio" value="private" className="h-4 w-4 cursor-pointer border-gray-300 bg-gray-100 text-gray-600 focus:ring-2" aria-describedby="visibility-error"/>
                                <label htmlFor="private" className="ml-2 flex cursor-pointer items-center gap-1.5 rounded-full bg-gray-100 px-3 py-1.5 text-xs font-medium text-gray-600">
                                    Private <EyeSlashIcon className="h-4 w-4"/>
                                </label>
                            </div>
                            <div className="flex items-center">
                                <input id="public" name="visibility" type="radio" value="public" className="h-4 w-4 cursor-pointer border-gray-300 bg-gray-100 text-gray-600 focus:ring-2" aria-describedby="visibility-error"/>
                                <label htmlFor="public" className="ml-2 flex cursor-pointer items-center gap-1.5 rounded-full bg-green-500 px-3 py-1.5 text-xs font-medium text-gray-600">
                                    Public <EyeIcon className="h-4 w-4"/>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div id="visibility-error" aria-polite="true" aria-atomic="true">
                        {state.errors?.visibility && state.errors.visibility.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                        ))}
                    </div>
                </fieldset>
            </div>
            <div className="mt-6 flex justify-end gap-4">
                <Button className="flex h-10 bg-surface-300 items-center rounded-lg px-4 text-sm font-medium hover:bg-surface-400" onClick={cancel}>Cancel</Button>
                <Button type="submit">Create Department</Button>
            </div>
        </form>
    )
}