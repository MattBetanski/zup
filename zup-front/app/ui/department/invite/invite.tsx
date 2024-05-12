'use client';

import { InviteState, inviteToDepartment } from "@/app/lib/department/action";
import clsx from "clsx";
import { stat } from "fs";
import { useRouter } from "next/navigation";
import { useFormState } from "react-dom";
import { Button } from "../../button";

export default function Invite(props: {departmentId: number}) {
    console.log("Props =", props);
    const router = useRouter();

    function cancel() {
        router.back();
    }
    const initialState: InviteState = {message: null, errors: {}, status: null};
    const [state, dispatch] = useFormState(inviteToDepartment, initialState);

    return (
        <form action={dispatch}>
            <div className="rounded-md bg-surface-200 p-4 md:p-6">
                <input type="number" hidden={true}  name="departmentId" value={props.departmentId} readOnly/>
                {/* Email */}
                <div className="mb-4">
                    <label htmlFor="email" className="mb-2 block text-sm font-medium">
                        Email
                    </label>
                    <div className="relative">
                        <input id="email" name="email" type="text" placeholder="Enter email of user to invite" className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] text-sm outline-2 placeholder:text-gray-500 bg-surface-200" aria-describedby="email-error" />
                    </div>
                    <div id="email-error" aria-live="polite" aria-atomic="true">
                        {state.errors?.email && state.errors.email.map((error: string) => (
                            <p className="mt-2 text-sm text-red-500" key={error}>{error}</p>
                        ))}
                    </div>
                </div>
                <div id="error-message" aria-atomic="true">
                    {(state.message && state.status != null) && (
                        <p className={clsx("mt-2 text-sm", state.status == true && "text-green-500", "text-red-500")} key={state.message}>{state.message}</p>
                    )}
                </div>
            </div>
            <div className="mt-6 flex justify-end gap-4">
                <Button type="submit">Invite User</Button>
            </div>
        </form>
    );
}