import * as ReactDOM from 'react-dom/client'
import { Login } from './components/Login'
import "./index.css"
import '@mantine/core/styles.css';
import { MantineProvider } from '@mantine/core';
import { useState } from 'react';
import { Page } from './types/Page';
import { Register } from './components/Register';
import { MainPage } from './components/MainPage';

function App() {
   const [page, setPage] = useState<Page>('login')
   if (page === 'login') {
      return <Login setPage={setPage} />
   }
   if(page === 'register') {
      return <Register setPage={setPage} />
   }
   if(page === 'main') {
      return <MainPage />
   }
   return null
}

const container = document.getElementById('root')
if (container) {
   const root = ReactDOM.createRoot(container)
   root.render(
      <MantineProvider defaultColorScheme="dark">
         <App />
      </MantineProvider>
   )
}
