import { fetchData } from "../fetchData";

type RegisterProps = {
   email: string;
   password: string;
};
export async function register({ email, password }: RegisterProps) {
   return await fetchData({
      method: "POST",
      url: "auth/register",
      body: { email, password },
   });
}
export type { RegisterProps }