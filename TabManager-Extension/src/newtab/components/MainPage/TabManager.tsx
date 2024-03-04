import { useTabOverStore } from "@/newtab/stores/useTabOver";
import { Tab } from "@/types/Tab";
import { useDroppable } from "@dnd-kit/core";
import { Accordion, ActionIcon, Button, Card, Collapse, Text, TextInput, Title } from "@mantine/core";
import { IconBrandFacebook, IconCircleX, IconEdit, IconSearch, IconTrash, IconXboxX } from "@tabler/icons-react";
import { CategoryModalForm, useCategoryModalFormStores } from "./CategoryModalForm";
import { MouseEventHandler, useEffect, useState } from "react";
import { useSelectedSpace } from "@/newtab/stores/useSelectedSpace";
import { getAllCategories } from "@/api/Category/getAllCategories";
import { Category, FullyCategory } from "@/types/Category";
import { useCategoriesStore } from "@/newtab/stores/useCategoriesStore";
import { deleteBookmark } from "@/api/Bookmark/deleteBookmark";
import { Bookmark } from "@/types/Bookmark";
import { deleteCategory } from "@/api/Category/deleteCategory";
import { SpaceModalForm, useSpaceModalFormStores } from "./SpaceModalForm";
import { deleteSpace } from "@/api/space/deleteSpace";
import { useSpaceStore } from "@/newtab/stores/useSpaceStore";
import { IconMonkeySleep } from "@/assets/monkeysleep";

export function TabManager() {
   const selectedSpace = useSelectedSpace(state => state.space);
   if(selectedSpace === null) return <EmptyTabManager /> 
   return (
      <div className="bg-[#121319] flex-grow rounded-l-xl px-5 space-y-5">
         <Header />
         <Toolbar />
         <CategoryList />
         <CategoryModalForm />
         <SpaceModalForm />
      </div>
   );
}

function EmptyTabManager() {
   const setSpaceModalOpen = useSpaceModalFormStores(state => state.setOpen);
   return (
      <div className="flex items-center justify-center h-full w-full bg-[#121319]">
         <div className="flex flex-col items-center gap-5">
            <IconMonkeySleep width={500} heigh={300}/>
            <Title order={1}> Create a space to get started </Title>
            <Button 
               variant="gradient"
               gradient={{ from: 'blue', to: 'cyan', deg: 90 }}
               onClick={() => setSpaceModalOpen({
                  action: "create",
                  isOpen: true
               })}
            >
               Create a space
            </Button>
         </div>
      </div>
   );
}

function Header() {
   const space = useSelectedSpace(state => state.space);
   const setSpaceModalOpen = useSpaceModalFormStores(state => state.setOpen);
   const removeSpaceFromList = useSpaceStore(state => state.deleteSpace);
   const setSelectedSpace = useSelectedSpace(state => state.setSpace);
   const spaces = useSpaceStore(state => state.spaces);
   const handleDeleteSpace = async () => {
      if(!space) return
      const res = await deleteSpace({id: space.id})
      if(res.ok) {
         removeSpaceFromList(space)
         setSelectedSpace(spaces?.find(s => s.id !== space.id) || null)
      }
   }
   return (
      <div className="flex items-center justify-between py-3">
         <div className="flex gap-2 items-center cursor-pointer hover:bg-gray-800 px-2 rounded-lg group"
            onClick={() => {
               if(!space) return
               setSpaceModalOpen({
                  action: "edit",
                  isOpen: true,
                  initValues: {
                     id: space.id,
                     name: space.name,
                     color: space.hexColor
                  }
               })
            }}
         >
            <h1 className="text-[25px] font-bold"> {space?.name} </h1>
            <IconEdit size={18} className="group-hover:visible invisible"/>
         </div>
         <div className="flex gap-2 items-center">
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
            <ActionIcon size={30} color="pink"
               onClick={e=> {
                  e.stopPropagation()
                  handleDeleteSpace()
               }}
            >
               <IconTrash style={{ width: '70%', height: '70%' }} stroke={1.5}/>
            </ActionIcon>
         </div>
      </div>
   );
}

