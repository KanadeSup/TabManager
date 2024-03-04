import { fetchPrivateData } from "../fetchPrivateData";

export async function getCategoryById({ id }: { id: string }) {
   const res = await fetchPrivateData({
      method: "GET",
      url: `categories/${id}`,
   });
   return res;
}