import Link from 'next/link';
import NavLinks from '@/app/ui/dashboard/nav-links';
import ZupLogo from '@/app/ui/zup-logo';
import { PowerIcon } from '@heroicons/react/24/outline';
import { cookies } from 'next/headers';
import { redirect } from 'next/navigation';
export default function SideNav() {
  return (
    <div className="flex h-full flex-col px-3 py-4 md:px-2 bg-surface-200">
      <Link
        className="mb-2 flex h-20 items-end justify-start rounded-md bg-primary-400 p-4 md:h-40"
        href="/"
      >
        <div className="w-32 text-white md:w-40">
          <ZupLogo />
        </div>
      </Link>
      <div className="flex grow flex-row justify-between space-x-2 md:flex-col md:space-x-0 md:space-y-2 bg-surface-200">
        <NavLinks />
        <div className="hidden h-auto w-full grow rounded-3xl bg-surface-200 md:block"></div>
        <form
          action={async () => {
            'use server';
            cookies().delete("token");
            redirect("/login");
          }}
        >
          <button className="flex h-[48px] w-full grow items-center justify-center gap-2 rounded-md bg-surface-300 p-3 text-sm font-medium hover:bg-surface-400 hover:text-primary-500 md:flex-none md:justify-start md:p-2 md:px-3">
            <PowerIcon className="w-6" />
            <div className="hidden md:block">Sign Out</div>
          </button>
        </form>
      </div>
    </div>
  );
}
