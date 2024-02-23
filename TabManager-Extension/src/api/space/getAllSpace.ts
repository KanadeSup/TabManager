import { fetchData } from "../fetchData";

export async function getAllSPace() {
   const res = await fetchData({
      method: "GET",
      url: `space`,
   });
   return res;
}