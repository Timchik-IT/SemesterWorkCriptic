import React, { useEffect, useState } from "react";
import DefaultTemplate from "../components/templates/DefaultTemplate";
import axios from "axios";

const NotificationsPage = () => {
    const [transactions, setTransactions] = useState([]);
    const [walletId, setWallet ] = useState("")

    const fetchTransactions = async () => {
        var walletRes = await axios.get(`http://localhost:5066/wallet/${localStorage.getItem("userId")}`)
        var data = walletRes.data
        console.log("Wallet:", data)
        setWallet(walletRes.data);
        
        var transactionsRes = await axios.get(`http://localhost:5066/transactions/${walletId}`)
        if (transactionsRes.status === 200){
            setTransactions(transactionsRes.data)
            console.log(transactionsRes.data)
            console.log("Transactions getting")
        }
    }

    useEffect(() => {
        fetchTransactions();
    }, [walletId]); 


    if (!walletId || !transactions) {
        return <DefaultTemplate><div>Loading...</div></DefaultTemplate>;
    }

    return (
        <DefaultTemplate>
            <div className="p-8">
                <h1 className="text-3xl font-bold mb-4">Your Notifications</h1>
                <div className="space-y-4">
                    {transactions.map((transaction, index) => (
                        <div key={index} className="bg-gray-800 p-4 rounded-md flex items-center justify-between">
                            <div>
                                <p className="text-lg font-bold">{transaction.description}</p>
                                <p>{transaction.operation}</p>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </DefaultTemplate>
    );
};

export default NotificationsPage;