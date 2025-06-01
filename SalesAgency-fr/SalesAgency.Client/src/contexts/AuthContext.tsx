// src/context/AuthContext.jsx
import React, { createContext, useContext, useState } from "react";

export interface AuthContextProps {
  token: string;
  email: string;
  login: (token: string, email: string) => {};
  logout: () => {};
}

const AuthContext = createContext({} as AuthContextProps);

export interface AuthProviderProps {
  children: React.ReactNode
}

export const AuthProvider = ({ children } : AuthProviderProps) => {
  const [token, setToken] = useState(localStorage.getItem("token") || null);
  const [email, setEmail] = useState(localStorage.getItem("email") || null);

  const login = (newToken: string, newEmail: string) => {
    localStorage.setItem("token", newToken);
    localStorage.setItem("email", newEmail);
    setToken(newToken);
    setEmail(newEmail);
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("email");
    setToken(null);
    setEmail(null);
  };

  return (
    <AuthContext.Provider value={{ token, email, login, logout } as AuthContextProps}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
