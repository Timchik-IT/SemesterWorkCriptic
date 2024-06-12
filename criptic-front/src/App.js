import './App.css';

import { BrowserRouter, Routes, Route } from "react-router-dom";

import ExploreNfts from './pages/ExploreNfts';
import NftDetail from './pages/NftDetail';
import NotFound from './pages/404';
import Profile from './pages/Profile';
import Login from './pages/Login';
import Registration from './pages/Registration';
import Notifications from './pages/Notifications';
import AdminPage from './pages/AdminPage';
import HomePage from './pages/Home';
import CreateNFT from './pages/CreateNft';
import { useState } from 'react';

function App() {
  const [isAuth, setAuth] = useState(localStorage.getItem("userId"))

  return (
    <BrowserRouter>
      <Routes>
        {isAuth  && (
          <Route path="/">  
            <Route index element={<HomePage />} />
            <Route path="search-nft" element={<ExploreNfts />} />
            <Route path="create-nft" element={<CreateNFT />} />
            <Route path="nft-detail/:id" element={<NftDetail />}/>
            <Route path="profile/:id" element={<Profile />} />
            <Route path="auth" element={<Login />} />
            <Route path="auth/sign-up" element={<Registration />} />
            <Route path="notifications" element={<Notifications />} />
            <Route path="admin" element={<AdminPage />} />
            <Route path="*" element={<NotFound />} />
          </Route>
        )}
        <Route path="/">  
          <Route index element={<HomePage />} />
          <Route path="search-nft" element={<ExploreNfts />} />
          <Route path="nft-detail/:id" element={<NftDetail />}/>
          <Route path="profile/:id" element={<Profile />} />
          <Route path="auth" element={<Login />} />
          <Route path="auth/sign-up" element={<Registration />} />
          <Route path="admin" element={<AdminPage />} />
          <Route path="*" element={<NotFound />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
