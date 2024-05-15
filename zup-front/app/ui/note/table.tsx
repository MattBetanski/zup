import { Note } from "@/app/lib/definitions";
import { DownvoteNote, UpvoteNote } from "./buttons";

export default function NoteTable({notes}: {notes: Note[]}) {
    function TableRow({note}: {note: Note}) {
        return (
            <tr key={note.noteId} className="w-full border-b py-3 text-sm last-of-type:border-none [&:first-child>td:first-child]:rounded-tl-lg [&:first-child>td:last-child]:rounded-tr-lg [&:last-child>td:first-child]:rounded-bl-lg [&:last-child>td:last-child]:rounded-br-lg hover:bg-surace-400">
                <td className="whitespace-nowrap px-3 py-3">{note.title}</td>
                <td className="whitespace-nowrap px-3 py-3">{note.content}</td>
                <td className="whitespace-nowrap px-3 py-3">{note.likeCount}</td>
                <td className="whitespace-nowrap py-3 pl-6 pr-3">
                    <div className="flex justify-end gap-3">
                        <UpvoteNote noteId={note.noteId} departmentId={note.departmentId}/>
                        <DownvoteNote noteId={note.noteId} departmentId={note.departmentId}/>
                    </div>
                </td>
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
                                <th scope="col" className="px-4 py-5 font-medium sm:pt-6">Name</th>
                                <th scope="col" className="px-4 py-5 font-medium sm:pt-6">Content</th>
                                <th scope="col" className="px-4 py-5 font-medium sm:pt-6">Rating</th>
                                <th scope="col" className="relative py-3 pl-6 pr-3"><span className="sr-only">Edit</span></th>
                            </tr>
                        </thead>
                        <tbody>
                            {notes.map((note) => (
                                <TableRow note={note} />
                            ))}
                        </tbody>
                   </table>
                </div>
            </div>
        </div>
    );
}