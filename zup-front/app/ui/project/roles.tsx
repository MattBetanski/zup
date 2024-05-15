'use client';

import { Role, User, UserRole } from "@/app/lib/definitions";
import { setRole } from "@/app/lib/roles/action";
import { Breadcrumbs } from "@primer/react";
import React from "react";

//TODO: Make table to store roles
export default function RoleAssignmentTable({roles, users, projectId}: {roles: Role[], users: UserRole[], projectId: number}) {
    function RoleSelector({user}: {user: UserRole}) {
        const updateRole = (event: React.ChangeEvent<HTMLSelectElement>) => {
            setRole(Number.parseInt(event.target.value), user.userId, projectId);
        }
        return (
            <select className="peer block w-full cursor-pointer rounded-lg border border-gray-500 py-2 text-sm outline-2 placeholder:text-gray-500 text-white bg-surface-200"
                onChange={updateRole}
            >
                <option value={"no role"} selected={user.roleId == 0}>No Role</option>
                <option disabled>------</option>
                {roles.map((role) => (
                    <option key={role.roleId} value={role.roleId} selected={user.roleId == role.roleId}>{role.name}</option>
                ))}
            </select>
        )
    }
    return (
        <div className="mt-6 flow-root">
            <div className="inline-block min-w-full align-middle bg-surface-200">
                <div className="rounded-lg text-white p-2 md:pt-0">
                    <table className="min-w-full">
                        <thead className="rounded-lg text-left text-sm font-normal ">
                            <tr>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Name</th>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Role</th>
                            </tr>
                        </thead>
                        <tbody>
                            {users.map((user) => (
                                <tr key={user.userId} className="w-full border-b py-3 text-sm last-of-type:border-none [&:first-child>td:first-child]:rounded-tl-lg [&:first-child>td:last-child]:rounded-tr-lg [&:last-child>td:first-child]:rounded-bl-lg [&:last-child>td:last-child]:rounded-br-lg">
                                    <td className="whitespace-nowrap px-3 py-3">{user.firstName + " " + user.lastName}</td>
                                    <RoleSelector user={user}/>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
           </div>
        </div>
    );
}


export function RoleAssignmentBreadcrumb({projectId}: {projectId: number}) {
    return (
        <Breadcrumbs>
            <Breadcrumbs.Item href="/dashboard">Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href="/dashboard/project">Project</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project/${projectId}`}>{projectId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project/${projectId}/roles`} selected>roles</Breadcrumbs.Item>
        </Breadcrumbs>
    );
}