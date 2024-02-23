import { addSpace } from "@/api/space/addSpace";
import { Button, ColorInput, Modal, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useState } from "react";

export function CreateSpaceModal({ isOpen, onClose }: { isOpen: boolean; onClose: () => void}){
   const form = useForm({
      initialValues: {
         name: "",
         hexColor: "#fc5656",
      },
      validate: {
         name: (value) => (value.length > 0 ? null : "Name is required"),
         hexColor: (value) => (/^#[0-9a-fA-F]{6}$/.test(value) ? null : "Invalid color"),
      },
   });
   const handleSubmit = async ({name, hexColor}: {name: string, hexColor: string}) => {
      const res = await addSpace({name, hexColor})
   }
   return (
      <Modal opened={isOpen} onClose={()=>{onClose()}} centered title="Create space"
         styles={{
            title: { fontSize: "20px", fontWeight: 600 }, 
         }}
      >
         <form onSubmit={form.onSubmit((values) => handleSubmit(values))}>
            <div className="space-y-5">
               <TextInput placeholder="Space name" className="" label="Space name" data-autofocus
                  {...form.getInputProps("name")}
               />
               <ColorInput label="Space color"
                  {...form.getInputProps("hexColor")}
               />
               <div className="flex gap-2 justify-end">
                  <Button color="gray" variant="filled" onClick={()=>onClose()}>Cancel</Button>
                  <Button color="violet" variant="filled" type="submit">Create space</Button>
               </div>
            </div>
         </form>
      </Modal>
   );
}
