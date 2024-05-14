'use client';
import { Breadcrumbs } from "@primer/react";

export function DepartmentIdBreadcrumb({departmentId}:{departmentId: number}) {
    return (
        <Breadcrumbs>
            <Breadcrumbs.Item href="/dashboard">Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href="/dashboard/department">Department</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}`} selected>{departmentId}</Breadcrumbs.Item>
        </Breadcrumbs>
    )
}

export function DepartmentListingBreadcrumb() {
    return (
        <Breadcrumbs>
            <Breadcrumbs.Item href="/dashboard">Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href="/dashboard/department" selected>Department</Breadcrumbs.Item>
        </Breadcrumbs>
    )
}