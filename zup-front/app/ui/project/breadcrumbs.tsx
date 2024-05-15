'use client';
import { Breadcrumbs } from "@primer/react";

export function ProjectItemBreadcrumb({projectId}: {projectId: number}) {
    return (
        <Breadcrumbs>
            <Breadcrumbs.Item href="/dashboard">Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href="/dashboard/project">Project</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project/${projectId}`}>{projectId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`#`} selected>Items</Breadcrumbs.Item>
        </Breadcrumbs>
    );
}