'use client';

import { downvoteNote, upvoteNote, VoteNoteState } from "@/app/lib/note/action";
import { ArrowDownIcon, ArrowUpIcon } from "@primer/octicons-react";
import { useFormState } from "react-dom";

export function UpvoteNote({noteId, departmentId}: {noteId: number, departmentId: number}) {
    const initialState: VoteNoteState = {errors: {}, message: null};
    const [state, dispatch] = useFormState(upvoteNote, initialState);
    return (
        <form action={dispatch}>
            <input hidden name="noteId" value={noteId} readOnly />
            <input hidden name="departmentId" value={departmentId} readOnly />
            <button type="submit" className="rounded-md border p-2 hover:bg-surface-300">
                <ArrowUpIcon />
            </button>
        </form>
    );
}

export function DownvoteNote({noteId, departmentId}: {noteId: number, departmentId: number}) {
    const initialState: VoteNoteState = {errors: {}, message: null};
    const [state, dispatch] = useFormState(downvoteNote, initialState);
    return (
        <form action={dispatch}>
            <input hidden name="noteId" value={noteId} readOnly />
            <input hidden name="departmentId" value={departmentId} readOnly />
            <button type="submit" className="rounded-md border p-2 hover:bg-surface-300">
                <ArrowDownIcon />
            </button>
        </form>
    );
}