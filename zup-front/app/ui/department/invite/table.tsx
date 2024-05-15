'use client'

import { DepartmentInvitations } from "@/app/lib/definitions";
import { Button } from "../../button";
import { DeleteInvite } from "../buttons";

export default function InviteTable({invites}: {invites: DepartmentInvitations[]}) {
    return (
        <div className="w-full">
            <div className="inline-block min-w-full align-middle">
                <div className="rounded-lg bg-surface-200 text-white p-2 md:pt-0">
                    <table className="hidden min-w-full text-white md:table">
                        <thead className="rounded-lg text-left text-sm font-normal">
                            <tr>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6" >Email</th>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6"><span className="sr-only">Edit</span></th>
                            </tr>
                        </thead>
                        <tbody>
                            {invites.map((invite) => (
                                <tr key={invite.inviteeId} className="w-full border-b py-3 text-sm last-of-type:border-none [&:first-child>td:first-child]:rounded-tl-lg [&:first-child>td:last-child]:rounded-tr-lg [&:last-child>td:first-child]:rounded-bl-lg [&:last-child>td:last-child]:rounded-br-lg">
                                    <td className="whitespace-nowrap px-3 py-3"><p>{invite.email}</p></td>
                                    <td className="whitespace-nowrap py-3 pl-6 pr-3">
                                        <div className="flex justify-end gap-3">
                                            <DeleteInvite inviteeId={invite.inviteeId} departmentId={invite.departmentId}/>
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