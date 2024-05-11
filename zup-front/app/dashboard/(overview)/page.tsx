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
import { fetchDepartmentsForUser } from '@/app/lib/action';
import { Button } from "@/app/ui/button";
import { checkIfAuthenticated } from "@/app/lib/authentication/action";
import { getDepartmentsForUser } from "@/app/lib/department/action";
export default async function Page() {
    let token = await checkAuthenticated();
    const departments = await getDepartmentsForUser();
    return (
        <main>
            <h1 className={`${lusitana.className} mb-4 text-xl md:text-2xl`}>
                Dashboard
            </h1>
            <h2 className={`${lusitana.className} mb-4 text-md md:text-xl`}>
                Welcome back, {token.unique_name}!
            </h2>
            <div className="overflow-hidden">
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
            <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-4">
                <Suspense fallback={<CardsSkeleton />}>
                    <CardWrapper />
                </Suspense>
            </div>
            <div className="mt-6 grid grid-cols-1 gap-6 md:grid-cols-4 lg:grid-cols-8">
                <Suspense fallback={<RevenueChartSkeleton />}>
                    <RevenueChart />
                </Suspense>
                <Suspense fallback={<LatestInvoicesSkeleton />}>
                    <LatestInvoices />
                </Suspense>
            </div>
        </main>
    );
}