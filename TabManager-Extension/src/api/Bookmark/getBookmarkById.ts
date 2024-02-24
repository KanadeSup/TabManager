import { fetchData } from "../fetchData";

export async function getBookmarkById({ id }: { id: string }) {
   const res = await fetchData({
      method: "GET",
      url: `bookmarks/${id}`,
   });
   return res;
}