'use client'
import { WikiListing } from "@/app/lib/definitions";
import { Agent } from "http";
import { useRouter } from "next/navigation";

export default function WikiTable({pages}: {pages: WikiListing[]}) {
    const router = useRouter();
    function TableRow({page}: {page: WikiListing}) {
        const openWiki = () => {
            router.push(`/dashboard/department/${page.departmentId}/wiki/${page.wikiId}`);
        }

        return (
            <tr className="w-full border-b py-3 text-sm last-of-type:border-none [&:first-child>td:first-child]:rounded-tl-lg [&:first-child>td:last-child]:rounded-tr-lg [&:last-child>td:first-child]:rounded-bl-lg [&:last-child>td:last-child]:rounded-br-lg hover:bg-surace-400"
                onDoubleClick={openWiki}
            >
                <td className="whitespace-nowrap px-3 py-3 select-none">{page.title}</td>
            </tr>
        );
    }

    return (
        <div className="mt-6 flow-root">
            <div className="inline-block min-w-full align-middle">
                <div className="rounded-lg bg-surface-200 text-white p-2 md:pt-0">
                    <table className="hidden min-w-full md:table">
                        <thead className="rounded-lg text-left text-sm font-normal">
                            <tr>
                                <th scope="col" className="px-4 py-5 font-medium sm:pl-6">Name</th>
                                <th scope="col" className="relative py-3 pl-6 pr-3"><span className="sr-only">Edit</span></th>
                            </tr>
                        </thead>
                        <tbody>
                            {pages.map((page) => (
                                <TableRow page={page} key={page.wikiPageId}/>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}