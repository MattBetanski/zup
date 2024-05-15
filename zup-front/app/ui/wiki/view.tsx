'use client';

import Markdown from 'react-markdown'
import MarkdownPreview from '@uiw/react-markdown-preview';
export default function ViewWiki({contents}: {contents: string}) {
    const value = `
    # test heading
    `;
    return (
        //<MarkdownPreview className={"prose min-w-full h-[80%] rounded-lg bg-white"} source={contents} />
        <Markdown className={"prose min-w-full mx-auto bg-white h-[80%] rounded-lg p-3" }>{contents}</Markdown>
    )
}