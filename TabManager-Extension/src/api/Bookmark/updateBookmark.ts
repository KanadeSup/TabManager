import { fetchData } from "../fetchData";

type UpdateBookmarkProps = {
   id: string;
   bookmark: UpdateBookmark;
};
type UpdateBookmark = {
   title: string;
   url: string;
   iconUrl: string;
};
export async function updateSpace({ id, bookmark }: UpdateBookmarkProps) {
   const res = await fetchData({
      method: "PUT",
      url: `bookmarks/${id}`,
      body: { bookmark },
   });
   return res;
}
export type { UpdateBookmarkProps, UpdateBookmark }