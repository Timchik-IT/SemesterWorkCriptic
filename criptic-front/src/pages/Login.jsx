import axios from "axios";
import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

const Login = () => {
    const [formData, setFormData] = useState({
        email: '',
        password: ''
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

            const response = await axios.post("http://localhost:5066/login", formData,
            {
                headers:{
                    "Content-Type": "application/json",
                    "Access-Control-Allow-Origin": "*"
                }
            })
            .then((response) => {
                console.log(response.data)
                if (response.status === 200){
                    const data = response.data
                    localStorage.setItem("token" ,data.token)
                    localStorage.setItem("userId", data.userId)
                    nav("/")
                }
            })

        } else {
            setErrors(errorsCopy);
        }
    };

    return (
        <div className="flex items-center justify-center h-screen bg-gray-900">
            <div className="max-w-md w-full mx-auto">                
                <Link to="/" className="absolute top-4 left-4 text-white hover:text-gray-400 transition-colors duration-300">HOME</Link>
                <h2 className="text-2xl font-bold text-white mb-4">Sign In</h2>
                <form onSubmit={handleSubmit} className="space-y-4">
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
                    <button type="submit" className="bg-indigo-500 text-white py-2 px-4 rounded-md hover:bg-indigo-600 transition-colors duration-300 w-full">Sign In</button>
                </form>
                <p className="mt-4 text-gray-500 text-center">Don't have an account? <Link to="/auth/sign-up" className="text-indigo-500">Register</Link></p>
            </div>
        </div>
    );
};

export default Login
