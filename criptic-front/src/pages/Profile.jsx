import React, { useEffect, useState } from "react";
import DefaultTemplate from "../components/templates/DefaultTemplate";
import { Link, useParams } from "react-router-dom";
import axios from "axios";

const Profile = () => {
    const userId = useParams().id;
    const [userData, setUserData] = useState(null);
    const [userNFTs, setUserNFTs] = useState([]);
    const [walletData, setWalletData] = useState(null);
    const [amount, setAmount] = useState(0);

    const [isCurrentUser, setIsCurrentUser] = useState(false);



    useEffect(() => {
        const fetchData = async () => {
            console.log(userId)

            var nftsRes = await axios.get(`http://localhost:5066/usernfts/${userId}`)
            setUserNFTs(nftsRes.data)

            var walletRes = await axios.get(`http://localhost:5066/walletModel/${userId}`)
            setWalletData(walletRes.data);

            var userRes = await axios.get(`http://localhost:5066/users/${userId}`)
            setUserData(userRes.data)

            const localUserId = localStorage.getItem('userId');
            setIsCurrentUser(localUserId === userId);

            console.log(walletData)
            console.log(userData)
        };

        fetchData();
    }, [userId]);


    const handleSubmit = async (e) => {
        e.preventDefault();
        var requestBody = {
            WalletId: walletData.id,
            Amount: amount
        }
        axios.put(`http://localhost:5066/wallet/replenish`, requestBody, {
            Headers: {
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "*"
            }
        }
        )
            .then(response => {
            if (response.status === 200) {
                console.log("Баланс пополнен!");
                window.location.reload();
            } else {
                console.error("Не удалось пополнить баланс.", response.status);
            }
            })
            .catch(error => {
            console.error("Ошибка при отправке запроса.", error);
        });
    }

    if (!userData || !walletData || !userNFTs) {
        return (
            <DefaultTemplate>
                <div>Loading...</div>
            </DefaultTemplate>
        );
    }

    return (
        <DefaultTemplate>
            <div>
                <h1 className="text-3xl font-bold mb-4">Profile</h1>
                <div className="bg-gray-800 p-6 rounded-md flex flex-col items-center space-y-4">
                    <div className="text-center">
                        <p className="text-lg font-bold">{userData.name}</p>
                        <p className="text-gray-400">{userData.email}</p>
                    </div>
                    <div className="text-center">
                        <p className="text-lg font-bold">Сумма на кошельке</p>
                        <p className="text-gray-400">{walletData.balance}</p>
                    </div>
                    {isCurrentUser && (
                        <>

                        {/*
                            <Link 
                                to="/edit-profile/" 
                                className="mt-4 bg-blue-600 text-white px-4 py-2 rounded-full hover:bg-blue-500 transition-colors duration-300"
                            >
                                Edit Profile
                            </Link>
                        */}
                        
                        <form
                            className="flex flex-col items-center space-y-4"
                            onSubmit={handleSubmit}
                        >
                            <label 
                                className="text-lg font-bold"
                            >
                                Сумма пополнения:
                            </label>
                            <input type="number" id="amount" name="amount" min="0" required
                            className="mt-4 bg-blue-600 text-white px-4 py-2 rounded-full"
                            onChange={(e) => setAmount(e.target.value)}
                        />
                            <button 
                                type="submit"
                                className="mt-4 bg-blue-600 text-white px-4 py-2 rounded-full hover:bg-blue-500 transition-colors duration-300"
                            >
                                Пополнить баланс
                            </button>
                        </form>
                        </>
                    )}
                </div>
                <div className="mt-8">
                    <h2 className="text-2xl font-bold mb-4">{userData.name}'s NFTs</h2>
                    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
                        {userNFTs.map(nft => (
                            <div key={nft.id} className="bg-gray-800 p-4 rounded-md">
                                <img src={`data:image/png;base64,${nft.imageData}`} alt={nft.title} className="w-full h-48 object-cover rounded-md mb-2" />
                                <h3 className="text-lg font-bold">{nft.name}</h3>
                                <p className="text-gray-400">{nft.price}</p>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </DefaultTemplate>
    );
};

export default Profile;