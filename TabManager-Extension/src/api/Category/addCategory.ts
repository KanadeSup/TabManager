import { fetchPrivateData } from "../fetchPrivateData";

type AddCategoryProps = {
   spaceId: string;
   name: string;
};
export async function addCategory({ spaceId, name }: AddCategoryProps) {
   const res = await fetchPrivateData({
      method: "POST",
      url: `spaces/${spaceId}/categories`,
      body: { name },
   });
   return res;
}
export type { AddCategoryProps }