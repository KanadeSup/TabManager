import { fetchData } from "../fetchData";

export async function deleteBookmark({ id }: { id: string }) {
   const res = await fetchData({
      method: "DELETE",
      url: `bookmarks/${id}`,
   });
   return res;
}