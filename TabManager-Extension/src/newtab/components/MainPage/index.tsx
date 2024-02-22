import { DndContext, DragOverEvent } from "@dnd-kit/core";
import { ActiveTabBar } from "./ActiveTabBar";
import { Sidebar } from "./Sidebar";
import { TabManager } from "./TabManager";
import { useTabOverStore } from "@/newtab/stores/useTabOver";

export function MainPage() {
   const setDropOverId = useTabOverStore(state => state.setDropOverId)
   const setTabItemData = useTabOverStore(state => state.setTabItemData)
   const handleDragOver = (e: DragOverEvent) => {
      const {over, active} = e
      if(!over) return
      setDropOverId(over.id as string)
      const dragData = active.data.current
      if(!dragData) return
      setTabItemData({
         id: active.id as string,
         title: dragData.title,
         url: dragData.url,
         icon: dragData.icon
      })
   }
   return (
      <div className="flex h-screen items-stretch bg-[#1A1D27]">
         <Sidebar />
         <DndContext onDragOver={handleDragOver}>
            <TabManager />
            <ActiveTabBar />
         </DndContext>
      </div>
   );
}