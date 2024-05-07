'use client';
import { fetchDepartmentsForUser } from "@/app/lib/action";
import { Department } from "@/app/lib/definitions";
import { lusitana } from "../fonts";
import { useEffect } from "react";
import Glide from '@glidejs/glide';
import { Button } from "../button";
import Link from "next/link";
import { useSearchParams, useRouter, usePathname, redirect } from "next/navigation";

export default function DepartmentWrapper({departments}: {departments: Department[]}) {
    const router = useRouter();
    const searchParams = useSearchParams();
    const pathname = usePathname() ?? '/dashboard';
    function createDepartment() {
        const params = new URLSearchParams(searchParams);
        params.set("previousName", "Dashboard");
        params.set("previousUrl", pathname);
        console.log(params);
        router.push(`/dashboard/department/create?${params.toString()}`)
    }
    useEffect(() => {
        const slider = new Glide(".glide-01", {
        type: "carousel",
        focusAt: "center",
        perView: 3,
        animationDuration: 700,
        gap: 24,
        classNames: {
            nav: {
            active: "[&>*]:bg-wuiSlate-700",
            },
        },
        breakpoints: {
            1024: {
            perView: 2,
            },
            640: {
            perView: 1,
            },
        },
    }).mount()

    return () => {
      slider.destroy()
    }
  }, [])
    return <>
        <div className="glide-01 relative-w-full h-64">
            <div className="h-32 overflow-hidden" data-glide-el="track">
                <ul className="h-full whitespace-no-wrap flex-no-wrap [backface-visibility: hidden] [transform-style: preserve-3d] [touch-action: pan-Y] [will-change: transform] relative flex w-full overflow-hidden p-0">
                    {departments.map(x => 
                        <li key={x.departmentId}>
                            <DepartmentCard title={x.name} description={x.description} id={x.departmentId.toString()} key={x.departmentId}/>
                        </li>
                    )}
                </ul>
            </div>
            <div
                className="relative flex h-16 w-full justify-between px-4 mt-4"
            >
                <div data-glide-el="controls">
                    <button
                        className="inline-flex h-8 w-8 items-center justify-center rounded-l-lg border border-black bg-surface-400 text-black transition duration-300 focus-visible:outline-none lg:h-12 lg:w-12"
                        data-glide-dir="<"
                        aria-label="prev slide"
                    >
                        <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth="1.5"
                        stroke="currentColor"
                        className="h-5 w-5"
                        >
                        <title>prev slide</title>
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M6.75 15.75L3 12m0 0l3.75-3.75M3 12h18"
                        />
                        </svg>
                    </button>
                    <button
                        className="inline-flex h-8 w-8 items-center justify-center rounded-r-lg border border-black bg-surface-400 text-black transition duration-300 focus-visible:outline-none lg:h-12 lg:w-12"
                        data-glide-dir=">"
                        aria-label="next slide"
                    >
                        <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth="1.5"
                        stroke="currentColor"
                        className="h-5 w-5"
                        >
                        <title>next slide</title>
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M17.25 8.25L21 12m0 0l-3.75 3.75M21 12H3"
                        />
                        </svg>
                    </button>

                </div>
                <Button className="absoute right-0 bg-primary-300 hover:bg-primary-200 active:bg-primary-100" onClick={createDepartment}>Create Department</Button>
            </div>
        </div>
    </>
}

export function DepartmentCard({
    title,
    description,
    id,
}: {
    title: string;
    description: string;
    id: string;
}) {
    return (
        <Link href={`/dashboard/department/${id}`}>
            <div className="rounded-xl bg-surface-200 p-2 shadow-sm h-full hover:bg-surface-300">
                <div className={`${lusitana.className} text-3xl italic`}>
                    {title}
                </div>
                <div className="text-sm">
                    {description}
                </div>
            </div>
        </Link>
    );
}