'use client';
import Form from "@/app/ui/project/create";
import { Breadcrumbs } from "@primer/react";
import { notFound, useSearchParams, useRouter } from "next/navigation";

export default function Page() {
    const router = useRouter();
    const searchParams = useSearchParams();
    const departmentId = searchParams?.get("departmentId");
    console.log(departmentId);
    if (departmentId == null) {
        notFound();
    }

    return (
        <main>
            <Form departmentId={parseInt(departmentId)}/>
        </main>
    )
}