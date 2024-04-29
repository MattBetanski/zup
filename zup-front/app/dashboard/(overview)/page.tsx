import { lusitana } from "../../ui/fonts";
import { fetchCardData } from "../../lib/data";
import RevenueChart from "../../ui/dashboard/revenue-chart";
import LatestInvoices from "../../ui/dashboard/latest-invoices";
import { Card } from "../../ui/dashboard/cards";
import { Suspense } from "react";
import { LatestInvoicesSkeleton, RevenueChartSkeleton, CardsSkeleton } from "@/app/ui/skeletons";
import CardWrapper from "../../ui/dashboard/cards";
import { useSession } from "next-auth/react";
export default async function Page() {
    const {totalPaidInvoices, totalPendingInvoices, numberOfCustomers, numberOfInvoices} = await fetchCardData();
    return (
        <main>
            <h1 className={`${lusitana.className} mb-4 text-xl md:text-2xl`}>
                Dashboard
            </h1>
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