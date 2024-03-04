import { fetchPrivateData } from "../fetchPrivateData";

export async function deleteSpace({ id }: { id: string }) {
   const res = await fetchPrivateData({
      method: "DELETE",
      url: `space/${id}`,
   });
   return res;
}