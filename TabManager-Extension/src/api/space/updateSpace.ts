import { fetchData } from "../fetchData";

type AddSpaceProps = {
   id: string;
   name: string;
   hexColor: string;
};
export async function updateSpace({ id, name, hexColor }: AddSpaceProps) {
   const res = await fetchData({
      method: "PUT",
      url: `space/${id}`,
      body: { name, hexColor },
   });
   return res;
}
export type { AddSpaceProps }