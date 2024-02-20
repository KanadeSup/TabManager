import { fetchData } from "../fetchData";

type LoginProps = {
   email: string;
   password: string;
};
export async function login({ email, password }: LoginProps) {
   const res = await fetchData({
      method: "POST",
      url: "auth/login",
      body: { email, password },
   });
   localStorage.setItem("token", res.data);
   return res;
}
export type { LoginProps }