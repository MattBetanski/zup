'use client'

import { lusitana } from "@/app/ui/fonts"
import Form from "@/app/ui/items/create"

export default function Page() {
return (
    <main>
        <h1 className={`${lusitana.className} text-3xl`}>Create Item</h1>
        <hr />
        <div className="mt-5">
            <Form />
        </div>
    </main>
)
}