import { fetchPublicData } from "../fetchPublicData";

type RegisterProps = {
   email: string;
   password: string;
};
export async function register({ email, password }: RegisterProps) {
   return await fetchPublicData({
      method: "POST",
      url: "auth/register",
      body: { email, password },
   });
}
export type { RegisterProps }