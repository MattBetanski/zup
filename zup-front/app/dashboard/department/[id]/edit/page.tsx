'use client'
import { fetchDepartmentbyId } from "@/app/lib/data";
import EditDepartmentForm from "@/app/ui/department/edit";
import { Box, Breadcrumbs, Button } from "@primer/react";
import { PageHeader } from "@primer/react/drafts";

export default async function Page({params}: {params: {id: string}}) {
    const id = params.id;
    const department = fetchDepartmentbyId(id);
    return (
        <Box>
            <Breadcrumbs>
                <Breadcrumbs.Item href="/dashboard">Dashboard</Breadcrumbs.Item>
                <Breadcrumbs.Item href="/dashboard/department">Departments</Breadcrumbs.Item>
                <Breadcrumbs.Item href={`/dashboard/department/${id}`} selected>Edit</Breadcrumbs.Item>
            </Breadcrumbs>
            <PageHeader>
                <PageHeader.TitleArea>
                    <PageHeader.Title>Edit Department</PageHeader.Title>
                </PageHeader.TitleArea>
            </PageHeader>
            <EditDepartmentForm params={{id: id}}/>
        </Box>
    )
}