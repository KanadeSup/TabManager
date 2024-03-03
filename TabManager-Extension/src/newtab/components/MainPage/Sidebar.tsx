import { IconLogout } from "@tabler/icons-react";
import { IconLibraryPlus, IconLogout2, IconPointFilled, IconSettings } from "@tabler/icons-react";
import { useEffect, useState } from "react";
import { CreateSpaceModal } from "./CreateSpaceModal";
import { getAllSPace } from "@/api/space/getAllSpace";
import { Space } from "@/types/Space";
import { useSelectedSpace } from "@/newtab/stores/useSelectedSpace";
import { logout } from "@/newtab/Utils/logout";
import { useNavigate } from "react-router-dom";

export function Sidebar() {
    return (
      <div className="flex flex-col w-[270px] bg-[#1A1D27] px-5 py-3 shrink-0">
         <Header/>
         <SpaceList/>
         <Footer/>
      </div>
    );
}

function Header() {
   const [isCreateSpaceModalOpen, setIsCreateSpaceModalOpen] = useState(false)
   return (
      <div>
         <h1 className="text-[30px] font-bold"> Tab </h1>
         <div className="flex justify-between items-center mt-5">
            <h2 className="text-[20px] font-bold"> Spaces </h2>
            <IconLibraryPlus size={35} className="cursor-pointer hover:bg-gray-800 p-[6px] rounded-lg"
               onClick={e=> setIsCreateSpaceModalOpen(true)}
            />
            <CreateSpaceModal isOpen={isCreateSpaceModalOpen} onClose={()=>setIsCreateSpaceModalOpen(false)}/>
         </div>
      </div>
   )
}

function SpaceList() {
   const spaceList = useSpaces()
   return (
      <div className="mt-1">
         {
            spaceList ? 
               spaceList.map(space => (
                  <SpaceCard key={space.id} space={space}/>
               )) : (
                  null
               )
         }
      </div>
   )
}

function useSpaces() {
   const [spaces, setSpaces] = useState<Array<Space> | null>(null)
   useEffect(() => {
      async function fetchSpaces() {
         const res = await getAllSPace()
         if(!res.ok) return
         setSpaces(res.data)
      }
      fetchSpaces()
   }, [])
   return spaces
}

function SpaceCard({ space }: { space: Space }) {
   const setSpace = useSelectedSpace(state => state.setSpace)
   return (
      <div className="flex items-center gap-3 hover:bg-gray-800 px-3 py-2 cursor-pointer rounded-lg"
         onClick={e=>setSpace(space)}
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
         <div className="flex items-center gap-2 px-2 py-2 cursor-pointer hover:bg-gray-800 rounded-lg"> 
            <IconLogout size={23} color="gray"/>
            <h1 className="font-bold text-[17px] text-gray-400"
               onClick={() => {
                  logout()   
                  navigate("/login")
               }}
            > 
               Logout 
            </h1>
         </div>
      </div>
   )
}