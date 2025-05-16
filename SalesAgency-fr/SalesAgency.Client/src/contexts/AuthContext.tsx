// src/context/AuthContext.jsx
import React, { createContext, useContext, useState } from "react";

export interface AuthContextProps {
  token: string;
  login: (token: string) => {};
  logout: () => {};
}

const AuthContext = createContext({} as AuthContextProps);

export interface AuthProviderProps {
  children: React.ReactNode
}

export const AuthProvider = ({ children } : AuthProviderProps) => {
  const [token, setToken] = useState(localStorage.getItem("token") || null);

  const login = (newToken: string) => {
    localStorage.setItem("token", newToken);
    setToken(newToken);
  };

  const logout = () => {
    localStorage.removeItem("token");
    setToken(null);
  };

  return (
    <AuthContext.Provider value={{ token, login, logout } as AuthContextProps}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
