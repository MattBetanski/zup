import { deleteRole } from "@/app/lib/roles/action";
import { PencilIcon, TrashIcon } from "@primer/octicons-react";
import { Box, Button, Dialog, Text } from "@primer/react";
import Link from "next/link";
import { useState } from "react";

export function EditRole({ roleId, departmentId}: { roleId: number, departmentId: number }) {
  return (
    <Link
      href={`/dashboard/department/${departmentId}/roles/${roleId}/edit`}
      className="rounded-md border p-2 hover:bg-surface-300"
    >
      <PencilIcon className="w-5" />
    </Link>
  );
}

export function DeleteRole({ roleId, departmentId }: { roleId: number, departmentId: number }) {
    const [isOpen, setIsOpen] = useState(false);
    const deleteDepartmentWithId = () => {
        setIsOpen(false);
        deleteRole.bind(null, roleId, departmentId)();
    }
    return (
        <div>
            <button onClick={() => setIsOpen(true)} className="rounded-md border p-2 hover:bg-surface-300">
                <span className="sr-only">Delete</span>
                <TrashIcon className="w-5" />
            </button>
            <Dialog isOpen={isOpen} onDismiss={() => setIsOpen(false)}>
                <div className="bg-white rounded-t-lg relative drop-shadow-lg w-full h-full">
                    <Dialog.Header cname="bg-red-300">
                        Delete Role?
                    </Dialog.Header>
                    <Box p={3}>
                        <Text cname="text-black">Are you sure you want to delete the role? This cannot be undone.</Text>
                    </Box>
                    <div className="bg-white w-full relative">
                        <div className="flex flex-row absolute right-0 bg-white w-full rounded-b-lg">
                            <form action={deleteDepartmentWithId}>
                                <Button className="m-3 bg-red-500 hover:bg-red-600 active:bg-red-700" >Delete Role</Button>
                            </form>
                            <Button className="m-3 bg-green-700 hover:bg-green-800 active:bg-green-900" onClick={() => setIsOpen(false)}>Cancel</Button>
                        </div>

                    </div>
                </div>
            </Dialog>
        </div>
    );
}