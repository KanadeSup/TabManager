import { Space } from '@/types/Space';
import { ActiveTab, Tab } from '@/types/Tab';
import { create } from 'zustand'

type ActiveTabStore = {
   activeTabs: ActiveTab[] | null;
   refreshActiveTabs: () => void;
}
export const useActiveTabStore = create<ActiveTabStore>(set => ({
   activeTabs : null,
   refreshActiveTabs: async () => {
      const tabList = await getAllTabs();
      let filterdTabList : ActiveTab[] = []
      for (const tab of tabList) {
         if (tab.url !== undefined) {
            filterdTabList.push({
               id: tab.id,
               title: tab.title,
               url: tab.url,
               icon: tab.icon
            })
         }
      }
      set({ activeTabs: filterdTabList });
   }
}))

async function getAllTabs() {
   const currentTab = await browser.tabs.getCurrent();
   const tabList = await browser.tabs.query({});
   return tabList
      .filter((tab) => tab.id !== currentTab.id)
      .map((tab) => {
         return {
            id: tab.id,
            title: tab.title,
            url: tab.url,
            icon: tab.favIconUrl,
         };
   });
}