function Toolbar() {
   const { setOpen : setCategoryModalOpen } = useCategoryModalFormStores();
   return (
      <div className="flex justify-end">
         <Button variant="light"
            onClick={() => setCategoryModalOpen({
               action: "create",
               isOpen: true,
            })}
         >
            Create
         </Button>
      </div>
   );
}
function CategoryList() {
   const space = useSelectedSpace(state => state.space);
   const setCategories = useCategoriesStore(state => state.setCategories);
   const categories = useCategoriesStore(state => state.categories);
   useEffect(() => {
      if(space === null) {
         setCategories(null)
         return
      }
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
            categories ? 
               categories
                  .sort((a, b) => {
                     const date1 = new Date(a.creationTime!);
                     const date2 = new Date(b.creationTime!);
                     return date2.getTime() - date1.getTime();
                  })
                  .map(category => (
                     <CategoryCard key={category.id} category={category} />
                  )) 
               : null
         }
      </div>
   );
}

function CategoryCard({ category }: { category: FullyCategory }) {
   const { isOver, setNodeRef } = useDroppable({
      id: category.id,
   });
   const setCategories = useCategoriesStore(state => state.setCategories);
   const categories = useCategoriesStore(state => state.categories);

   async function handleDeleteCategory(cateId: string) {
      if(!categories) return 
      const res = await deleteCategory({id: cateId})
      if(res.ok) {
         setCategories(categories.filter(cate => cateId !== cate.id)) 
         return
      }
   }

   const {setOpen: setCategoryModalOpen} = useCategoryModalFormStores()
   const [isOpened, setIsOpened] = useState(false);
   const dropOverId = useTabOverStore((state) => state.dropOverId);
   const tabItemData = useTabOverStore((state) => state.tabItemData);
   return (
      <div className="border border-gray-600 rounded-lg px-5 py-2 bg-[#191A21] w-full" ref={setNodeRef}>
         <div
            onClick={() => setIsOpened(!isOpened)}
            className="cursor-pointer select-none flex justify-between items-center group"
         >
            <h1 className="text-2xl font-bold"> {category.name} </h1>
            <div className="flex gap-3 items-center group-hover:visible invisible">
               <ActionIcon size={25} color="blue" >
                  <IconEdit style={{ width: '70%', height: '70%' }} stroke={1.5}
                     onClick={e=> {
                        e.stopPropagation()
                        setCategoryModalOpen({
                           action: "edit",
                           isOpen: true,
                           initValues: {
                              id: category.id,
                              name: category.name
                           }
                        })
                     }}
                  />
               </ActionIcon>
               <ActionIcon size={25} color="pink"
                  onClick={e=> {
                     e.stopPropagation()
                     handleDeleteCategory(category.id)
                  }}
               >
                  <IconTrash style={{ width: '70%', height: '70%' }} stroke={1.5}/>
               </ActionIcon>
            </div>
         </div>
         <Collapse in={isOpened || dropOverId === category.id}>
            <div className="py-3 flex gap-5 flex-wrap">
               {category.bookmarks.map((bookmark) => (
                  <TabCard 
                     key={bookmark.id} 
                     tab={{
                        id: bookmark.id,
                        title: bookmark.title,
                        url: bookmark.url,
                        icon: bookmark.webIcon,
                        mineType: bookmark.iconMineType
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
   mineType?: string;
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
            <img 
               src={tab.mineType ? `data:${tab.mineType};base64,${tab.icon}` : tab.icon}
               className="w-8 h-8" 
            />
            <h1 className="font-medium text-[15px] truncate"> {tab.title} </h1>
         </div>
         <Text c="dimmed" size="sm">
            Nothing here
         </Text>
      </Card>
   );
}