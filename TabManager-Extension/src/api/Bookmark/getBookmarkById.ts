import { fetchPrivateData } from "../fetchPrivateData";

export async function getBookmarkById({ id }: { id: string }) {
   const res = await fetchPrivateData({
      method: "GET",
      url: `bookmarks/${id}`,
   });
   return res;
}