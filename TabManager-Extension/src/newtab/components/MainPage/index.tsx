import { DndContext, DragEndEvent, DragOverEvent } from "@dnd-kit/core";
import { ActiveTabBar } from "./ActiveTabBar";
import { Sidebar } from "./Sidebar";
import { TabManager } from "./TabManager";
import { useTabOverStore } from "@/newtab/stores/useTabOver";
import { AddBookmark, addBookmark } from "@/api/Bookmark/addBookmark";
import { useCategoriesStore } from "@/newtab/stores/useCategoriesStore";

export function MainPage() {
   const setDropOverId = useTabOverStore(state => state.setDropOverId)
   const setTabItemData = useTabOverStore(state => state.setTabItemData)
   const appendBookmark = useCategoriesStore(state => state.addBookmark)
   const handleDragOver = (e: DragOverEvent) => {
      const {over, active} = e
      if(!over) return
      setDropOverId(over.id as string)
      const dragData = active.data.current
      if(!dragData) return
      setTabItemData({
         title: dragData.title,
         url: dragData.url,
         icon: dragData.icon
      })
   }
   const handleDragEnd = (e: DragEndEvent) => {
      const { over, active } = e
      if(!over) return
      const cateId = over.id as string
      const activeData = active.data.current as AddBookmark
      if(!activeData) return
      async function fetchData() {
         const res = await addBookmark({
            categoryId: cateId,
            bookmark: activeData
         })
         if(!res.ok) return
         appendBookmark(cateId, res.data)
         setDropOverId(null)
         browser.tabs.remove(active.id as number)
      }
      fetchData()
   }
   return (
      <div className="flex h-screen items-stretch bg-[#1A1D27]">
         <Sidebar />
         <DndContext onDragOver={handleDragOver} onDragEnd={handleDragEnd}>
            <TabManager />
            <ActiveTabBar />
         </DndContext>
      </div>
   );
}