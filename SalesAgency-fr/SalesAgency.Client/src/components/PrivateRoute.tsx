import { Navigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";

export interface PrivateRouteProps {
  children: React.ReactNode
}

export const PrivateRoute = ({ children } : PrivateRouteProps) => {
  const { token } = useAuth();
  return token ? children : <Navigate to="/login" />;
};