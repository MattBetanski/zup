//TOOD: Page for editing role for department NOT FINISHED
'use client';
import { EyeIcon, EyeSlashIcon } from "@heroicons/react/24/solid";
import { stat } from "fs";
import { useState } from "react";
import { useFormState } from 'react-dom';
import { useRouter } from "next/navigation";
import { Button } from "@/app/ui/button";
import {PermissionLevel, Role} from '@/app/lib/definitions';
import EditRoleForm from "@/app/ui/roles/edit";

export default function Page({params}: {params: {id: number, roleId: number}}) {
    let role_obj: Role = {
        roleId: 0,
        departmentId: 0,
        name: "Test Role",
        description: "Test Description",
        wikiDelete: true,
        wikiLevel: PermissionLevel.Modify,
        itemLevel: PermissionLevel.Read
    };
   return (
        <EditRoleForm role={role_obj}/>
   )
}