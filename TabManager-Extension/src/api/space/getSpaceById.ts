import { fetchPrivateData } from "../fetchPrivateData";

export async function getSpaceById({ id }: { id: string }) {
   const res = await fetchPrivateData({
      method: "GET",
      url: `space/${id}`,
   });
   return res;
}