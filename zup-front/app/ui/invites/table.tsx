'use client'

import { UserInvitations } from "@/app/lib/definitions";
import { AcceptInvite, DeclineInvite } from "./buttons";

export default function InviteTable({invites}: {invites: UserInvitations[]}) {
    return (
        <main>
            {invites.length > 0 ? 
                <div className="w-full">
                    <div className="inline-block min-w-full align-middle">
                        <div className="rounded-lg bg-surface-200 text-white p-2 md:pt-0">
                            <table className="hidden min-w-full text-white md:table">
                                <thead className="rounded-lg text-let text-sm font-normal">
                                    <tr>
                                        <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Department Name</th>
                                        <th scope="col" className="px-4 py-5 font-medium sm:pl-6"><span className="sr-only">Edit</span></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {invites.map((invite) => (
                                        <tr key={invite.departmentId} className="w-full border-b py-3 text-sm last-of-type:border-none [&:first-child>td:first-child]:rounded-tl-lg [&:first-child>td:last-child]:rounded-tr-lg [&:last-child>td:first-child]:rounded-bl-lg [&:last-child>td:last-child]:rounded-br-lg">
                                            <td className="whitespace-nowrap px-3 py-3"><p>{invite.name}</p></td>
                                            <td className="whitespace-nowrap px-3 pl-6 pr-3">
                                                <div className="flex justify-end gap-3">
                                                    <AcceptInvite departmentId={invite.departmentId}/>
                                                    <DeclineInvite departmentId={invite.departmentId}/>
                                                </div>
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                : <p>No department invitations</p>
            }
        </main>
    )
}