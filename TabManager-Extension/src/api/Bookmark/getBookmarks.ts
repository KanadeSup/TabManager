import { fetchData } from "../fetchData";

export async function getBookmarks({ cateId }: { cateId: string }) {
   const res = await fetchData({
      method: "GET",
      url: `categories/${cateId}/bookmarks`,
   });
   return res;
}