import { appsetting } from "@/appsetting";
import { AxiosError } from "axios";
import { privateAxios } from "./privateAxios";
export async function fetchPrivateData({method = "GET", url="", headers = {}, body={}}) {
   headers = {
      "Content-Type": "application/json",
      ...headers,
   };
   try {
      const res = await privateAxios({
         method,
         url: appsetting.apiBaseUrl + url,
         headers: {
            "Content-Type": "application/json",
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