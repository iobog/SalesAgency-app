// src/pages/Login.jsx
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";
import { loginRequest } from "../lib/api";
import type { LoginRequest, LoginResponse } from "../lib/interfaces";

const Login = () => {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [form, setForm] = useState({ user: "", pass: "" });
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    setErrorMessage(null);

    const body: LoginRequest = {
      user: form.user,
      pass: form.pass
    }
    loginRequest(body).then((resp: LoginResponse) => {
      login(resp.token, form.user);
      navigate("/products");
    })
    .catch((e: Error) => setErrorMessage(e.message))
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <form onSubmit={handleSubmit}>
        <input placeholder="User" onChange={(e) => setForm({ ...form, user: e.target.value })} className="block mb-2 p-2 border" />
        <input placeholder="Password" type="password" onChange={(e) => setForm({ ...form, pass: e.target.value })} className="block mb-2 p-2 border" />
        <button type="submit" className="bg-blue-500 text-white px-4 py-2">Login</button>
      </form>
      {errorMessage && <div className="text-red-400 mt-2">{errorMessage}</div>}
    </div>
  );
};

export default Login;
