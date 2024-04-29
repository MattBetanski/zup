import { EyeIcon, FolderIcon, GlobeAltIcon } from '@heroicons/react/24/solid';
import { lusitana } from '@/app/ui/fonts';
import { source } from '@/app/ui/fonts';
import { FileDashed } from '@phosphor-icons/react';
export default function ZupLogo() {
  return (
    <div
      className={`${source.className} flex flex-row items-center leading-none text-white drop-shadow-2xl`}
    >
      <FolderIcon className="h-24 w-24 ml-5 scale-150" />
      <p className="text-[44px] pl-5">Zup</p>
    </div>
  );
}
