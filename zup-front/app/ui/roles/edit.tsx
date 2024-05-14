import { createRole, CreateRoleState, editRole, EditRoleState } from "@/app/lib/roles/action";
import { EyeIcon, EyeSlashIcon } from "@heroicons/react/24/solid";
import { useRouter } from "next/navigation";
import { useFormState } from "react-dom";
import { Button } from "../button";
import { PermissionLevel, Role } from "@/app/lib/definitions";

export default function EditRoleForm({role}: {role: Role}){
    const router = useRouter();

    function cancel() {
        router.back();
    }
  const initialState: EditRoleState = { message: null, errors: {}};
  const [state, dispatch] = useFormState(editRole, initialState);
    return (
        <form action={dispatch}>
            <input hidden type="number" defaultValue={role.departmentId} name="departmentId" readOnly/>
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
                        defaultValue={role.name}
                        placeholder="Enter role name"
                        className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                        aria-describedby='amount-error'
                    />
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
                        defaultValue={role.description}
                        placeholder="Enter Description"
                        className="peer block w-full rounded-r-3xl border border-gray-500 rounded-l-lg py-[9px] text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                        aria-describedby='amount-error'
                    />
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

                {/* Item level */}
                <fieldset className='mb-4'>
                <legend className="mb-2 block text-sm font-medium">
                    Item Permissions
                </legend>
                <div className="rounded-r-3xl rounded-l-lg border border-gray-500 bg-surface-200 px-[14px] py-3">
                    <div className="flex gap-4">
                    <div className="flex items-center">
                        <input
                        id="noAccess"
                        name="itemLevel"
                        type="radio"
                        value="NoAccess"
                        className="h-4 w-4 cursor-pointer border-gray-300 bg-gray-100 text-gray-600 focus:ring-2"
                        aria-describedby='status-error'
                        checked={role.itemLevel == PermissionLevel.NoAccess}
                        />
                        <label
                        htmlFor="noAccess"
                        className="ml-2 flex cursor-pointer items-center gap-1.5 rounded-full bg-gray-100 px-3 py-1.5 text-xs font-medium text-gray-600"
                        >
                        No Access <EyeSlashIcon className="h-4 w-4" />
                        </label>
                    </div>
                    <div className="flex items-center">
                        <input
                        id="readAccess"
                        name="itemLevel"
                        type="radio"
                        value="Read"
                        className="h-4 w-4 cursor-pointer border-gray-300 bg-gray-100 text-gray-600 focus:ring-2"
                        checked={role.itemLevel == PermissionLevel.Read}
                        />
                        <label
                        htmlFor="readAccess"
                        className="ml-2 flex cursor-pointer items-center gap-1.5 rounded-full bg-blue-500 px-3 py-1.5 text-xs font-medium text-white"
                        >
                        Read Access <EyeIcon className="h-4 w-4" />
                        </label>
                    </div>
                    <div className="flex items-center">
                        <input
                        id="writeAccess"
                        name="itemLevel"
                        type="radio"
                        value="Modify"
                        className="h-4 w-4 cursor-pointer border-gray-300 bg-gray-100 text-gray-600 focus:ring-2"
                        checked={role.itemLevel == PermissionLevel.Modify}
                        />
                        <label
                        htmlFor="writeAccess"
                        className="ml-2 flex cursor-pointer items-center gap-1.5 rounded-full bg-green-500 px-3 py-1.5 text-xs font-medium text-white"
                        >
                        Modify <EyeIcon className="h-4 w-4" />
                        </label>
                    </div>
                    </div>
                </div>
                <div id="item-error" aria-polite="true" aria-atomic="true">
                    {state.errors?.itemLevel &&
                        state.errors.itemLevel.map((error: string) => (
                        <p className='mt-2 text-sm text-red-500' key={error}>
                            {error}
                        </p>
                        ))}
                </div>
                </fieldset>
                {/* Wiki level */}
                <fieldset className='mb-4'>
                <legend className="mb-2 block text-sm font-medium">
                    Wiki Permissions
                </legend>
                <div className="rounded-r-3xl rounded-l-lg border border-gray-500 bg-surface-200 px-[14px] py-3">
                    <div className="flex gap-4">
                    <div className="flex items-center">
                        <input
                        id="wikiNoAccess"
                        name="wikiLevel"
                        type="radio"
                        value="NoAccess"
                        className="h-4 w-4 cursor-pointer border-gray-300 bg-gray-100 text-gray-600 focus:ring-2"
                        aria-describedby='status-error'
                        checked={role.wikiLevel == PermissionLevel.NoAccess}
                        />
                        <label
                        htmlFor="wikiNoAccess"
                        className="ml-2 flex cursor-pointer items-center gap-1.5 rounded-full bg-gray-100 px-3 py-1.5 text-xs font-medium text-gray-600"
                        >
                        No Access <EyeSlashIcon className="h-4 w-4" />
                        </label>
                    </div>
                    <div className="flex items-center">
                        <input
                        id="wikiReadAccess"
                        name="wikiLevel"
                        type="radio"
                        value="Read"
                        className="h-4 w-4 cursor-pointer border-gray-300 bg-gray-100 text-gray-600 focus:ring-2"
                        checked={role.wikiLevel == PermissionLevel.Read}
                        />
                        <label
                        htmlFor="wikiReadAccess"
                        className="ml-2 flex cursor-pointer items-center gap-1.5 rounded-full bg-blue-500 px-3 py-1.5 text-xs font-medium text-white"
                        >
                        Read Access <EyeIcon className="h-4 w-4" />
                        </label>
                    </div>
                    <div className="flex items-center">
                        <input
                        id="wikiWriteAccess"
                        name="wikiLevel"
                        type="radio"
                        value="Modify"
                        className="h-4 w-4 cursor-pointer border-gray-300 bg-gray-100 text-gray-600 focus:ring-2"
                        checked={role.wikiLevel == PermissionLevel.Modify}
                        />
                        <label
                        htmlFor="wikiWriteAccess"
                        className="ml-2 flex cursor-pointer items-center gap-1.5 rounded-full bg-green-500 px-3 py-1.5 text-xs font-medium text-white"
                        >
                        Modify <EyeIcon className="h-4 w-4" />
                        </label>
                    </div>
                    </div>
                </div>
                <div id="wiki-level-error" aria-polite="true" aria-atomic="true">
                    {state.errors?.wikiLevel &&
                        state.errors.wikiLevel.map((error: string) => (
                        <p className='mt-2 text-sm text-red-500' key={error}>
                            {error}
                        </p>
                        ))}
                </div>
                </fieldset>
                {/* Wiki Delete */}
                <div className="mb-4">
                <label htmlFor="wikiDelete" className="mb-2 block text-sm font-medium">
                    Wiki Delete
                </label>
                <div className="relative">
                    <input
                        id="wikiDelete"
                        name="wikiDelete"
                        type="checkbox"
                        defaultValue={"false"}
                        placeholder="Enter role name"
                        className="peer block w-4 h-4 rounded-lg border border-gray-500  py-[9px] pl-10 text-sm outline-2 placeholder:text-gray-500 bg-surface-200"
                        aria-describedby='amount-error'
                        checked={role.wikiDelete == true}
                    />
                </div>
                <div id="customer-error" aria-live="polite" aria-atomic="true">
                    {state.errors?.wikiDelete &&
                    state.errors.wikiDelete.map((error: string) => (
                        <p className='mt-2 text-sm text-red-500' key={error}>
                        {error}
                        </p>
                    ))}
                </div>
                </div>
                <div id="error-message" aria-atomic="true">
                {state.message && 
                    (
                    <p className="mt-2 text-sm text-red-500" key={state.message}>
                        {state.message}
                    </p>
                    )}
                </div>
            </div>
            <div className="mt-6 flex justify-end gap-4">
                <Button className='flex h-10 bg-surface-300 items-center rounded-lg px-4 text-sm font-medium hover:bg-surface-400' onClick={cancel}>Cancel</Button>
                <Button type="submit">Create Role</Button>
            </div>
        </form>
    );
}