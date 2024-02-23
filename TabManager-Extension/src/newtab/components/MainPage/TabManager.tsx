import { useTabOverStore } from "@/newtab/stores/useTabOver";
import { Tab } from "@/types/Tab";
import { useDroppable } from "@dnd-kit/core";
import { Accordion, Button, Card, Collapse, Text, TextInput, Title } from "@mantine/core";
import { IconBrandFacebook, IconSearch } from "@tabler/icons-react";
import { CreateCategoryModal } from "./CreateCategoryModal";
import { useEffect, useState } from "react";
import { useSelectedSpace } from "@/newtab/stores/useSelectedSpace";
import { getAllCategories } from "@/api/Category/getAllCategories";
import { Category } from "@/types/Category";

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
   const space = useSelectedSpace(state => state.space);
   return (
      <div className="flex items-center justify-between py-3">
         <h1 className="text-[25px] font-bold"> {space?.name} </h1>
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
   const space = useSelectedSpace(state => state.space);
   const [categories, setCategories] = useState<Array<Category>|null>(null);
   useEffect(() => {
      async function fetchCategories() {
         if (!space?.id) return;
         const res = await getAllCategories({ spaceId: space.id });
         if (!res.ok) return;
         setCategories(res.data);
      }
      fetchCategories();
   },[space?.id])
   return (
      <div className="space-y-5">
         {
            categories ? categories.map(category => (
               <CategoryCard key={category.id} name={category.name} id={category.id} />
            )) : null
         }
      </div>
   );
}

function CategoryCard({ name, id }: { name: string; id: string }) {
   const { isOver, setNodeRef } = useDroppable({
      id: id,
   });
   const [isOpened, setIsOpened] = useState(false);
   const dropOverId = useTabOverStore((state) => state.dropOverId);
   const tabItemData = useTabOverStore((state) => state.tabItemData);
   console.log(dropOverId, dropOverId == id, id);
   return (
      <div className="border border-gray-600 rounded-lg px-5 py-2 bg-[#191A21] " ref={setNodeRef}>
         <div
            onClick={() => setIsOpened(!isOpened)}
            className="cursor-pointer select-none"
         >
            <h1 className="text-2xl font-bold"> {name} </h1>
         </div>
         <Collapse in={isOpened}>
            <div className="py-3">
               {dropOverId === id && tabItemData ? <TabCard tab={tabItemData} /> : null}
            </div>
         </Collapse>  
      </div>  
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
