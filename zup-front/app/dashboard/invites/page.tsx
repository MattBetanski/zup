'use server';
import { getInvitesForUser } from "@/app/lib/invite/action";
import InviteTable from "@/app/ui/invites/table";

export default async function Page() {
    const invites = await getInvitesForUser();
    return (
        <main>
            <InviteTable invites={invites}/>
        </main>
    );
}