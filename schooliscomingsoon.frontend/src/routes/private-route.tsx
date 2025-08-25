import { useContext } from 'react';
import { AuthContext } from '../auth/auth-provider';
import { Navigate, Outlet } from 'react-router-dom';

export function PrivateRoute() {
    const { isAuthenticated } = useContext(AuthContext);
    return isAuthenticated ? <Outlet/> : <Navigate to='/'/>;
};

export default PrivateRoute;