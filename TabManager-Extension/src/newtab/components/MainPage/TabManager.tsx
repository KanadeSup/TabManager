import { useTabOverStore } from "@/newtab/stores/useTabOver";
import { Tab } from "@/newtab/types/Tab";
import { useDroppable } from "@dnd-kit/core";
import { Accordion, Button, Card, Text, TextInput, Title } from "@mantine/core";
import { IconBrandFacebook, IconSearch } from "@tabler/icons-react";
import { CreateCategoryModal } from "./CreateCategoryModal";
import { useState } from "react";

export function TabManager() {
   return (
      <div className="bg-[#121319] flex-grow rounded-l-xl px-5 space-y-5">
         <Header />
         <Toolbar />
         <CategoryList />
      </div>
   );
}
function Header() {
   return (
      <div className="flex items-center justify-between py-3">
         <h1 className="text-[25px] font-bold"> Header name </h1>
         <TextInput
            leftSection={<IconSearch size={16} />}
            placeholder="Search"
            styles={{
               input: {
                  backgroundColor: "#191A21",
                  border: "none",
                  width: "270px",
               },
            }}
         />
      </div>
   );
}
function Toolbar() {
   const [isCreateCategoryModalOpen, setIsCreateCategoryModalOpen] = useState(false);
   return (
      <div className="flex justify-end">
         <Button variant="light"
            onClick={() => setIsCreateCategoryModalOpen(true)}
         >
            Create
         </Button>
         <CreateCategoryModal
            isOpen={isCreateCategoryModalOpen}
            onClose={() => setIsCreateCategoryModalOpen(false)}
         />
      </div>
   );
}
function CategoryList() {
   return (
      <Accordion
         multiple={true}
         styles={{
            item: {
               border: "none",
            },
            control: {
               backgroundColor: "#191A21",
               padding: "0px 30px",
               borderRadius: "10px 10px",
            },
            panel: {
               backgroundColor: "#191A21",
               borderRadius: "0px 0px 10px 10px",
            },
         }}
         className="space-y-5"
      >
         <CategoryCard name="Category one" id="one" />
         <CategoryCard name="Category two" id="two" />
      </Accordion>
   );
}

function CategoryCard({ name, id }: { name: string; id: string }) {
   const { isOver, setNodeRef } = useDroppable({
      id: id,
   });
   const dropOverId = useTabOverStore((state) => state.dropOverId);
   const tabItemData = useTabOverStore((state) => state.tabItemData);
   console.log(dropOverId, dropOverId == id, id);
   return (
      <Accordion.Item
         value={id}
         ref={setNodeRef}
         style={{
            border: isOver ? "1px solid gray" : "none",
            borderRadius: "10px",
         }}
      >
         <Accordion.Control 
            className="group"
         >
            <div className="flex justify-between items-center">
               <h1 className="font-bold text-[20px]"> {name} </h1>
               <div className="mr-10 invisible group-hover:visible">
                  <Button> edit </Button>
               </div>
            </div>
         </Accordion.Control>
         <Accordion.Panel className="flex">
            {dropOverId === id && tabItemData ? <TabCard tab={tabItemData} /> : null}
         </Accordion.Panel>
      </Accordion.Item>
   );
}

function TabCard({ tab }: { tab: Tab }) {
   return (
      <Card
         withBorder
         className="w-[280px] h-[130px] space-y-3 cursor-pointer"
         styles={{
            root: {
               backgroundColor: "#1F2129",
               borderRadius: "5px",
            },
         }}
      >
         <div className="flex items-center gap-2 border-b border-b-gray-700 pb-3">
            <img src={tab.icon} className="w-8 h-8" />
            <h1 className="font-medium text-[15px] truncate"> {tab.title} </h1>
         </div>
         <Text c="dimmed" size="sm">
            Nothing here
         </Text>
      </Card>
   );
}
