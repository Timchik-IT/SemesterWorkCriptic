import React, { useEffect, useState } from 'react';
import DefaultTemplate from '../components/templates/DefaultTemplate';
import { Link } from 'react-router-dom';

const ExploreNfts = () => {
    const [searchQuery, setSearchQuery] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const nftsPerPage = 9;
    const [nftItems, setNftItems] = useState([]);
    const [loading, setLoading] = useState(false);

    const indexOfLastNft = currentPage * nftsPerPage;
    const indexOfFirstNft = indexOfLastNft - nftsPerPage;
    const currentNfts = Array.isArray(nftItems) ? nftItems.slice(indexOfFirstNft, indexOfLastNft) : [];

    const pageNumbers = [];
    for (let i = 1; i <= Math.ceil(nftItems.length / nftsPerPage); i++) {
        pageNumbers.push(i);
    }

    const handleSearchChange = (e) => {
        setSearchQuery(e.target.value);
        console.log("Search like", e.target.value);
    };

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
        window.scrollTo(0, 0);
    };

    const fetchNfts = async (searchTerm) => {
        setLoading(true);
        try {
            const response = await fetch(`http://localhost:5066/nfts${searchQuery ? `?search=${searchQuery}` : ''}`);
            const data = await response.json();
            if (Array.isArray(data)) {
                setNftItems(data);
            } else {
                setNftItems([]);
                console.error('Received data is not an array:', data);
            }
        } catch (error) {
            console.error('Error fetching NFTs:', error);
            setNftItems([]);
        }
        setLoading(false);
    };

    useEffect(() => {
        fetchNfts(searchQuery);
    }, [searchQuery]);

    return (
        <DefaultTemplate>
            <div className="flex min-h-screen bg-gray-900 text-white">
                <div className="w-1/4 p-8 bg-gray-800">
                    <h2 className="text-2xl font-bold mb-4">Filter</h2>
                    <div className="mb-4">
                        <label className="block text-sm font-medium mb-2">Search by name</label>
                        <input
                            type="text"
                            value={searchQuery}
                            onChange={handleSearchChange}
                            className="w-full p-2 rounded-md bg-gray-700 border border-gray-600"
                            placeholder="Enter NFT name"
                        />
                    </div>
                </div>
                <div className="w-3/4 p-8">
                    <h2 className="text-3xl font-bold mb-8">NFT Shop</h2>
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
                        {loading ? (
                            <h3>Loading...</h3>
                        ) : (
                            currentNfts.map((nft, index) => (
                                <div key={index} className="bg-gray-800 p-4 rounded-md">
                                    <img 
                                        src={`data:image/png;base64,${nft.imageData}`} 
                                        alt={nft.name} 
                                        className="w-full h-64 object-cover rounded-lg mb-4"
                                    />
                                    <h3 className="text-xl font-bold">{nft.name}</h3>
                                    <p className="text-gray-400">Price: {nft.price} ETH</p>
                                    <Link to={`/nft-detail/${nft.id}`}> SHOW DETAILS</Link>
                                </div>
                            ))
                        )}
                    </div>
                    <div className="mt-8 flex justify-center">
                        <ul className="flex space-x-2">
                            {pageNumbers.map(number => (
                                <li 
                                    key={number} 
                                    className={`px-4 py-2 border ${currentPage === number ? 'bg-blue-600' : 'bg-gray-800'} rounded-md cursor-pointer`} 
                                    onClick={() => handlePageChange(number)}>
                                    {number}
                                </li>
                            ))}
                        </ul>
                    </div>
                </div>
            </div>
        </DefaultTemplate>
    );
};

export default ExploreNfts;
