import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

const Header = () => {
  const [isProfileMenuOpen, setProfileMenuOpen] = useState(false);
  const [user, setUser] = useState(null);
  const [isAuth, setAuth] = useState(localStorage.getItem("userId"))

  const toggleProfileMenu = () => {
    setProfileMenuOpen(!isProfileMenuOpen);
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    setUser(null);
  };

  useEffect(() => {
    const fetchUserData = async () => {
      const token = localStorage.getItem('token');
      if (token) {
        const userId = isAuth;
        setUser(userId)
      }
    };

    fetchUserData();
  }, []);

  return (
    <header className="flex justify-between items-center p-4 bg-gray-900 text-white">
      <div className="flex items-center space-x-4">
        <img src="/path/to/logo.png" alt="Criptic Logo" className="h-8 mr-2" />
        <span className="text-lg font-bold">CRIPTIC</span>
        <nav className="flex items-center space-x-4 ml-8">
          <Link to="/" className="text-white hover:text-gray-400 transition-colors duration-300">HOME</Link>
          <Link to="/search-nft" className="text-white hover:text-gray-400 transition-colors duration-300">SEARCH NFT</Link>
          {isAuth  && (
            <Link to="/create-nft" className="text-white hover:text-gray-400 transition-colors duration-300">CREATE NFT</Link>
          )}
          <Link to="/search-nft" className="text-white hover:text-gray-400 transition-colors duration-300">NFT DETAIL</Link>
          <div className="relative inline-block text-left">
            <button
              onClick={toggleProfileMenu}
              className="hover:text-gray-400 focus:outline-none"
            >
              PROFILE 
            </button>
            {isProfileMenuOpen && (
              <div className="absolute right-0 mt-2 w-48 bg-gray-800 text-white rounded-md shadow-lg z-10">
                {isAuth  && (
                  <Link to={`/profile/${user}`} className="block px-4 py-2 hover:bg-gray-700">Profile</Link>
                )}
                <Link to="/auth" className="block px-4 py-2 hover:bg-gray-700">Login</Link>
                <Link to="/auth/sign-up" className="block px-4 py-2 hover:bg-gray-700">Register</Link>
              </div>
            )}
          </div>
        </nav>
      </div>
      <div className="flex items-center space-x-4">
        <Link to="/notifications" className="text-white hover:text-gray-400 transition-colors duration-300">&#9889;</Link>
        {user ? (
          <div className="flex items-center space-x-2">
            <Link to={"/"}>
              <button 
                onClick={handleLogout} 
                className="bg-red-600 text-white px-4 py-2 rounded-full hover:bg-red-500 transition-colors duration-300"
              >
                Logout
              </button>
            </Link>
          </div>
        ) : (
          <Link to="/auth/sign-up" className="bg-gray-700 text-white px-4 py-2 rounded-full hover:bg-gray-600 transition-colors duration-300">CONNECT</Link>
        )}
      </div>
    </header>
  );
};

export default Header;