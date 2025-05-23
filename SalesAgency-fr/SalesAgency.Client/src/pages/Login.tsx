// src/pages/Login.jsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";

const Login = () => {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [form, setForm] = useState({ user: "", pass: "" });

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    // fake login
    if (form.user === "admin" && form.pass === "admin") {
      login("mocked-bearer-token");
      navigate("/products");
    }
    
  };

  return (
    <form onSubmit={handleSubmit} className="p-4 max-w-md mx-auto">
      <input placeholder="User" onChange={(e) => setForm({ ...form, user: e.target.value })} className="block mb-2 p-2 border" />
      <input placeholder="Password" type="password" onChange={(e) => setForm({ ...form, pass: e.target.value })} className="block mb-2 p-2 border" />
      <button type="submit" className="bg-blue-500 text-white px-4 py-2">Login</button>
    </form>
  );
};

export default Login;
