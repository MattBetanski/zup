'use server'
import ZupLogo from "../ui/zup-logo";
import LoginForm from "../ui/login-form";
import { checkIfAuthenticated } from "../lib/authentication/action";

export default async function LoginPage() {
    return (
        <main className="flex items-center justify-self-center md:h-screen bg-surface-200">
            <div className="relative mx-auto flex w-full max-w-[400px] flex-col space-y-2.5 p-4 md:-mt-32">
                <div className="flex h-20 w-full items-end rounded-tr-lg rounded-tl-3xl bg-primary-400 p-3 md:h-36">
                    <div className="w-32 text-white md:w-36">
                        <ZupLogo />
                    </div>
                </div>
                <LoginForm />
            </div>
        </main>
    );
}