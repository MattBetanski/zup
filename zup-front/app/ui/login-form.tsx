'use client';

import { source } from '@/app/ui/fonts';
import {
  AtSymbolIcon,
  KeyIcon,
  ExclamationCircleIcon,
} from '@heroicons/react/24/outline';
import { ArrowRightIcon } from '@heroicons/react/20/solid';
import { Button } from './button';
import { useFormState, useFormStatus } from 'react-dom';
import Link from 'next/link';
import { UserCircleIcon } from '@heroicons/react/20/solid';
import { loginAccount, LoginAccountState } from '../lib/authentication/action';

export default function LoginForm() {
  const initialState: LoginAccountState = {message: null, errors: {}};
  const [state, dispatch] = useFormState(loginAccount, initialState);
  return (
    <form action={dispatch} className="space-y-3 drop-shadow-2xl text-white bg-surface-300 rounded-b-3xl">
      <div className="flex-1 rounded-lg px-6 pb-4 pt-8">
        <h1 className={`${source.className} mb-3 text-2xl`}>
          Please log in to continue.
        </h1>
        <div className="w-full">
          <div className='mb-4'>
            <label
              className="mb-3 mt-5 block text-xs font-medium text-white"
              htmlFor="username"
            >
              Username
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                id="username"
                type="username"
                name="username"
                placeholder="Enter your username"
                aria-describedby='username-error'
              />
              <UserCircleIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-white" />
            </div>
            <div id="username-error" aria-live='polite' aria-atomic='true'>
              {state.errors?.username && state.errors.username.map((error: string) => (
                <p className='mt-2 text-sm text-red-500' key={error}>{error}</p>
              ))}
            </div>
          </div>
          <div className="mt-4">
            <label
              className="mb-3 mt-5 block text-xs font-medium text-white"
              htmlFor="password"
            >
              Password
            </label>
            <div className="relative">
              <input
                className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                id="password"
                type="password"
                name="password"
                placeholder="Enter password"
                aria-describedby='password-error'
              />
              <KeyIcon className="pointer-events-none absolute left-3 top-1/2 h-[18px] w-[18px] -translate-y-1/2 text-gray-500 peer-focus:text-white" />
            </div>
            <div id="password-error" aria-live='polite' aria-atomic='true'>
              {state.errors?.password && state.errors.password.map((error: string) => (
                <p className='mt-2 text-sm text-red-500' key={error}>{error}</p>
              ))}
            </div>
          </div>
        </div>
        <LoginButton />
        <CreateLink />
        <div className="flex h-8 items-end space-x-1"
          aria-live="polite"
          aria-atomic="true"
        >
          {state.message && (
            <>
              <ExclamationCircleIcon className='h-5 w-5 text-red-500' />
              <p className='text-sm text-red-500'>{state.message}</p>
            </>
          )}
        </div>
      </div>
    </form>
  );
}

function LoginButton() {
  const { pending } = useFormStatus();

  return (
    <Button className="mt-4 w-full bg-primary-300" aria-disabled={pending}>
      Log in <ArrowRightIcon className="ml-auto h-5 w-5 text-gray-50" />
    </Button>
  );
}

function CreateLink() {
  return (
    <div className='mt-5 text-blue-300 text-sm text-center'>
      <Link href="/create">Create an account</Link>
    </div>
  )
}