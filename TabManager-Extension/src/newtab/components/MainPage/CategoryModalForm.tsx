import { addCategory } from "@/api/Category/addCategory";
import { updateCategory } from "@/api/Category/updateCategory";
import { useCategoriesStore } from "@/newtab/stores/useCategoriesStore";
import { useSelectedSpace } from "@/newtab/stores/useSelectedSpace";
import { Button, Modal, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useEffect, useState } from "react";
import { create } from "zustand";

function CategoryModalForm(){
   const { action, isOpen, initValues, setClose } = useCategoryModalFormStores()
   const form = useForm({
      initialValues: {
         name: ""
      },
      validate: {
         name: (value) => (value.length > 0 ? null : "Name is required"),
      },
   });
   useEffect(() => {
      if(!initValues) return
      form.setValues({
         name: initValues.name
      })
   }, [initValues])
   const space = useSelectedSpace(state => state.space);
   const {
      addCategory: addCategoryToList,
      updateCategory: updateCategoryFromList,
   } = useCategoriesStore();
   const handleSubmit = async ({ name }: {name: string}) => {
      if(!space?.id) return
      if(action === "create"){
         const res = await addCategory({name, spaceId: space.id})
         if(res.ok) {
            addCategoryToList(res.data)
            setClose()
            form.reset()
         }
      }
      if(action === "edit"){
         const res = await updateCategory({
            id: initValues?.id || "",
            name: name,
         })
         if(res.ok) {
            updateCategoryFromList(res.data)
            setClose()
            form.reset()
         }
      }
   }
   return (
      <Modal
         onClose={setClose}
         opened={isOpen}
         title={action === "create" ? "Create Category" : "Edit Category"}
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

type CategoryModalFormStores = {
   action: "create" | "edit";
   isOpen: boolean;
   initValues?: {
      id?: string;
      name: string;
   };
   setOpen: ({ action, isOpen, initValues }: { 
      action: "create" | "edit", 
      isOpen: boolean, 
      initValues? : { id?: string, name: string }
   }) => void;
   setClose: () => void;
}

const useCategoryModalFormStores = create<CategoryModalFormStores>((set) => ({
   action : "create",
   isOpen: false,
   initValues: undefined,
   setOpen: ({ action, isOpen, initValues }) => set({ action, isOpen, initValues }),
   setClose: () => set({ isOpen: false })
}))
export { useCategoryModalFormStores, CategoryModalForm }