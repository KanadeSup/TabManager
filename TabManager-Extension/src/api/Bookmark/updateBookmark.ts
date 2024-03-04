import { fetchPrivateData } from "../fetchPrivateData";

type UpdateBookmarkProps = {
   id: string;
   bookmark: UpdateBookmark;
};
type UpdateBookmark = {
   title: string;
   url: string;
   iconUrl: string;
};
export async function updateBookmark({ id, bookmark }: UpdateBookmarkProps) {
   const res = await fetchPrivateData({
      method: "PUT",
      url: `bookmarks/${id}`,
      body: { bookmark },
   });
   return res;
}
export type { UpdateBookmarkProps, UpdateBookmark }