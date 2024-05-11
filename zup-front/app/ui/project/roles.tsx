//TODO: Make table to store roles
export default function RoleAssignmentTable() {
    return (
        <div className="mt-6 flow-root">
            <div className="inline-block min-w-full align-middle bg-surface-200">
                <div className="rounded-lg text-white p-2 md:pt-0">
                    <table className="min-w-full">
                        <thead className="rounded-lg text-left text-sm font-normal ">
                            <tr>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Name</th>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Role</th>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Item Level</th>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Wiki Level</th>
                                <th scope="col" className="relative py-3 pl-6 pr-3"><span className="sr-only">Edit</span></th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
           </div>
        </div>
    );
}