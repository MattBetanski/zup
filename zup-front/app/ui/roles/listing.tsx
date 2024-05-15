'use client';

import { Role } from "@/app/lib/definitions";
import { DeleteRole, EditRole } from "./buttons";

export default function RolesTable({roles}: {roles: Role[]}){
  return (
    <div className="mt-6 flow-root">
        <div className="inline-block min-w-full align-middle">
            <div className="rounded-lg bg-surface-200 text-white p-2 md:pt-0">
                <table className="hidden min-w-full text-white md:table">
                    <thead className="rounded-lg text-left text-sm font-normal">
                        <tr>
                            <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Name</th>
                            <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Description</th>
                            <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Item Level</th>
                            <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Wiki Level</th>
                            <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Wiki Delete</th>
                            <th scope="col" className="relative py-3 pl-6 pr-3"><span className="sr-only">Edit</span></th>
                        </tr>
                    </thead>
                    <tbody className="">
                        {roles?.map((role) => (
                            <tr key={role.roleId} className="w-full border-b py-3 text-sm last-of-type:border-none [&:first-child>td:first-child]:rounded-tl-lg [&:first-child>td:last-child]:rounded-tr-lg [&:last-child>td:first-child]:rounded-bl-lg [&:last-child>td:last-child]:rounded-br-lg">
                                <td className="whitespace-nowrap px-3 py-3"><p>{role.name}</p></td>
                                <td className="whitespace-nowrap px-3 py-3 overflow-hidden line-clamp-1 text-ellipsis w-64">{role.description}</td>
                                <td className="whitespace-nowrap px-3 py-3">{role.itemLevel.toString()}</td>
                                <td className="whitespace-nowrap px-3 py-3">{role.wikiLevel.toString()}</td>
                                <td className="whitespace-nowrap px-3 py-3">{role.wikiDelete.toString()}</td>
                                <td className="whitespace-nowrap py-3 pl-6 pr-3">
                                    <div className="flex justify-end gap-3">
                                        <EditRole departmentId={role.departmentId} roleId={role.roleId}/>
                                        <DeleteRole departmentId={role.departmentId} roleId={role.roleId}/>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    </div>
  );
}