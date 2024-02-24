import { Bookmark } from "./Bookmark";

export type Category = {
   id: string;
   name: string;
}

export type FullyCategory = Category & {
   bookmarks: Array<Bookmark>;
}