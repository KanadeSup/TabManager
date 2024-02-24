import { fetchData } from "../fetchData";

type UpdateSpaceProps = {
   id: string;
   name: string;
   hexColor: string;
};
export async function updateSpace({ id, name, hexColor }: UpdateSpaceProps) {
   const res = await fetchData({
      method: "PUT",
      url: `space/${id}`,
      body: { name, hexColor },
   });
   return res;
}
export type { UpdateSpaceProps }