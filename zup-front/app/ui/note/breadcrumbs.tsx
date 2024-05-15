'use client';

import { Breadcrumbs } from "@primer/react";


export function CreateNoteBreadcrumb({departmentId}: {departmentId: number}) {
    return (
        <Breadcrumbs>
            <Breadcrumbs.Item href={"/dashboard"}>Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href={"/dashboard/department"}>Department</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}`}>{departmentId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}/notes`}>Notes</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}/notes/create`} selected>Create</Breadcrumbs.Item>
        </Breadcrumbs>
    );
}

export function NoteListingBreadcrumb({departmentId}: {departmentId: number}) {
    return (
        <Breadcrumbs>
            <Breadcrumbs.Item href={"/dashboard"}>Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href={"/dashboard/department"}>Department</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}`}>{departmentId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}/notes`} selected>Notes</Breadcrumbs.Item>
        </Breadcrumbs>
    );
}