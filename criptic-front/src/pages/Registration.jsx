import axios from "axios";
import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import profile from "../assets/profile.jpg"

const Registration = () => {    
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

    const [formData, setFormData] = useState({
        email: '',
        password: '',
        role: 'User',
        name: ''
    });

    const [errors, setErrors] = useState({
        email: '',
        password: ''
    });

    const nav = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value
        }));
        setErrors(prevErrors => ({
            ...prevErrors,
            [name]: ''
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        let isValid = true;
        const errorsCopy = { ...errors };

        if (!formData.email.includes('@') || !formData.email.includes('.')) {
            errorsCopy.email = 'Invalid email address';
            isValid = false;
        }

        if (formData.password.length < 6) {
            errorsCopy.password = 'Password must be at least 6 characters long';
            isValid = false;
        }

        if (isValid) {
            console.log('Form submitted:', formData);
            
            var userData = {
                Name: formData.name,
                Role: formData.role,
                Email: formData.email,
                Password: formData.password
            }

            try{
                const response = await axios.post("http://localhost:5066/register", userData,
                {
                    headers:{
                        "Content-Type": "application/json"
                    }
                })
                .then((response) => {
                    if (response.status === 200){
                        console.log("Success", response)
                        nav("/auth")
                    }
                })
            } catch (er) {
                console.log(er)
                var error = { ...errors };
                error.email = "Mail is busy"
                setErrors(error)
            }
                
        } else {
            setErrors(errorsCopy);

        }
    };

    return (
        <div className="flex flex-col items-center justify-center h-screen bg-gray-900">
            <div className="max-w-md w-full mx-auto">
                <Link to="/" className="absolute top-4 left-4 text-white hover:text-gray-400 transition-colors duration-300">Home</Link>
                <h2 className="text-2xl font-bold text-white mb-4">Register</h2>
                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label htmlFor="name" className="block text-sm font-medium text-white">Name</label>
                        <input
                            type="text"
                            id="name"
                            name="name"
                            value={formData.name}
                            onChange={handleChange}
                            className="mt-1 p-2 w-full rounded-md bg-gray-800 border border-gray-600 text-white focus:border-indigo-500 focus:ring focus:ring-indigo-200"
                            required
                        />
                    </div>
                    <div>
                        <label htmlFor="email" className="block text-sm font-medium text-white">Email</label>
                        <input
                            type="email"
                            id="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                            className="mt-1 p-2 w-full rounded-md bg-gray-800 border border-gray-600 text-white focus:border-indigo-500 focus:ring focus:ring-indigo-200"
                            required
                        />
                        {errors.email && <p className="mt-1 text-red-500 text-sm">{errors.email}</p>}
                    </div>
                    <div>
                        <label htmlFor="password" className="block text-sm font-medium text-white">Password</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                            className="mt-1 p-2 w-full rounded-md bg-gray-800 border border-gray-600 text-white focus:border-indigo-500 focus:ring focus:ring-indigo-200"
                            required
                        />
                        {errors.password && <p className="mt-1 text-red-500 text-sm">{errors.password}</p>}
                    </div>
                    <button type="submit" className="bg-indigo-500 text-white py-2 px-4 rounded-md hover:bg-indigo-600 transition-colors duration-300 w-full">Sign Up</button>
                </form>
                <p className="mt-4 text-gray-500 text-center">Already have an account? <Link to="/auth" className="text-indigo-500">Sign In</Link></p>
            </div>
        </div>
    );
};

export default Registration;
