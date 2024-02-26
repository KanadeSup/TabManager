import { useActiveTabStore } from "@/newtab/stores/useActiveTabStore";
import { ActiveTab } from "@/types/Tab";
import { useDraggable } from "@dnd-kit/core";
import { useEffect, useState } from "react";

export function ActiveTabBar() {
   return (
      <div className="w-[280px] bg-[#121319] px-3 py-3 space-y-5 flex-shrink-0">
         <h1 className="text-[20px] font-bold"> Active Tabs </h1>
         <ActiveTabList />
      </div>
   );
}

function ActiveTabList() {
   const activeTabs = useActiveTabStore((state) => state.activeTabs);
   const refreshActiveTabs = useActiveTabStore((state) => state.refreshActiveTabs);
   useEffect(() => {
      refreshActiveTabs()
   },[]);
   return (
      <div className="flex flex-col gap-2">
         {activeTabs && activeTabs
            .filter((tab) => tab.id !== undefined )
            .map((tab) => {
            return (
               <TabCard tab={tab} key={tab.id} />
            );
         })}
      </div>
   );
}

function TabCard({tab} : {tab: ActiveTab}) {
   const {attributes, listeners, setNodeRef, transform} = useDraggable({
      id: tab.id || -1,
      data: {
         title: tab.title,
         url: tab.url,
         icon: tab.icon,
      }
   });
   return (
      <div 
         className="flex gap-4 p-3 items-center rounded-lg bg-[#191A21] hover:bg-[#1F2129] cursor-pointer"
         ref={setNodeRef}
         style={{transform: transform ? `translate3d(${transform.x}px, ${transform.y}px, 0)` : undefined}}
         {...listeners}
         {...attributes}
      >
         <img src={tab.icon} className="w-4 h-4"/>
         <h1 className="truncate font-medium text-[14px]">{tab.title}</h1>
      </div>
   );
}
