import { fetchData } from "../fetchData";

export async function getAllCategories({ spaceId }: { spaceId: string }) {
   const res = await fetchData({
      method: "GET",
      url: `spaces/${spaceId}/categories`,
   });
   return res;
}