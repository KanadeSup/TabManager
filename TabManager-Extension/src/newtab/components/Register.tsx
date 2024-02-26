import { LoginProps, login } from "@/api/auth/login";
import { register } from "@/api/auth/register";
import { Button, PasswordInput, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useState } from "react";

type FormValues = {
   email: string;
   password: string;
};

type RegisterPageProps = {
   setPage: (page: 'login' | 'register') => void
}
export function Register({ setPage } : RegisterPageProps) {
   const handleRegister = async ({email, password}: FormValues) => {
      const res = await register({email, password});
      if (res.ok) {
         setPage('login')
      } else {
         setError(res.data || "An error occurred")
      }
   };
   const form = useForm({
      initialValues: {
         email: "",
         password: "",
         rePassword: "",
      },
      validate: {
         email: (value) => (/^\S+@\S+$/.test(value) ? null : "Invalid email"),
         rePassword: (value, values) => value === values.password ? null : "Passwords do not match",
         password: (value) => value.length >= 6 ? null : "Password is too short",
      },
   });
   const [error, setError] = useState<string | null>(null);
   return (
      <div className="flex items-center justify-center h-screen">
         <div className="w-[400px] space-y-10">
            <h1 className="text-[28px] text-center font-bold">Sign in to your account</h1>
            <form
               className="flex flex-col gap-5"
               onSubmit={form.onSubmit((values) => handleRegister(values))}
            >
               <TextInput
                  label="Email"
                  size="md"
                  placeholder="your@email.com"
                  {...form.getInputProps("email")}
               />
               <PasswordInput
                  label="Password"
                  type="password"
                  size="md"
                  placeholder="***********"
                  {...form.getInputProps("password")}
               />
               <PasswordInput
                  label="Repeat password"
                  type="password"
                  size="md"
                  placeholder="***********"
                  {...form.getInputProps("rePassword")}
               />
               <div className="mt-5 flex flex-col gap-2">
                  <Button type="submit" variant="filled" size="lg" color="violet">
                     Sign up
                  </Button>
                  <p className="text-red-500 text-sm"> 
                     {error}
                  </p>
               </div>
               <p className="text-center font-bold text-sm">
                  Already has account?{" "}
                  <span className="text-violet-600 font-bold cursor-pointer"
                     onClick={() => setPage('login')}
                  >
                     Login
                  </span>
               </p>
            </form>
         </div>
      </div>
   );
}
