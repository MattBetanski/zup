import { lusitana } from "../../ui/fonts";
import { fetchCardData } from "../../lib/data";
import RevenueChart from "../../ui/dashboard/revenue-chart";
import LatestInvoices from "../../ui/dashboard/latest-invoices";
import { Card } from "../../ui/dashboard/cards";
import { Suspense } from "react";
import { LatestInvoicesSkeleton, RevenueChartSkeleton, CardsSkeleton } from "@/app/ui/skeletons";
import CardWrapper from "../../ui/dashboard/cards";
import getServerSession from 'next-auth';
import { checkAuthenticated, getToken } from "@/app/lib/action";
import DepartmentWrapper from "@/app/ui/dashboard/departments";
import { Button } from "@/app/ui/button";
import { checkIfAuthenticated } from "@/app/lib/authentication/action";
import { getDepartmentsForUser } from "@/app/lib/department/action";
import Link from "next/link";
export default async function Page() {
    let token = await checkAuthenticated();
    const departments = await getDepartmentsForUser();
    return (
        <main className="h-full">
            <h1 className={`${lusitana.className} mb-4 text-xl md:text-2xl`}>
                Dashboard
            </h1>
            <h2 className={`${lusitana.className} mb-4 text-md md:text-xl`}>
                Welcome back, {token.unique_name}!
            </h2>
            <div className="overflow-hidden h-[80%]">
                <Suspense fallback={<CardsSkeleton />}>
                    {departments ? (
                        departments?.length > 0 ? (
                            <DepartmentWrapper  departments={departments}/>
                        ) : (
                            <p>No departments</p>
                        )
                    ): (
                        <p>Failed to fetch departments</p>
                    )}
                </Suspense>
            </div>
            <div className="justify-end flex flex-row">
                <Link href={`/dashboard/department/create`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Create Department</Link>
            </div>
        </main>
    );
}