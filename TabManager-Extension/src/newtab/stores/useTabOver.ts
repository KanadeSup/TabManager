import { Tab } from '@/types/Tab';
import { create } from 'zustand'

type TabOverStore = {
   dropOverId: string | null;
   tabItemData: Tab | null;
   setDropOverId: (id: string | null) => void;
   setTabItemData: (data: Tab | null) => void;
}
export const useTabOverStore = create<TabOverStore>(set => ({
   dropOverId : null,
   tabItemData: null,
   setDropOverId: (id) => set({dropOverId: id}),
   setTabItemData: (data) => set({tabItemData: data})
}))