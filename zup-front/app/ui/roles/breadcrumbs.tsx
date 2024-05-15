'use client';

import { Breadcrumbs } from "@primer/react";

export async function DepartmentRoleListingBreadcrumb({departmentId}: {departmentId: number}) {
    return (
        <Breadcrumbs>
            <Breadcrumbs.Item href={`/dashboard`}>Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department`}>Department</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}`}>{departmentId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}/roles`} selected>Roles</Breadcrumbs.Item>
        </Breadcrumbs>
    )
}