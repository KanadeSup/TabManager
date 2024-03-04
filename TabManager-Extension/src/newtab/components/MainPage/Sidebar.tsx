import { IconLogout } from "@tabler/icons-react";
import { IconLibraryPlus, IconLogout2, IconPointFilled, IconSettings } from "@tabler/icons-react";
import { useEffect, useState } from "react";
import { Space } from "@/types/Space";
import { useSelectedSpace } from "@/newtab/stores/useSelectedSpace";
import { logout } from "@/newtab/Utils/logout";
import { useNavigate } from "react-router-dom";
import { useSpaceStore } from "@/newtab/stores/useSpaceStore";
import { SpaceModalForm, useSpaceModalFormStores } from "./SpaceModalForm";

export function Sidebar() {
    return (
      <div className="flex flex-col w-[270px] bg-[#1A1D27] px-5 py-3 shrink-0">
         <Header/>
         <SpaceList/>
         <Footer/>
         <SpaceModalForm />
      </div>
    );
}

function Header() {
   const setSpaceModalOpen = useSpaceModalFormStores(state => state.setOpen)
   return (
      <div>
         <h1 className="text-[30px] font-bold"> Tab </h1>
         <div className="flex justify-between items-center mt-5">
            <h2 className="text-[20px] font-bold"> Spaces </h2>
            <IconLibraryPlus size={35} className="cursor-pointer hover:bg-gray-800 p-[6px] rounded-lg"
               onClick={e=> setSpaceModalOpen({
                  action: "create",
                  isOpen: true
               })}
            />
         </div>
      </div>
   )
}

function SpaceList() {
   const fetchSpaces = useSpaceStore(state => state.fetchSpaces)
   const spaces = useSpaceStore(state => state.spaces)
   const setSelectedSpace = useSelectedSpace(state => state.setSpace)
   useEffect(() => {
      fetchSpaces()
   }, [])
   useEffect(() => {
      if(!spaces) return
      const lastSelectedSpaceId = localStorage.getItem("lastSelectedSpaceId")
      if(lastSelectedSpaceId) {
         const lastSelectedSpace = spaces.find(space => space.id == lastSelectedSpaceId)
         setSelectedSpace(lastSelectedSpace || spaces[0])
      }
   },[spaces !== null])
   return (
      <div className="mt-1">
         {
            spaces ? 
               spaces
               .sort((a, b) => {
                  const date1 = new Date(a.creationTime!)
                  const date2 = new Date(b.creationTime!)
                  return date1.getTime() - date2.getTime()
               })
               .map(space => (
                  <SpaceCard key={space.id} space={space}/>
               )) : (
                  null
               )
         }
      </div>
   )
}

function SpaceCard({ space }: { space: Space }) {
   const setSelectedSpace = useSelectedSpace(state => state.setSpace)
   return (
      <div className="flex items-center gap-3 hover:bg-gray-800 px-3 py-2 cursor-pointer rounded-lg"
         onClick={e=>setSelectedSpace(space)}
      >
         <div className={`w-[10px] h-[10px] rounded-full`} style={{backgroundColor: `${space.hexColor}`}}></div>
         <h1 className="font-medium text-[16px]">{space.name}</h1>
      </div>
   )
}

function Footer() {
   const navigate = useNavigate()
   return (
      <div className="mt-auto">
         <div className="flex items-center gap-2 px-2 py-2 cursor-pointer hover:bg-gray-800 rounded-lg"> 
            <IconSettings size={23} color="gray"/>
            <h1 className="font-bold text-[17px] text-gray-400"> Settings </h1>
         </div>
         <div className="flex items-center gap-2 px-2 py-2 cursor-pointer hover:bg-gray-800 rounded-lg"
            onClick={() => {
               logout()   
               navigate("/login")
            }}
         > 
            <IconLogout size={23} color="gray"/>
            <h1 className="font-bold text-[17px] text-gray-400"> 
               Logout 
            </h1>
         </div>
      </div>
   )
}