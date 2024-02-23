import { fetchData } from "../fetchData";

export async function getCategoryById({ id }: { id: string }) {
   const res = await fetchData({
      method: "GET",
      url: `categories/${id}`,
   });
   return res;
}