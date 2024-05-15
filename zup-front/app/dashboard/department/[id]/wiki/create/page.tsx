'use client'
import "@uiw/react-md-editor/markdown-editor.css";
import "@uiw/react-markdown-preview/markdown.css";
import dynamic from "next/dynamic";
import { useState } from "react";

import { lusitana } from "@/app/ui/fonts";
import Link from "next/link";
import { Button } from "@/app/ui/button";
import { Router } from "next/router";
import { useRouter } from "next/navigation";
import { createWikiPage, CreateWikiState } from "@/app/lib/wiki/action";
import { useFormState } from "react-dom";
import CreateWiki from "@/app/ui/wiki/edit";
import { WikiCreateBreadcrumb } from "@/app/ui/wiki/breadcrumbs";
const MDEditor = dynamic(
  () => import("@uiw/react-md-editor"),
  { ssr: false }
);

export default function Page({params}: {params: {id: number}}) {

    return (
        <main data-color-mode="dark" className="h-full">
            <WikiCreateBreadcrumb departmentId={params.id}/>
            <h1 className={`${lusitana.className} text-3xl`}>Create Wiki Page</h1>
            <hr className="mb-3"/>
            <CreateWiki id={params.id}/>
        </main>
    );
}