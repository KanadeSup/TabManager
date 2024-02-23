import { fetchData } from "../fetchData";

export async function getSpaceById({ id }: { id: string }) {
   const res = await fetchData({
      method: "GET",
      url: `space/${id}`,
   });
   return res;
}