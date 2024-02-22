import { LoginProps, login } from "@/api/auth/login";
import { Button, PasswordInput, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useEffect, useState } from "react";
import { Page } from "../types/Page";

type FormValues = {
   email: string;
   password: string;
};

type LoginPageProps = {
   setPage: (page: Page) => void
}
export function Login({ setPage } : LoginPageProps) {
   const handleLogin = async ({email, password}: FormValues) => {
      const res = await login({email, password});
      if (res.ok) {
         setPage('main')
      } else {
         setError(res.data || "An error occurred")
      }
   };
   const form = useForm({
      initialValues: {
         email: "",
         password: "",
      },
      validate: {
         email: (value) => (/^\S+@\S+$/.test(value) ? null : "Invalid email"),
      },
   });
   useEffect(() => {
      if(localStorage.getItem('token')) setPage('main')
   },[])
   const [error, setError] = useState<string | null>(null);
   return (
      <div className="flex items-center justify-center h-screen">
         <div className="w-[400px] space-y-10">
            <h1 className="text-[28px] text-center font-bold">Sign in to your account</h1>
            <form
               className="flex flex-col gap-5"
               onSubmit={form.onSubmit((values) => handleLogin(values))}
            >
               <TextInput
                  label="Email"
                  size="md"
                  placeholder="your@email.com"
                  {...form.getInputProps("email")}
               />
               <div className="flex flex-col gap-3">
                  <PasswordInput
                     label="Password"
                     type="password"
                     size="md"
                     placeholder="***********"
                     {...form.getInputProps("password")}
                  />
                  <a
                     href=""
                     className="text-sm font-bold ml-auto hover:underline-offset-2 hover:underline"
                  >
                     Forgot password?
                  </a>
               </div>
               
               <div className="mt-5 flex flex-col gap-2">
                  <Button type="submit" variant="filled" size="lg" color="violet">
                     Sign in
                  </Button>
                  <p className="text-red-500 text-sm"> 
                     {error}
                  </p>
               </div>
               <p className="text-center font-bold text-sm">
                  Don't have an account?{" "}
                  <span className="text-violet-600 font-bold cursor-pointer"
                     onClick={() => setPage('register')}
                  >
                     Sign up
                  </span>
               </p>
            </form>
         </div>
      </div>
   );
}
