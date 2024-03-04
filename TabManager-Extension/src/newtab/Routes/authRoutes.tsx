import { redirect } from "react-router-dom";
import { Login } from "../components/Login";
import { Register } from "../components/Register";

export const authRoutes =  [
   {
      path: "/Login",
      element: <Login />,
      loader: () => {
         if(localStorage.getItem("accessToken")) {
            return redirect("/")
         }
         return null
      }
   },
   {
      path: "/Register",
      element: <Register />
   }
]