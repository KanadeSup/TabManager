import { addSpace } from "@/api/space/addSpace";
import { updateSpace } from "@/api/space/updateSpace";
import { useSpaceStore } from "@/newtab/stores/useSpaceStore";
import { Button, ColorInput, Modal, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useEffect } from "react";
import { create } from "zustand";

type FormValue = {
   name: string;
   color: string
}
function SpaceModalForm() {
   const { action, isOpen, initValues, setClose } = useSpaceModalFormStores()
   const addToSpaceList = useSpaceStore(state => state.addSpace)
   const updateToSpaceList = useSpaceStore(state => state.updateSpace)
   const form = useForm({
      initialValues: {
         name: "",
         color: "#fc5656"
      },
      validate: {
         name: (value) => (value.length > 0 ? null : "Name is required"),
         color: (value) => (value.length > 0 ? null : "Color is required"),
      },
   });
   useEffect(() => {
      if(!initValues) return
      form.setValues({
         name: initValues.name,
         color: initValues.color
      })
   }, [initValues])
   const handleSubmit = async (value: FormValue) => {
      if(action === "create") {
         const res = await addSpace({
            name: value.name,
            hexColor: value.color
         })
         if(res.ok) {
            addToSpaceList(res.data)
            form.reset()
            setClose()
         }
      }
      if(action === "edit") {
         if(!initValues?.id) return
         const res = await updateSpace({
            id: initValues?.id,
            name: value.name,
            hexColor: value.color
         })
         if(res.ok) {
            updateToSpaceList(res.data)
            form.reset()
            setClose()
         }
      }
   }
   return (
      <Modal
         onClose={setClose}
         opened={isOpen}
         title={action== "create" ? "Create Space" : "Edit Space"}
         styles={{
            title: { fontSize: "20px", fontWeight: 600 },
         }}
         centered
      >
         <form className="space-y-5"
            onSubmit={form.onSubmit((values) => {
               handleSubmit(values)
            })}
         >
            <TextInput
               label="Category name"
               placeholder="Category name"
               styles={{
                  input: {
                     backgroundColor: "#191A21",
                     border: "none",
                  },
               }}
               {...form.getInputProps("name")}
            />
            <ColorInput label="Space color" {...form.getInputProps("color")} />
            <div className="flex justify-end gap-2">
               <Button color="gray" variant="light" onClick={setClose}>
                  Cancel
               </Button>
               <Button color="violet" variant="filled" type="submit">
                  {action === "create" ? "Create" : "Update"}
               </Button>
            </div>
         </form>
      </Modal>
   );
}

type SpaceModalFormStores = {
   action: "create" | "edit";
   isOpen: boolean;
   initValues?: {
      id?: string;
      name: string;
      color: string;
   };
   setOpen: ({ action, isOpen, initValues }: { 
      action: "create" | "edit", 
      isOpen: boolean, 
      initValues? : { id?: string, name: string, color: string}
   }) => void;
   setClose: () => void;
}

const useSpaceModalFormStores = create<SpaceModalFormStores>((set) => ({
   action : "create",
   isOpen: false,
   initValues: undefined,
   setOpen: ({ action, isOpen, initValues }) => set({ action, isOpen, initValues }),
   setClose: () => set({ isOpen: false })
}))
export { useSpaceModalFormStores, SpaceModalForm }