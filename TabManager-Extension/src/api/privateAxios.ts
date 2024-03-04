import axios from "axios";
import { refreshToken } from "./auth/refreshToken";


axios.interceptors.request.use(
   async config => {
      const token = localStorage.getItem("accessToken");
      if(token) {
         config.headers.Authorization = "Bearer " + token;
      }
      return config;
   },
   (error) => {
      return Promise.reject(error);
   }
)
axios.interceptors.response.use(
   response => response,
   async error => {
      const config = error?.config;
      if(error?.response?.status === 401 && !config?.sent) {
         config.sent = true;
         const res = await refreshToken();
         if(res?.accessToken) {
            config.headers.Authorization = "Bearer " + res.accessToken;
         }
         return axios(config);
      }
      return Promise.reject(error);
   }
)

export const privateAxios = axios;