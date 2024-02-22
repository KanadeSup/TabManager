import { Button, Modal, TextInput } from "@mantine/core";

export function CreateCategoryModal({ isOpen, onClose }: { isOpen: boolean; onClose: () => void }){
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
         <div className="space-y-5">
            <TextInput
               label="Category name"
               placeholder="Category name"
               styles={{
                  input: {
                     backgroundColor: "#191A21",
                     border: "none",
                  },
               }}
            />
            <div className="flex justify-end gap-2">
               <Button color="gray" variant="light" onClick={onClose}>
                  Cancel
               </Button>
               <Button color="violet" variant="filled">Create</Button>
            </div>
         </div>
      </Modal>
   );
}