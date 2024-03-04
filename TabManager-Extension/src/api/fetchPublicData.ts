import { appsetting } from "@/appsetting";
import axios, { AxiosError } from "axios";
export async function fetchPublicData({method = "GET", url="", headers = {}, body={}}) {
   headers = {
      "Content-Type": "application/json",
      ...headers,
   };
   try {
      const res = await axios({
         method,
         url: appsetting.apiBaseUrl + url,
         headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + localStorage.getItem("token"),
            ...headers,
         },
         data: body,
      });
      return {
         data: res.data,
         status: res.status,
         ok: true,
      };
   } catch (e) {
      // cast error to AxiosError
      const error = e as AxiosError;
      return {
         data: error.response?.data,
         status: error.response?.status,
         ok: false,
      }
   }
}