import { fetchPublicData } from "../fetchPublicData";

type LoginProps = {
   email: string;
   password: string;
};
export async function login({ email, password }: LoginProps) {
   const res = await fetchPublicData({
      method: "POST",
      url: "auth/login",
      body: { email, password },
   });
   localStorage.setItem("accessToken", res.data.token);
   localStorage.setItem("refreshToken", res.data.refreshToken);
   return res;
}
export type { LoginProps }