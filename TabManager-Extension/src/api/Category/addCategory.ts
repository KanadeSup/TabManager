import { fetchData } from "../fetchData";

type AddCategoryProps = {
   spaceId: string;
   name: string;
};
export async function addCategory({ spaceId, name }: AddCategoryProps) {
   const res = await fetchData({
      method: "POST",
      url: `spaces/${spaceId}/categories`,
      body: { name },
   });
   return res;
}
export type { AddCategoryProps }