'use client'
//TODO: Page for creating role for department NOT TESTED
import { useFormState } from 'react-dom';
import { CustomerField } from '@/app/lib/definitions';
import Link from 'next/link';
import {
  CheckIcon,
  ClockIcon,
  CurrencyDollarIcon,
  UserCircleIcon,
} from '@heroicons/react/24/outline';
import { Button } from '@/app/ui/button';
import { stat } from 'fs';
import { EyeIcon, EyeSlashIcon } from '@heroicons/react/24/solid';
import { useRouter, useSearchParams } from 'next/navigation';
import { createDepartment, CreateDepartmentState } from '@/app/lib/department/action';
import { createRole, CreateRoleState } from '@/app/lib/roles/action';
import CreateRole from '@/app/ui/roles/create';
import { Breadcrumbs } from '@primer/react';

export default function Page({params}: {params: {id: number}}) {
  return (
    <main>
      <Breadcrumbs>
        <Breadcrumbs.Item href={`/dashboard`}>Dashboard</Breadcrumbs.Item>
        <Breadcrumbs.Item href={`/dashboard/department`}>Department</Breadcrumbs.Item>
        <Breadcrumbs.Item href={`/dashboard/department/${params.id}`}>{params.id}</Breadcrumbs.Item>
        <Breadcrumbs.Item href={`/dashboard/department/${params.id}/roles`}>Roles</Breadcrumbs.Item>
        <Breadcrumbs.Item href={`/dashboard/department/${params.id}/roles/create`} selected>Create</Breadcrumbs.Item>
      </Breadcrumbs>
      <CreateRole departmentId={params.id}/>
    </main>
  );
}
