import ZupLogo from "../ui/zup-logo";
import CreateForm from "../ui/create-form";

export default function CreatePage() {
    return (
        <main className="flex items-center justify-self-center bg-surface-200 md:h-screen">
            <div className="relative mx-auto flex w-full max-w-[400px] flex-col space-y-2.5 p-4 md:-mt-32">
                <div className="flex h-20 w-full items-end rounded-tl-3xl rounded-tr-lg bg-primary-400 p-3 md:h-36">
                    <div className="w-32 text-white md:w-36">
                        <ZupLogo />
                    </div>
                </div>
                <CreateForm />
            </div>
        </main>
    );
}