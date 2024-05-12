'use client';
import { deleteDepartment, removeInvite } from "@/app/lib/department/action";
import { PencilIcon, PlusIcon, TrashIcon } from "@heroicons/react/20/solid";
import { Box, Dialog, Text } from "@primer/react";
import Link from "next/link";
import { useState } from "react";
import { Button } from "../button";
import { XIcon } from "@primer/octicons-react";

export function CreateDepartment() {
  return (
    <Link
      href="/dashboard/department/create"
      className="flex h-10 items-center rounded-lg bg-blue-600 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600"
    >
      <span className="hidden md:block">Create Department</span>{' '}
      <PlusIcon className="h-5 md:ml-4" />
    </Link>
  );
}

export function UpdateDepartment({ id }: { id: string }) {
  return (
    <Link
      href={`/dashboard/department/${id}/edit`}
      className="rounded-md border p-2 hover:bg-surface-300"
    >
      <PencilIcon className="w-5" />
    </Link>
  );
}

export function DeleteDepartment({ id }: { id: string }) {
    const [isOpen, setIsOpen] = useState(false);
    const deleteDepartmentWithId = () => {
        setIsOpen(false);
        deleteDepartment.bind(null, id)();
    }
    return (
        <div>
            <button onClick={() => setIsOpen(true)} className="rounded-md border p-2 hover:bg-surface-300">
                <span className="sr-only">Delete</span>
                <TrashIcon className="w-5" />
            </button>
            <Dialog isOpen={isOpen} onDismiss={() => setIsOpen(false)}>
                <div className="bg-white rounded-t-lg relative drop-shadow-lg w-full h-full">
                    <Dialog.Header>
                        Delete Department?
                    </Dialog.Header>
                    <Box p={3}>
                        <Text>Are you sure you want to delete the department? This cannot be undone.</Text>
                    </Box>
                    <div className="bg-white w-full relative">
                        <div className="flex flex-row absolute right-0 bg-white w-full rounded-b-lg">
                            <form action={deleteDepartmentWithId}>
                                <Button className="m-3 bg-red-500 hover:bg-red-600 active:bg-red-700" >Delete Department</Button>
                            </form>
                            <Button className="m-3 bg-green-700 hover:bg-green-800 active:bg-green-900" onClick={() => setIsOpen(false)}>Cancel</Button>
                        </div>

                    </div>
                </div>
            </Dialog>
        </div>
    );
}

export function DeleteInvite({inviteeId, departmentId}: {inviteeId: number, departmentId: number}) {
  return (
    <div>
      <form action={removeInvite}>
        <input hidden name="inviteeId" value={inviteeId} readOnly />
        <input hidden name="departmentId" value={departmentId} readOnly />
        <button type="submit" className="rounded-md border p-2 hover:bg-surface-300">
          <span className="sr-only">Delete</span>
          <XIcon className="w-5" />
        </button>
      </form>
    </div>
  )
}