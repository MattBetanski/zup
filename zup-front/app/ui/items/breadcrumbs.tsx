'use client';

import { Item } from "@/app/lib/definitions";
import { Breadcrumbs } from "@primer/react";

export function ItemDetailBreadcrumb({item}: {item: Item}) {
    return (
        <Breadcrumbs className="mb-2">
            <Breadcrumbs.Item href={`/dashboard`}>Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project`}>Project</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project/${item.projectId}`}>{item.projectId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project/${item.projectId}/items`}>Items</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project/${item.projectId}/items/${item.itemId}`} selected>{item.itemId}</Breadcrumbs.Item>
        </Breadcrumbs>
    );
}

export function EditBreadcrumb({item}: {item: Item}) {
    return (
        <Breadcrumbs className="mb-2">
            <Breadcrumbs.Item href={`/dashboard`}>Dashboard</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project`}>Project</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project/${item.projectId}`}>{item.projectId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project/${item.projectId}/items`}>Items</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project/${item.projectId}/items/${item.itemId}`}>{item.itemId}</Breadcrumbs.Item>
            <Breadcrumbs.Item href={`/dashboard/project/${item.projectId}/items/${item.itemId}/edit`} selected>Edit</Breadcrumbs.Item>
        </Breadcrumbs>

    )
}