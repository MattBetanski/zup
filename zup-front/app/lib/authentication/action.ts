'use server';
import { User } from "@phosphor-icons/react";
import { jwtDecode } from "jwt-decode";
import { revalidatePath } from "next/cache";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";
import { userAgent } from "next/server";
import { z } from "zod";

const UserSchema = z.object({
    userId: z.string(),
    username: z.string({invalid_type_error: "Please enter a username"})
        .min(1, {message: "Please enter a username"}),
    password: z.string({invalid_type_error: "Please enter a password"})
        .min(1, {message: "Please enter a password"}),
    email: z.string({invalid_type_error: "Please enter an email"})
        .min(1, {message: "Please enter a email"}),
    firstName: z.string({invalid_type_error: "Please enter a first name"})
        .min(1, {message: "Please enter a first name"}),
    lastName: z.string({invalid_type_error: "Please enter a last name"})
        .min(1, {message: "Please enter a last name"}),
    accountActivated: z.boolean(),
    joinDate: z.date(),
});

const CreateAccount = UserSchema.omit({userId: true, accountActivated: true, joinDate: true});
const LoginAccount = UserSchema.omit({userId: true, accountActivated: true, firstName: true, lastName: true, joinDate: true, email: true});

export type CreateAccountState = {
    errors?: {
        username?: string[],
        password: string[],
        email?: string[],
        firstName?: string[],
        lastName?: string[]
    },
    message?: string | null;
}

export async function createAccount(prevState: CreateAccountState, formData: FormData) {
    try {
        const validatedFields = CreateAccount.safeParse({
            username: formData.get("username"),
            password: formData.get("password"),
            email: formData.get("email"),
            firstName: formData.get("firstName"),
            lastName: formData.get("lastName")
        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing required fields. Failed to create account"
            };
        }

        const {username, password, email, firstName , lastName} = validatedFields.data;
        console.log("got here");
        const response = await fetch('http://localhost:5001/user/create', {
            method: "POST",
            body: JSON.stringify({
                "username": username,
                "email": email,
                "password": password,
                "firstName": firstName,
                "lastName": lastName
            }),
            headers: {
                "Content-Type": "application/json"
            }
        });
        if (response.status == 200) {
            const body = await response.json();
            cookies().set("token", body.token);
        } else if (response.status == 400) {
            const body = await response.text();
            return {message: body}; 
        } else if (response.status == 500) {
            const body = await response.text();
            return {message: body};
        } else {
            return {message: "Internal server error occured. Try again later."}
        }
        revalidatePath("./dashboard")
    } catch (err) {
        console.error(err);
        return { message: "Internal error occured" };
    }
    redirect("./dashboard");
}

export type LoginAccountState = {
    errors?: {
        username?: string[],
        password?: string[],
    },
    message?: string | null;
}

export async function loginAccount(prevState: LoginAccountState, formData: FormData) {
    try {

        const validatedFields = LoginAccount.safeParse({
            username: formData.get("username"),
            password: formData.get("password"),
        });

        if (!validatedFields.success) {
            return {
                errors: validatedFields.error.flatten().fieldErrors,
                message: "Missing required fields. Failed to log in to account"
            };
        }
        console.log("got here");
        const {username, password} = validatedFields.data;
        console.log("got here");
        const response = await fetch('http://localhost:5001/user/login', {
            method: "POST",
            body: JSON.stringify({
                "username": username,
                "password": password,
            }),
            headers: {
                "Content-Type": "application/json"
            }
        });
        if (response.status == 200) {
            const body = await response.json();
            cookies().set("token", body.token);
            revalidatePath("./dashboard")
        } else if (response.status == 400) {
            const body = await response.text();
            return {message: body};
        } else if (response.status == 500) {
            const body = await response.text();
            return {message: body};
        } else {
            return {message: "Internal server error occured. Try again later."}
        }
    } catch (err) {
        console.error(err);
        return {
            message: "Internal error occured"
        };
    }
    redirect("./dashboard");
}

export async function checkIfAuthenticated() {
    let authenticated = false;
    try {
        const token = cookies().get("token")?.value;
        if (token != null) {
            let decoded = jwtDecode(token);
            if (decoded.exp != null && Date.now() < decoded.exp * 1000) {
                authenticated = true;
            }
        }
        console.log(token);
    } catch (err) {
        console.log(err);
    }
    if (!authenticated) redirect("./login");
}