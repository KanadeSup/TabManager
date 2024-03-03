import { getAllSPace } from "@/api/space/getAllSpace";
import { Space } from "@/types/Space";
import { create } from "zustand";

type SpaceStore = {
   spaces: Space[] | null;
   setSpaces: (spaces: Space[]) => void;
   fetchSpaces: () => Promise<void>;
   addSpace: (space: Space) => void;
   deleteSpace: (space: Space) => void;
   updateSpace: (space: Space) => void;
}
export const useSpaceStore = create<SpaceStore>((set) => ({
   spaces: null,
   setSpaces: (spaces) => set({ spaces }),
   fetchSpaces: async () => {
      const res = await getAllSPace()
      if (!res.ok) return
      set({ spaces: res.data })
   },
   addSpace: (space) => set(state => {
      if (!state.spaces) return state
      return { spaces: [...state.spaces, space] }
   }),
   deleteSpace: (space) => set((state) => {
      if (!state.spaces) return state;
      return { spaces: state.spaces.filter((s) => s.id !== space.id) }
   }),
   updateSpace: (space) => set((state) => {
      if (!state.spaces) return state;
      if (!state.spaces.find((s) => s.id === space.id)) return state;
      return { spaces: state.spaces.map((s) => s.id === space.id ? space : s) }
   }),
}));