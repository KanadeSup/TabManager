import { fetchPrivateData } from "../fetchPrivateData";

export async function getBookmarks({ cateId }: { cateId: string }) {
   const res = await fetchPrivateData({
      method: "GET",
      url: `categories/${cateId}/bookmarks`,
   });
   return res;
}