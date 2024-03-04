import { fetchPrivateData } from "../fetchPrivateData";

export async function getAllSPace() {
   const res = await fetchPrivateData({
      method: "GET",
      url: `space`,
   });
   return res;
}