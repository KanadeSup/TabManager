import { IconLogout } from "@tabler/icons-react";
import { IconLibraryPlus, IconLogout2, IconPointFilled, IconSettings } from "@tabler/icons-react";
import { useState } from "react";
import { CreateSpaceModal } from "./CreateSpaceModal";

export function Sidebar() {
    return (
      <div className="flex flex-col w-[270px] bg-[#1A1D27] px-5 py-3">
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
   return (
      <div className="mt-1">
         <SpaceCard name="Personal"/>
         <SpaceCard name="Work"/>
         <SpaceCard name="Social"/>
      </div>
   )
}

type SpaceCardProps = {
   name: string;
}
function SpaceCard({ name }: SpaceCardProps) {
   return (
      <div className="flex items-center gap-3 hover:bg-gray-800 px-3 py-2 cursor-pointer rounded-lg">
         <div className="w-[10px] h-[10px] rounded-full bg-[#de493e]"></div>
         <h1 className="font-medium text-[16px]">{name}</h1>
      </div>
   )
}

function Footer() {
   return (
      <div className="mt-auto">
         <div className="flex items-center gap-2 px-2 py-2 cursor-pointer hover:bg-gray-800 rounded-lg"> 
            <IconSettings size={23} color="gray"/>
            <h1 className="font-bold text-[17px] text-gray-400"> Settings </h1>
         </div>
         <div className="flex items-center gap-2 px-2 py-2 cursor-pointer hover:bg-gray-800 rounded-lg"> 
            <IconLogout size={23} color="gray"/>
            <h1 className="font-bold text-[17px] text-gray-400"> Logout </h1>
         </div>
      </div>
   )
}