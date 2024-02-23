import { fetchData } from "../fetchData";

export async function deleteSpace({ id }: { id: string }) {
   const res = await fetchData({
      method: "DELETE",
      url: `space/${id}`,
   });
   return res;
}