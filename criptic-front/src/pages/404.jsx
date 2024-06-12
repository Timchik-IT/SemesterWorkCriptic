import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const NotFound = () => {
  const navigate = useNavigate();

  useEffect(() => {
    setTimeout(() => {
      navigate("/");
    }, 3000);
  }, [navigate]);

  return (
    <div className="bg-gray-900 text-white flex flex-col justify-center items-center h-screen">
      <h1 className="text-4xl font-bold mb-4">404</h1>
      <p className="text-lg mb-8">Page not found.</p>
      <p>Redirecting to home page in 3 seconds...</p>
    </div>
  );
};

export default NotFound;