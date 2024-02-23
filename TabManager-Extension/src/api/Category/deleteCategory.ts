import { fetchData } from "../fetchData";

export async function deleteCategory({ id }: { id: string }) {
   const res = await fetchData({
      method: "DELETE",
      url: `categories/${id}`,
   });
   return res;
}