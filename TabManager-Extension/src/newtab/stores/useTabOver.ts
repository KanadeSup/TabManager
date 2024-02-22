import { create } from 'zustand'

type TabItemData = {
   id: string | undefined;
   title: string | undefined;
   url: string | undefined;
   icon: string | undefined;
}

type TabOverStore = {
   dropOverId: string | null;
   tabItemData: TabItemData | null;
   setDropOverId: (id: string | null) => void;
   setTabItemData: (data: TabItemData | null) => void;
}
export const useTabOverStore = create<TabOverStore>(set => ({
   dropOverId : null,
   tabItemData: null,
   setDropOverId: (id: string | null) => set({dropOverId: id}),
   setTabItemData: (data: TabItemData | null) => set({tabItemData: data})
}))