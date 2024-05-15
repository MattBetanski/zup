'use client'
import Form from "@/app/ui/department/create";
import { lusitana } from "@/app/ui/fonts";
import { Breadcrumb, Breadcrumbs } from "@primer/react";
import { usePathname, useRouter, useSearchParams } from "next/navigation";

export default function Page() {
    const router = useRouter();
    const searchParams = useSearchParams();
    const previous = searchParams?.get('previousName') ?? 'Dashboard';
    const previousUrl = searchParams?.get("previousUrl") ?? '/dashboard';
    console.log(previous, previousUrl);
    console.log(previous);

    return (
        <main>
            <Breadcrumbs>
                <Breadcrumbs.Item href={previousUrl}>{previous}</Breadcrumbs.Item>
                <Breadcrumbs.Item href="#" selected>Create</Breadcrumbs.Item>
            </Breadcrumbs>
            <Form />
        </main>
    )
}