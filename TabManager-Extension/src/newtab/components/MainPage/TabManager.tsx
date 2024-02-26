import { useTabOverStore } from "@/newtab/stores/useTabOver";
import { Tab } from "@/types/Tab";
import { useDroppable } from "@dnd-kit/core";
import { Accordion, Button, Card, Collapse, Text, TextInput, Title } from "@mantine/core";
import { IconBrandFacebook, IconCircleX, IconSearch, IconXboxX } from "@tabler/icons-react";
import { CreateCategoryModal } from "./CreateCategoryModal";
import { useEffect, useState } from "react";
import { useSelectedSpace } from "@/newtab/stores/useSelectedSpace";
import { getAllCategories } from "@/api/Category/getAllCategories";
import { Category, FullyCategory } from "@/types/Category";
import { useCategoriesStore } from "@/newtab/stores/useCategoriesStore";
import { deleteBookmark } from "@/api/Bookmark/deleteBookmark";
import { Bookmark } from "@/types/Bookmark";

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
   const setCategories = useCategoriesStore(state => state.setCategories);
   const categories = useCategoriesStore(state => state.categories);
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
               <CategoryCard key={category.id} category={category} />
            )) : null
         }
      </div>
   );
}

function CategoryCard({ category }: { category: FullyCategory }) {
   const { isOver, setNodeRef } = useDroppable({
      id: category.id,
   });
   const [isOpened, setIsOpened] = useState(false);
   const dropOverId = useTabOverStore((state) => state.dropOverId);
   const tabItemData = useTabOverStore((state) => state.tabItemData);
   return (
      <div className="border border-gray-600 rounded-lg px-5 py-2 bg-[#191A21] w-full" ref={setNodeRef}>
         <div
            onClick={() => setIsOpened(!isOpened)}
            className="cursor-pointer select-none"
         >
            <h1 className="text-2xl font-bold"> {category.name} </h1>
         </div>
         <Collapse in={isOpened}>
            <div className="py-3 flex gap-5 flex-wrap">
               {category.bookmarks.map((bookmark) => (
                  <TabCard 
                     key={bookmark.id} 
                     tab={{
                        id: bookmark.id,
                        title: bookmark.title,
                        url: bookmark.url,
                        icon: "data:image/png;base64,"+bookmark.webIcon
                     }} 
                  />
               ))}
               {dropOverId === category.id && tabItemData ? <TabCard tab={{
                  id: -1,
                  title: tabItemData.title,
                  url: tabItemData.url,
                  icon: tabItemData.icon
               }} /> : null}
            </div>
         </Collapse>  
      </div>  
   );
}

type TabCardProps = {
   id: string | -1;
   title?: string;
   url: string;
   icon?: string;
};
function TabCard({ tab }: { tab: TabCardProps }) {
   const popBookmark = useCategoriesStore(state => state.deleteBookmark)
   const appendBookmark = useCategoriesStore(state => state.addBookmark)
   async function handleDelete() {
      if(tab.id === -1) return
      popBookmark(tab.id)
      const res = await deleteBookmark({
         id: tab.id
      }) 
      if(!res.ok) {
         appendBookmark(tab.id, {
            id: tab.id,
            title: tab.title,
            url: tab.url,
            webIcon: tab.icon
         })
         return
      }
   }
   return (
      <Card
         withBorder
         className="w-[280px] h-[130px] space-y-3 cursor-pointer relative group"
         styles={{
            root: {
               backgroundColor: "#1F2129",
               borderRadius: "5px",
            },
         }}
         onClick={() => {
            browser.tabs.create({ url: tab.url });
         }}
      >
         <IconCircleX className="w-6 h-6 absolute top-2 right-2 invisible group-hover:visible hover:stroke-red-500"
            onClick={e=> {
               e.stopPropagation()
               handleDelete()
            }}
         />
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