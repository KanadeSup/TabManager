import { fetchPrivateData } from "../fetchPrivateData";

export async function getAllCategories({ spaceId }: { spaceId: string }) {
   const res = await fetchPrivateData({
      method: "GET",
      url: `spaces/${spaceId}/categories`,
   });
   return res;
}