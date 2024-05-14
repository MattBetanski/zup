'use client'

import { acceptInvite, declineInvite } from "@/app/lib/invite/action"
import { CheckIcon, XIcon } from "@primer/octicons-react"

export function AcceptInvite({departmentId}: {departmentId: number}) {
    return (
        <div>
            <form action={acceptInvite}>
                <input hidden name="departmentId" value={departmentId} readOnly />
                <button type="submit" className="rounded-md border p-2 hover:bg-surface-300">
                    <span className="sr-only">Accept</span>
                    <CheckIcon className="w-5"/>
                </button>
            </form>
        </div>
    )
}

export function DeclineInvite({departmentId}: {departmentId: number}) {
    return (
        <div>
            <form action={declineInvite}>
                <input hidden name="departmentId" value={departmentId} readOnly />
                <button type="submit" className="rounded-md border p-2 hover:bg-surface-300">
                    <span className="sr-only">Decline</span>
                    <XIcon className="w-5"/>
                </button>
            </form>
        </div>
    )
}