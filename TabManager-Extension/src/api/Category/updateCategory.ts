import { fetchData } from "../fetchData";

type UpdateCategoryProps = {
   id: string;
   name: string;
};
export async function updateCategory({ id, name }: UpdateCategoryProps) {
   const res = await fetchData({
      method: "PUT",
      url: `categories/${id}`,
      body: { name },
   });
   return res;
}
export type { UpdateCategoryProps }