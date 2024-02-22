import { Button, ColorInput, Modal, TextInput } from "@mantine/core";
import { useState } from "react";

export function CreateSpaceModal({ isOpen, onClose }: { isOpen: boolean; onClose: () => void}){
   return (
      <Modal opened={isOpen} onClose={()=>{onClose()}} centered title="Create space"
         styles={{
            title: { fontSize: "20px", fontWeight: 600 }, 
         }}
      >
         <div className="space-y-5">
            <TextInput placeholder="Space name" className="" label="Space name" data-autofocus/>
            <ColorInput label="Space color" defaultValue="#fc5656"/>
            <div className="flex gap-2 justify-end">
               <Button color="gray" variant="filled" onClick={()=>onClose()}>Cancel</Button>
               <Button color="violet" variant="filled">Create space</Button>
            </div>
         </div>
      </Modal>
   );
}
