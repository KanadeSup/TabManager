import { fetchPrivateData } from "../fetchPrivateData";

export async function deleteBookmark({ id }: { id: string }) {
   const res = await fetchPrivateData({
      method: "DELETE",
      url: `bookmarks/${id}`,
   });
   return res;
}