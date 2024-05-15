'use client';

import { useFormState } from 'react-dom';
import { CustomerField } from '@/app/lib/definitions';
import Link from 'next/link';
import {
  CheckIcon,
  ClockIcon,
  CurrencyDollarIcon,
  UserCircleIcon,
} from '@heroicons/react/24/outline';
import { Button } from '@/app/ui/button';
import { stat } from 'fs';
import { EyeIcon, EyeSlashIcon } from '@heroicons/react/24/solid';
import { useRouter, useSearchParams } from 'next/navigation';
import { createDepartment, CreateDepartmentState } from '@/app/lib/department/action';
import { createNote, CreateNoteState } from '@/app/lib/note/action';

export default function Form({departmentId}: {departmentId: number}) {
    const router = useRouter();

    function cancel() {
        router.back();
    }
  const initialState: CreateNoteState = { message: null, errors: {}};
  const [state, dispatch] = useFormState(createNote, initialState);
  return (
    <form action={dispatch}>
        <input hidden type='text' name="departmentId" value={departmentId} readOnly />
        <div className="rounded-md bg-surface-200 p-4 md:p-6">
            {/* Note Title */}
            <div className="mb-4">
            <label htmlFor="name" className="mb-2 block text-sm font-medium">
                Title
            </label>
            <div className="relative">
                <input
                    id="title"
                    name="title"
                    type="text"
                    placeholder="Enter note title"
                    className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                    aria-describedby='title-error'
                />
            </div>
            <div id="title-error" aria-live="polite" aria-atomic="true">
                {state.errors?.title &&
                state.errors.title.map((error: string) => (
                    <p className='mt-2 text-sm text-red-500' key={error}>
                    {error}
                    </p>
                ))}
            </div>
            </div>

            {/* Content */}
            <div className="mb-4">
            <label htmlFor="amount" className="mb-2 block text-sm font-medium">
                Content
            </label>
            <div className="relative mt-2 rounded-md">
                <div className="relative">
                <input
                    id="content"
                    name="content"
                    type="text"
                    placeholder="Enter content"
                    className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px]  text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                    aria-describedby='content-error'
                />
                </div>
                <div id="content-error" aria-live="polite" aria-atomic="true">
                {state.errors?.content &&
                    state.errors.content.map((error: string) => (
                    <p className='mt-2 text-sm text-red-500' key={error}>
                        {error}
                    </p>
                    ))}
                </div>
            </div>
            </div>
        </div>
        <div className="mt-6 flex justify-end gap-4">
            <Button className='flex h-10 bg-surface-300 items-center rounded-lg px-4 text-sm font-medium hover:bg-surface-400' onClick={cancel}>Cancel</Button>
            <Button type="submit">Create Note</Button>
        </div>
    </form>
  );
}

