import React from "react";
import DefaultTemplate from "../components/templates/DefaultTemplate";

const HomePage = () => {
    return (
        <DefaultTemplate>
            <div className="container mx-auto">
                <div className="text-center mb-12">
                    <h1 className="text-4xl font-bold mb-4">Welcome to Criptic</h1>
                    <p className="text-lg mb-4">
                        Criptic is your one-stop destination for buying, selling, and trading NFTs. 
                        Join our community to explore the world of digital assets and unique collectibles.
                    </p>
                    <p className="text-lg">
                        Dive into a new era of digital ownership with Criptic. Secure, user-friendly, and 
                        packed with features to help you manage and grow your NFT portfolio.
                    </p>
                </div>

                <div className="grid grid-cols-1 md:grid-cols-3 gap-8 mb-12">
                    <div className="bg-gray-800 p-6 rounded-lg shadow-md">
                        <h2 className="text-2xl font-bold mb-4">Discover NFTs</h2>
                        <p className="mb-4">Explore a wide range of NFTs across various categories. Find rare and unique digital assets.</p>
                    </div>

                    <div className="bg-gray-800 p-6 rounded-lg shadow-md">
                        <h2 className="text-2xl font-bold mb-4">Create Your Own NFTs</h2>
                        <p className="mb-4">Mint your own NFTs with our easy-to-use tools. Start creating your digital assets today.</p>
                    </div>

                    <div className="bg-gray-800 p-6 rounded-lg shadow-md">
                        <h2 className="text-2xl font-bold mb-4">Join Our Community</h2>
                        <p className="mb-4">Connect with other NFT enthusiasts. Share, trade, and grow together.</p>
                    </div>
                </div>
            </div>
        </DefaultTemplate>
    )
}

export default HomePage;