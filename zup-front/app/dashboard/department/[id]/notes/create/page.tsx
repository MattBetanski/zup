'use server';

import { CreateNoteBreadcrumb } from "@/app/ui/note/breadcrumbs";
import Form from "@/app/ui/note/create";

export default async function Page({params}: {params: {id: number}}) {
    return (
        <main>
            <CreateNoteBreadcrumb departmentId={params.id}/>
            <Form departmentId={params.id}/>
        </main>
    );
}