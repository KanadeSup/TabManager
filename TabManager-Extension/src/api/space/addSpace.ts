import { fetchData } from "../fetchData";

type AddSpaceProps = {
   name: string;
   hexColor: string;
};
export async function addSpace({ name, hexColor }: AddSpaceProps) {
   const res = await fetchData({
      method: "POST",
      url: "space",
      body: { name, hexColor },
   });
   return res;
}
export type { AddSpaceProps }