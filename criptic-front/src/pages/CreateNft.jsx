import React, { useState } from 'react';
import axios from 'axios';
import DefaultTemplate from '../components/templates/DefaultTemplate';

const CreateNFT = () => {
    const [file, setFile] = useState(null);
    const [name, setName] = useState('');
    const [price, setPrice] = useState('');
    const [preview, setPreview] = useState(null);

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        setFile(file);

        const reader = new FileReader();
        reader.onloadend = () => {
            setPreview(reader.result);
        };
        reader.readAsDataURL(file);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!file || !name || !price) return alert('All fields are required!');

        const byteArray = await fileToBase64(file);
        const creatorId = localStorage.getItem("userId"); 

        const nftData = {
            ImageData: byteArray,
            Name: name,
            Price: parseFloat(price),
            CreatorId: creatorId
        };

        try {
            const response = await axios.post("http://localhost:5066/createNft", nftData, {
                Headers: {
                    "Content-Type": "application/json"
                }
            })
            console.log('NFT created successfully:', response.data);
        } catch (error) {
            console.error('Error creating NFT:', error);
        }
    };

    const fileToBase64 = (file) => {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = () => {
                resolve(reader.result.split(',')[1]);
            };
            reader.onerror = () => {
                reject(new Error('Error reading file.'));
            };
            reader.readAsDataURL(file);
        });
    };

    return (
        <DefaultTemplate>
            <div className="bg-gray-900 text-white min-h-screen">
                <div className="p-8">
                    <h1 className="text-3xl font-bold mb-6">Create New Item</h1>
                    <form onSubmit={handleSubmit} className="space-y-4">
                        <div className="flex flex-col space-y-2">
                            <label htmlFor="file" className="text-lg">Upload File *</label>
                            <input
                                type="file"
                                id="file"
                                onChange={handleFileChange}
                                className="bg-gray-800 text-gray-400 p-2 rounded"
                            />
                        </div>
                        <div className="flex flex-col space-y-2">
                            <label htmlFor="name" className="text-lg">NFT Name *</label>
                            <input
                                type="text"
                                id="name"
                                value={name}
                                onChange={(e) => setName(e.target.value)}
                                className="bg-gray-800 text-gray-400 p-2 rounded"
                            />
                        </div>
                        <div className="flex flex-col space-y-2">
                            <label htmlFor="price" className="text-lg">Price *</label>
                            <input
                                type="text"
                                id="price"
                                value={price}
                                onChange={(e) => setPrice(e.target.value)}
                                className="bg-gray-800 text-gray-400 p-2 rounded"
                            />
                        </div>
                        <button
                            type="submit"
                            className="bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full"
                        >
                            Create
                        </button>
                    </form>
                    {preview && (
                        <div className="mt-8">
                            <h2 className="text-xl font-bold mb-2">Preview</h2>
                            <img src={preview} alt="NFT Preview" className="rounded-lg shadow-lg" />
                        </div>
                    )}
                </div>
            </div>
        </DefaultTemplate>
    );
};

export default CreateNFT;