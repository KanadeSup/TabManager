import { fetchPrivateData } from "../fetchPrivateData";

export async function deleteCategory({ id }: { id: string }) {
   const res = await fetchPrivateData({
      method: "DELETE",
      url: `categories/${id}`,
   });
   return res;
}