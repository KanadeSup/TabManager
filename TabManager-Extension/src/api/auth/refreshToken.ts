import { appsetting } from "@/appsetting";
import axios from "axios";

export async function refreshToken() {
   try {
      const res = await axios({
         method: "POST",
         url: appsetting.apiBaseUrl + "auth/refresh",
         headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + localStorage.getItem("accessToken"),
         },
         data: localStorage.getItem("refreshToken"),
      });
      localStorage.setItem("accessToken", res.data.token);
      localStorage.setItem("refreshToken", res.data.refreshToken);
      return {
         accessToken: res.data.token,
         refreshToken: res.data.refreshToken,
      }
   } catch (e) {
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");
      return null;
   }
}
