import { addCategory } from "@/api/Category/addCategory";
import { useSelectedSpace } from "@/newtab/stores/useSelectedSpace";
import { Button, Modal, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";

export function CreateCategoryModal({ isOpen, onClose }: { isOpen: boolean; onClose: () => void }){
   const form = useForm({
      initialValues: {
         name: "",
      },
      validate: {
         name: (value) => (value.length > 0 ? null : "Name is required"),
      },
   });
   const space = useSelectedSpace(state => state.space);
   const handleSubmit = async ({ name }: {name: string}) => {
      if(!space?.id) return
      const res = await addCategory({name, spaceId: space.id})
      if(res.ok) onClose()
   }
   return (
      <Modal
         onClose={onClose}
         opened={isOpen}
         title="Create category"
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
               <Button color="gray" variant="light" onClick={onClose}>
                  Cancel
               </Button>
               <Button color="violet" variant="filled" type="submit">Create</Button>
            </div>
         </form>
      </Modal>
   );
}