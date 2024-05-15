'use server';
//TODO: Page for viewing all roles for department NOT TESTED
import Image from 'next/image';
import { UpdateInvoice, DeleteInvoice } from '@/app/ui/invoices/buttons';
import InvoiceStatus from '@/app/ui/invoices/status';
import { formatDateToLocal, formatCurrency } from '@/app/lib/utils';
import { fetchFilteredInvoices, getMembersList, getRolesList } from '@/app/lib/data';
import { string, number } from 'zod';
import { getRolesForDepartment } from '@/app/lib/roles/action';
import RolesTable from '@/app/ui/roles/listing';
import { DepartmentRoleListingBreadcrumb } from '@/app/ui/roles/breadcrumbs';
import Link from 'next/link';

export default async function Page({params}: {params: {id: number}}){
  const roles = await getRolesForDepartment(params.id);
  return (
    <main className='h-full'>
      <div className='flex flex-col h-full'>
        <div className='h-[90%]'>
          <DepartmentRoleListingBreadcrumb departmentId={params.id}/>
          <RolesTable roles={roles}/>
        </div>
        <div className='flex flex-row justify-end'>
            <Link href={`/dashboard/department/${params.id}/roles/create`} className="flex h-10 mr-5 items-center rounded-lg bg-blue-500 px-4 text-sm font-medium text-white transition-colors hover:bg-blue-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-500 active:bg-blue-600 aria-disabled:cursor-not-allowed aria-disabled:opacity-50">Create Role</Link>
        </div>
      </div>
    </main>
  );
}