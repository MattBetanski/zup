import NextAuth from "next-auth";
import { authConfig } from "@/auth.config";
import Credentials from 'next-auth/providers/credentials';
import { z } from "zod";
import type { User } from '@/app/lib/definitions';

export const { auth, signIn, signOut } = NextAuth({
    ...authConfig,
    providers: [
        Credentials({
            async authorize(credentials) {
                const parsedCredentials = z
                    .object({ email: z.string().email(), password: z.string().min(6) })
                    .safeParse(credentials);

                if(parsedCredentials.success) {
                    const { email, password } = parsedCredentials.data;
                    const authenticated = await fetch('http://localhost:9090/account/login', {
                        method: "POST",
                        body: JSON.stringify({
                            "email": email,
                            "password": password
                        }),
                        headers: {
                            "Content-Type": "application/json"
                        },
                    });
                    if(authenticated.status == 401 || authenticated.status == 500) {
                        return null;
                    }
                    const user_data: User = await authenticated.json();
                    return user_data;
                }
                console.log('Invalid user');
                return null;
            },
        }),
    ],
});