'use client';

import { createProject, CreateProjectState } from "@/app/lib/project/action";
import { useRouter } from "next/navigation";
import { useFormState } from "react-dom";
import { Button } from "../button";

export default function Form({departmentId}: {departmentId: number}) {
    const router = useRouter();

    function cancel() {
        router.back();
    }

    const initialState: CreateProjectState = {message: null, errors: {}};
    const [state, dispatch] = useFormState(createProject, initialState);

    return (
        <form action={dispatch}>
            <div className="rounded-md bg-surface-200 p-4 md:p-6">
                <input hidden={true} name="departmentId" value={departmentId} type="number"/>
                {/* Project Name */}
                <div className="mb-4">
                    <label htmlFor="name" className="mb-2 block text-sm font-medium">Project Name</label>   
                    <div className="relative">
                        <input id="name" name="name" type="text" placeholder="Enter project name" className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px]  text-sm outline-2 placeholder:text-gray-500 bg-surface-200" aria-describedby="name-error"/>
                    </div>
                    <div id="name-error" aria-live="polite" aria-atomic="true">
                        {state.errors?.name && state.errors.name.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                        ))}
                    </div>
                </div>
                {/* Project Description */}
                <div className="mb-4">
                    <label htmlFor="name" className="mb-2 block text-sm font-medium">Project Name</label>   
                    <div className="relative">
                        <input id="description" name="description" type="text" placeholder="Enter project description" className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px]  text-sm outline-2 placeholder:text-gray-500 bg-surface-200" aria-describedby="name-error"/>
                    </div>
                    <div id="name-error" aria-live="polite" aria-atomic="true">
                        {state.errors?.description && state.errors.description.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                        ))}
                    </div>
                </div>
            </div>
            <div className="mt-6 flex justify-end gap-4">
                <Button className="flex h-10 bg-surface-300 items-center rounded-lg px-4 text-sm font-medium hover:bg-surface-400" onClick={cancel}>Cancel</Button>
                <Button type="submit">Create Project</Button>
            </div>
        </form>
    )
}