import { fetchDepartmentsForUser } from "@/app/lib/action"
import { DeleteDepartment, UpdateDepartment } from "./buttons";

export default async function DepartmentTable() {
    const departments = await fetchDepartmentsForUser();
    return (
        <div className="mt-6 flow-root">
            <div className="inline-block min-w-full align-middle">
                <div className="rounded-lg bg-surface-200 text-white p-2 md:pt-0">
                    {/* Mobile Devices */}
                    <div className="md:hidden">
                        {departments?.map((department) => (
                            <div key={department.departmentId} className="mb-2 w-full rounded-md bg-surface-200 p-4">

                            </div>
                        ))}
                    </div>
                    <table className="hidden min-w-full text-white md:table">
                        <thead className="rounded-lg text-left text-sm font-normal">
                            <tr>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Name</th>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Description</th>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Creation Date</th>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Visibility</th>
                                <th scope="col" className="relative py-3 pl-6 pr-3"><span className="sr-only">Edit</span></th>
                            </tr>
                        </thead>
                        <tbody className="">
                            {departments?.map((department) => (
                                <tr key={department.departmentId} className="w-full border-b py-3 text-sm last-of-type:border-none [&:first-child>td:first-child]:rounded-tl-lg [&:first-child>td:last-child]:rounded-tr-lg [&:last-child>td:first-child]:rounded-bl-lg [&:last-child>td:last-child]:rounded-br-lg">
                                    <td className="whitespace-nowrap px-3 py-3"><p>{department.name}</p></td>
                                    <td className="whitespace-nowrap px-3 py-3 overflow-hidden line-clamp-1 text-ellipsis w-64">{department.description}</td>
                                    <td className="whitespace-nowrap px-3 py-3">{department.creationDate.toString()}</td>
                                    <td className="whitespace-nowrap px-3 py-3">Public</td>
                                    <td className="whitespace-nowrap py-3 pl-6 pr-3">
                                        <div className="flex justify-end gap-3">
                                            <UpdateDepartment id={department.departmentId} />
                                            <DeleteDepartment id={department.departmentId}/>
                                        </div>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}