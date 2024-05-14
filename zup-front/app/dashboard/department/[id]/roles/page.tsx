//TODO: Page for viewing all roles for department NOT TESTED
import Image from 'next/image';
import { UpdateInvoice, DeleteInvoice } from '@/app/ui/invoices/buttons';
import InvoiceStatus from '@/app/ui/invoices/status';
import { formatDateToLocal, formatCurrency } from '@/app/lib/utils';
import { fetchFilteredInvoices, getMembersList, getRolesList } from '@/app/lib/data';
import { string, number } from 'zod';
import { getRolesForDepartment } from '@/app/lib/roles/action';
import RolesTable from '@/app/ui/roles/listing';

export default async function Page({params}: {params: {id: number}}){
  const roles = await getRolesForDepartment(params.id);
  return (
    <RolesTable roles={roles}/>
  );
}