import { getDepartmentsForUser } from "@/app/lib/department/action";
import { DepartmentIdBreadcrumb, DepartmentListingBreadcrumb } from "@/app/ui/department/breadcrumbs";
import { CreateDepartment } from "@/app/ui/department/buttons";
import { DepartmentTableSkeleton } from "@/app/ui/department/suspend";
import DepartmentTable from "@/app/ui/department/table";
import { lusitana } from "@/app/ui/fonts";
import Search from "@/app/ui/search";
import { Suspense } from "react";

export default async function Page({searchParams}: {searchParams?: {query?: string; page?: string}}){
    const query = searchParams?.query || '';
    const currentPage = Number(searchParams?.page) || 1;
    const departments = await getDepartmentsForUser();
    return (
        <div className="w-full">
            <DepartmentListingBreadcrumb />
            <div className="flex w-full items-center justify-between">
                <h1 className={`${lusitana.className} text-white text-3xl`}>My Departments</h1>
            </div>
            <div className="mt-4 flex items-center justify-between gap-2 md:mt-8">
                <Search placeholder="Search Departments ..." />
                <CreateDepartment />
            </div>
            <Suspense key={query + currentPage} fallback={<DepartmentTableSkeleton />}>
                <DepartmentTable departments={departments}/>
            </Suspense>
        </div>
    )
}