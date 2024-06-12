import React, { useEffect, useState } from "react";
import DefaultTemplate from "../components/templates/DefaultTemplate";
import { Link, useParams } from "react-router-dom";
import axios from "axios";

const NftDetail = () => {
    const nftId = useParams().id;
    const [nft, setNft] = useState(null);
    const [owner, setOwner] = useState(null);

    useEffect(() => {
        const fetchNft = async () => {
            console.log(nftId)

            var nftResponse = await axios.get(`http://localhost:5066/nft/${nftId}`)
            var data = nftResponse.data
            setNft(data)
            console.log(data)

            var userResponse = await axios.get(`http://localhost:5066/users/${data.ownerId}`)
            setOwner(userResponse.data)
            console.log(userResponse)
        };

        fetchNft();
    }, [nftId]);

    const handleBuyNft = async () => {
        try {
            var newOwnerId = localStorage.getItem("userId")
            var res = await axios.put(`http://localhost:5066/buynft/${nftId}/${newOwnerId}`)
            console.log(res.data)
            alert('NFT bought successfully!');

        } catch (err) {
            console.error('Error buying NFT:', err);
            alert('Failed to buy NFT.');
        }
    };

    if (!nft || !owner) {
        return <DefaultTemplate><div>Loading...</div></DefaultTemplate>;
    }

    return (
        <DefaultTemplate>
            <div className="flex min-h-screen bg-gray-900 text-white p-8">
                <div className="w-1/2 pr-4">
                    <img src={`data:image/png;base64,${nft.imageData}`} alt={nft.name} className="w-full rounded-md mb-4" />
                </div>
                <div className="w-1/2 pl-4">
                    <h1 className="text-3xl font-bold mb-4">{nft.name}</h1>
                    <p className="text-gray-400 mb-2">Minted on {nft.mintedDate}</p>
                    <div className="mb-4">
                        <h2 className="text-xl font-bold mb-2">DETAILS</h2>
                        <p className="text-gray-400">{nft.description}</p>
                    </div>
                    <div className="flex items-center mb-4">
                        <span className="mr-2 text-gray-400">OWNER</span>
                        <Link to={`/profile/${owner.id}`}>
                            <span className="bg-gray-800 text-white px-2 py-1 rounded">{owner.name}</span>
                        </Link>
                    </div>

                    {localStorage.getItem("userId") !== owner.id && (
                        <button 
                            onClick={handleBuyNft} 
                            className="mt-4 bg-blue-600 text-white px-4 py-2 rounded-full hover:bg-blue-500 transition-colors duration-300"
                        >
                            Buy NFT
                        </button>
                    )}

                </div>
            </div>
        </DefaultTemplate>
    )
}

export default NftDetail;
