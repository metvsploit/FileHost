import React from "react";
import { BrowserRouter, Routes, Route} from "react-router-dom";
import {Header}  from './components/header';
import {HomePage} from './pages/home-page';
import {DownloadPage} from './pages/download-page';

function App() {
  return (
    <BrowserRouter>
      <Header/>
      <Routes>
        <Route path="/" element={<HomePage/>}/>
        <Route path="/:id" element={<DownloadPage/>}/> 
      </Routes>
    </BrowserRouter>
  );
}

export default App;
