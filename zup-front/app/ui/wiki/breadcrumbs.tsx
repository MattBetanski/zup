'use client'

import { Breadcrumbs } from "@primer/react";

export function WikiCreateBreadcrumb({departmentId}: {departmentId: number}) {
    return (
        <Breadcrumbs>
            <Breadcrumbs.Item href="/dashboard">Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href="/dashboard/department">Department</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}`}>{departmentId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}/wiki`}>Wiki</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}/wiki/create`} selected>Create</Breadcrumbs.Item>
        </Breadcrumbs>
    );
}

export function WikiTableBreadcrumb({departmentId}: {departmentId: number}) {
    return (
        <Breadcrumbs>
            <Breadcrumbs.Item href="/dashboard">Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href="/dashboard/department">Department</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}`}>{departmentId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}/wiki`} selected>Wiki</Breadcrumbs.Item>
        </Breadcrumbs>
    );
}

export function WikiPageBreadcrumb({departmentId, wikiPageId}: {departmentId: number, wikiPageId: number}) {
    return (
        <Breadcrumbs>
            <Breadcrumbs.Item href="/dashboard">Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href="/dashboard/department">Department</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}`}>{departmentId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}/wiki`} >Wiki</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/department/${departmentId}/wiki/${wikiPageId}`} selected>{wikiPageId}</Breadcrumbs.Item>
        </Breadcrumbs>
    );
}