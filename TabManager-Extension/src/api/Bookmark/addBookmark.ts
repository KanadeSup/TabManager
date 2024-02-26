import { base64ImgToImgData } from "@/ultilities/helper";
import { fetchData } from "../fetchData";

type AddBookmarkProps = {
   categoryId: string;
   bookmark: AddBookmark;
};
type AddBookmark = {
   title : string;
   url : string;
   icon : string;
}
export async function addBookmark({ categoryId, bookmark }: AddBookmarkProps) {
   // create multipart form data
   const formData = new FormData();
   formData.append("title", bookmark.title);
   formData.append("Url", bookmark.url.trim());
   if(bookmark.icon) {
      const imgData = base64ImgToImgData(bookmark.icon)
      formData.append("icon", imgData.data, imgData.type);
   }
   const res = await fetchData({
      method: "POST",
      url: `categories/${categoryId}/bookmarks`,
      headers: {
         "Content-Type": "multipart/form-data",
      },
      body: formData,
   });
   return res;
}
export type { AddBookmarkProps, AddBookmark }