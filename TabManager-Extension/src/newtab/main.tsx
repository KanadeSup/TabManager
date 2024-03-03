import * as ReactDOM from 'react-dom/client'
import { Login } from './components/Login'
import "./index.css"
import '@mantine/core/styles.css';
import { MantineProvider } from '@mantine/core';
import { Register } from './components/Register';
import { MainPage } from './components/MainPage';
import {
   RouterProvider,
   createMemoryRouter,
 } from "react-router-dom";
import { routes } from './Routes';


 
const router = createMemoryRouter(routes, {
   initialEntries: ["/Login"],
   initialIndex: 0,
});

function App() {
   return (
      <MantineProvider defaultColorScheme="dark">
         <RouterProvider router={router} />
      </MantineProvider>
   )
}

const container = document.getElementById('root')
if (container) {
   const root = ReactDOM.createRoot(container)
   root.render(
      <App />
   )
}
