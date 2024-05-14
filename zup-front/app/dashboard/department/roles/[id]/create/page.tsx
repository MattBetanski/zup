'use client'
//TODO: Page for creating role for department NOT TESTED
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

export default function Form() {
    const router = useRouter();

    function cancel() {
        router.back();
    }
  const initialState: CreateDepartmentState = { message: null, errors: {}};
  const [state, dispatch] = useFormState(createDepartment, initialState);
  return (
    <form action={dispatch}>
      <div className="rounded-md bg-surface-200 p-4 md:p-6">
        {/* Role Name */}
        <div className="mb-4">
          <label htmlFor="name" className="mb-2 block text-sm font-medium">
            Role Name
          </label>
          <div className="relative">
              <input
                id="name"
                name="name"
                type="text"
                placeholder="Enter role name"
                className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                aria-describedby='amount-error'
              />
            <UserCircleIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500" />
          </div>
          <div id="customer-error" aria-live="polite" aria-atomic="true">
            {state.errors?.name &&
              state.errors.name.map((error: string) => (
                <p className='mt-2 text-sm text-red-500' key={error}>
                  {error}
                </p>
              ))}
          </div>
        </div>

        {/* Description */}
        <div className="mb-4">
          <label htmlFor="amount" className="mb-2 block text-sm font-medium">
            Description
          </label>
          <div className="relative mt-2 rounded-md">
            <div className="relative">
              <input
                id="description"
                name="description"
                type="text"
                placeholder="Enter Description"
                className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                aria-describedby='amount-error'
              />
              <CurrencyDollarIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-gray-900" />
            </div>
            <div id="invoice-error" aria-live="polite" aria-atomic="true">
              {state.errors?.description &&
                state.errors.description.map((error: string) => (
                  <p className='mt-2 text-sm text-red-500' key={error}>
                    {error}
                  </p>
                ))}
            </div>
          </div>
        </div>

        {/* Visibility */}
        <fieldset>
          <legend className="mb-2 block text-sm font-medium">
            Permissions
          </legend>
          <div className="rounded-r-3xl rounded-l-lg border border-gray-500 bg-surface-200 px-[14px] py-3">
            <div className="flex gap-4">
              <div className="flex items-center">
                <input
                  id="public"
                  name="Permissions"
                  type="radio"
                  value="Read Only"
                  className="h-4 w-4 cursor-pointer border-gray-300 bg-gray-100 text-gray-600 focus:ring-2"
                  aria-describedby='status-error'
                />
                <label
                  htmlFor="pending"
                  className="ml-2 flex cursor-pointer items-center gap-1.5 rounded-full bg-gray-100 px-3 py-1.5 text-xs font-medium text-gray-600"
                >
                  Read Access <EyeSlashIcon className="h-4 w-4" />
                </label>
              </div>
              <div className="flex items-center">
                <input
                  id="public"
                  name="Permissions"
                  type="radio"
                  value="Write Only"
                  className="h-4 w-4 cursor-pointer border-gray-300 bg-gray-100 text-gray-600 focus:ring-2"
                />
                <label
                  htmlFor="paid"
                  className="ml-2 flex cursor-pointer items-center gap-1.5 rounded-full bg-green-500 px-3 py-1.5 text-xs font-medium text-white"
                >
                  Write Access <EyeIcon className="h-4 w-4" />
                </label>
              </div>
            </div>
          </div>
          <div id="status-error" aria-polite="true" aria-atomic="true">
              {state.errors?.visibility &&
                state.errors.visibility.map((error: string) => (
                  <p className='mt-2 text-sm text-red-500' key={error}>
                    {error}
                  </p>
                ))}
          </div>
          <div id="error-message" aria-atomic="true">
            {state.message && 
              (
                <p className="mt-2 text-sm text-red-500" key={state.message}>
                  {state.message}
                </p>
              )}
          </div>
        </fieldset>
      </div>
      <div className="mt-6 flex justify-end gap-4">
        <Button className='flex h-10 bg-surface-300 items-center rounded-lg px-4 text-sm font-medium hover:bg-surface-400' onClick={cancel}>Cancel</Button>
        <Button type="submit">Create Role</Button>
      </div>
    </form>
  );
}
