import React, {  useState } from "react";
import DefaultTemplate from "../components/templates/DefaultTemplate";

const EditProfile = () => {
    const [userData, setUserData] = useState({
        name: ""   
    });
    const [selectedFile, setSelectedFile] = useState(null);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setUserData({ ...userData, [name]: value });
    };

    const handleFileChange = (e) => {
        setSelectedFile(e.target.files[0]);
    };

    const handleSubmit = async (e) => {
        
    };

    return (
        <DefaultTemplate>
            <div>
                <h1 className="text-3xl font-bold mb-4">Edit Profile</h1>
                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label className="block text-lg font-medium mb-2" htmlFor="name">Name</label>
                        <input
                            type="text"
                            id="name"
                            name="name"
                            value={userData.name}
                            onChange={handleInputChange}
                            className="w-full p-2 bg-gray-700 rounded-md text-white"
                            required
                        />
                    </div>
                    <div>
                        <label className="block text-lg font-medium mb-2" htmlFor="profilePicture">Profile Picture</label>
                        <input
                            type="file"
                            id="profilePicture"
                            onChange={handleFileChange}
                            className="w-full p-2 bg-gray-700 rounded-md text-white"
                        />
                        {userData.profilePictureUrl && (
                            <img
                                src={userData.profilePictureUrl}
                                alt="Profile"
                                className="w-32 h-32 rounded-full object-cover mt-4"
                            />
                        )}
                    </div>
                    <button
                        type="submit"
                        className="bg-blue-600 text-white px-4 py-2 rounded-full hover:bg-blue-500 transition-colorsduration-300"
                    >
                        Save Changes
                    </button>
                </form>
            </div>
        </DefaultTemplate>
    );
};

export default EditProfile;