import { fetchData } from "../fetchData";

type UpdateCategoryProps = {
   id: string;
   name: string;
};
export async function updateSpace({ id, name }: UpdateCategoryProps) {
   const res = await fetchData({
      method: "PUT",
      url: `space/${id}`,
      body: { name },
   });
   return res;
}
export type { UpdateCategoryProps }