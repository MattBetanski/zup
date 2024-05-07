'use client';

import { UserCircleIcon } from '@heroicons/react/20/solid';
import {TreeView} from '@primer/react';
import { PackageIcon } from '@primer/octicons-react';
export default function Page() {
    return (
        <main>
            <TreeView>
                <TreeView.Item id="src" defaultExpanded className='text-blue-500'>
                    <TreeView.LeadingVisual>
                        <PackageIcon />
                    </TreeView.LeadingVisual>
                    src
                    <TreeView.SubTree>

                    </TreeView.SubTree>
                </TreeView.Item>
            </TreeView>
        </main>
    )
}