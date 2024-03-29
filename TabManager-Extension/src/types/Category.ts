import { Bookmark } from "./Bookmark";

export type Category = {
   id: string;
   name: string;
   creationTime?: string;
}

export type FullyCategory = Category & {
   bookmarks: Array<Bookmark>;
}