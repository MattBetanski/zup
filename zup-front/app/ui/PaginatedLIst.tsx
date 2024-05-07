'use client';
import { Stack } from "@primer/react/experimental";
import { lusitana } from "./fonts";
import { useState } from "react";
import { ArrowLeftIcon, ArrowRightIcon } from "@primer/octicons-react";
import clsx from "clsx";
import Link from "next/link";

export default function PaginatedList({ itemsPerPage, items}: {itemsPerPage: number, items: any[]}) {
    const [currentPage, setCurrentPage] = useState(1);

    function goToProject(id: string) {
        console.log(id);
    }
    // Calculate the index range for the current page
    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;

    // Slice the items array to get the items for the current page
    const currentItems = items.slice(startIndex, endIndex);
    console.log(currentItems)
    // Function to handle pagination navigation
    const goToPage = (pageNumber: number) => {
        setCurrentPage(pageNumber);
    };
    return (
        <div>
            <div className="">
                <Stack direction="vertical">
                    {currentItems.map(x => <Stack.Item>
                        <Link href={`/dashboard/projects/${x.projectId}`}>
                            <div className="bg-surface-300 rounded-lg h-48 p-5 border-2 border-surface-400 shadow-lg shadow-surface-200 overflow-hidden line-clamp-2 hover:bg-surface-400">
                                <h1 className={`${lusitana.className} text-3xl`}>{x.name}</h1>
                                <hr className="bg-surface-300"></hr>
                                <p className="line-clamp-2 mt-5">{x.description}</p>
                            </div>
                        </Link>
                    </Stack.Item>)}
                </Stack>
            </div>
            <div>
                {/* Previous page button */}
                <button
                    className={clsx("rounded-l-lg h-8 mt-5 w-8 border-r-black", {
                        'bg-surface-300': currentPage === 1,
                        'bg-white hover:bg-gray-300': currentPage != 1
                    })}
                    onClick={() => goToPage(currentPage - 1)}
                    disabled={currentPage === 1}
                >
                    <ArrowLeftIcon className={clsx("", {
                        'text-white': currentPage === 1,
                        'text-black': currentPage != 1
                    })}/>
                </button>

                {/* Page numbers */}
                {Array.from({ length: Math.ceil(items.length / itemsPerPage) }).map(
                (page, index) => (
                    <button
                        key={index}
                        onClick={() => goToPage(index + 1)}
                        disabled={currentPage === index + 1}
                        className={clsx("pl-3 pr-3 h-8",{
                            'text-white bg-surface-300': currentPage === index + 1,
                            'text-black bg-white hover:bg-gray-300': currentPage != index + 1
                        })}
                    >
                    {index + 1}
                    </button>
                )
                )}

                {/* Next page button */}
                <button
                    className={clsx("rounded-r-lg h-8 mt-5 w-8 border-l-black",{
                        'bg-surface-300': currentPage === Math.ceil(items.length / itemsPerPage),
                        'bg-white hover:bg-gray-300': currentPage != Math.ceil(items.length / itemsPerPage)
                    })}
                    onClick={() => goToPage(currentPage + 1)}
                    disabled={currentPage === Math.ceil(items.length / itemsPerPage)}
                >
                    <ArrowRightIcon className={clsx("", {
                        'text-white': currentPage === Math.ceil(items.length / itemsPerPage),
                        'text-black': currentPage != Math.ceil(items.length / itemsPerPage)
                    })}/>
                </button>
            </div>
        </div>
    )
}
