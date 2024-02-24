import { useDraggable } from "@dnd-kit/core";
import { useEffect, useState } from "react";

type Tab = {
   id: number | undefined;
   title: string | undefined;
   url: string | undefined;
   icon: string | undefined;
};

export function ActiveTabBar() {
   return (
      <div className="w-[280px] bg-[#121319] px-3 py-3 space-y-5">
         <h1 className="text-[20px] font-bold"> Active Tabs </h1>
         <ActiveTabList />
      </div>
   );
}

function ActiveTabList() {
   const tabs = useTabs();
   return (
      <div className="flex flex-col gap-2">
         {tabs
            .filter((tab) => tab.id !== undefined )
            .map((tab) => {
            return (
               <TabCard tab={tab} key={tab.id} />
            );
         })}
      </div>
   );
}

const useTabs = () => {
   const [tabs, setTabs] = useState<Array<Tab>>([]);
   useEffect(() => {
      async function getTabs() {
         const tabList = await getAllTabs();
         setTabs(tabList);
      }
      getTabs();
   }, []);
   return tabs;
};

async function getAllTabs() {
   const tabList = await browser.tabs.query({});
   return tabList
      .map((tab) => {
         return {
            id: tab.id,
            title: tab.title,
            url: tab.url,
            icon: tab.favIconUrl,
         };
   });
}

function TabCard({tab} : {tab: Tab}) {
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
