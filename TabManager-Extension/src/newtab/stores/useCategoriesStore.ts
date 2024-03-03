import { Bookmark } from '@/types/Bookmark';
import { FullyCategory } from '@/types/Category';
import { create } from 'zustand'

type CategoriesStore = {
   categories: FullyCategory[] | null;
   setCategories: (categories: FullyCategory[]) => void;
   addCategory: (category: FullyCategory) => void;
   deleteCategory(cateId: string): void;
   updateCategory(category : FullyCategory): void;
   addBookmark: (cateId: string, bookmark: Bookmark) => void;
   deleteBookmark: (bookmarkId: string) => void;
}
export const useCategoriesStore = create<CategoriesStore>(set => ({
   categories: null,
   setCategories: (categories) => set({ categories }),
   addCategory: (category) => set(state => {
      if(!state.categories) return state;
      return { categories: [...state.categories, category] }
   }),
   deleteCategory: (cateId) => set(state => {
      if(!state.categories) return state;
      return { categories: state.categories.filter(cate => cate.id !== cateId) }
   }),
   updateCategory: (category) => set(state => {
      if(!state.categories) return state;
      const newCategories = state.categories.map(cate => {
         if(cate.id === category.id) {
            return category
         }
         return cate
      })
      return { categories: newCategories };
   }),
   addBookmark: (cateId, bookmark) => set(state => {
      if(!state.categories) return state;
      const newCategories = state.categories.map(cate => {
         if(cate.id === cateId) {
            return {
               ...cate,
               bookmarks: [...cate.bookmarks, bookmark]
            }
         }
         return cate
      })
      return { categories: newCategories };
   }),
   deleteBookmark: (bookmarkId) => set(state => {
      if(!state.categories) return state;
      const newCategories = state.categories.map(cate => {
         return {
            ...cate,
            bookmarks: cate.bookmarks.filter(bookmark => bookmark.id !== bookmarkId)
         }
      })
      return { categories: newCategories }
   }) 
}))