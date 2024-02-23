import { Space } from '@/types/Space';
import { create } from 'zustand'

type SelectedSpace = {
   space: Space | null;
   setSpace: (space: Space | null) => void;
}
export const useSelectedSpace = create<SelectedSpace>(set => ({
   space : null,
   setSpace: (space) => set({space})
}))