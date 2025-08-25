import React, { FC, useEffect, useRef, useState, createContext } from 'react';
import { User, UserManager } from 'oidc-client';
import { setAuthHeader, setIdToken, setUserId, setUserName, setUserRole } from './auth-headers';

type AuthContextType = {
    role: any;
    id: any;
    name: any;
    isAuthenticated: boolean;
    setId: (userId: any) => void;
    setName: (name: any) => void;
    setRole: (role: any) => void;
    setAuth: (auth: boolean) => void;
};
  
export const AuthContext = createContext<AuthContextType>({
    role: '',
    id: '',
    name: '',
    isAuthenticated: false,
    setId: () => { },
    setName: () => { },
    setRole: () => { },
    setAuth: () => { }
});

type AuthProviderProps = {
    userManager: UserManager;
    children?: React.ReactNode;
};

export const AuthProvider: FC<AuthProviderProps> = ({
        userManager: manager,
        children,
    }): any => {

    const [id, setId] = useState(localStorage.getItem('user_id'));
    const [role, setRole] = useState(localStorage.getItem('user_role'));
    const [name, setName] = useState(localStorage.getItem('user_name'));
    const [isAuthenticated, setAuth] = useState(
        localStorage.getItem('isAuthenticated') ? localStorage.getItem('isAuthenticated') === 'true' : false
    );

    let userManager = useRef<UserManager>();
    useEffect(() => {
        userManager.current = manager;
        const onUserLoaded = (user: User) => {
            console.log('User loaded: ', user);
            setAuthHeader(user.access_token);
            setIdToken(user.id_token);
            setUserRole(user.access_token);
            setUserName(user.access_token);
            setUserId(user.access_token);
            localStorage.setItem('isAuthenticated', 'true');

            let i = localStorage.getItem('user_id');
            let r = localStorage.getItem('user_role');
            let n = localStorage.getItem('user_name');

            setId(i ? i : '');
            setRole(r ? r : '');
            setName(n ? n : '');
            setAuth(true);
        };
        const onUserUnloaded = () => {
            setAuthHeader(null);
            setUserRole(null);
            setUserName(null);
            setUserId(null);
            localStorage.setItem('isAuthenticated', 'false');
            console.log('User unloaded');
        };
        const onAccessTokenExpiring = () => {
            console.log('User token expiring');
        };
        const onAccessTokenExpired = () => {
            console.log('User token expired');
        };
        const onUserSignedOut = () => {
            console.log('User signed out');
        };

        userManager.current.events.addUserLoaded(onUserLoaded);
        userManager.current.events.addUserUnloaded(onUserUnloaded);
        userManager.current.events.addAccessTokenExpiring(onAccessTokenExpiring);
        userManager.current.events.addAccessTokenExpired(onAccessTokenExpired);
        userManager.current.events.addUserSignedOut(onUserSignedOut);

        return function cleanup() {
            if (userManager && userManager.current) {
                userManager.current.events.removeUserLoaded(onUserLoaded);
                userManager.current.events.removeUserUnloaded(onUserUnloaded);
                userManager.current.events.removeAccessTokenExpiring(onAccessTokenExpiring);
                userManager.current.events.removeAccessTokenExpired(onAccessTokenExpired);
                userManager.current.events.removeUserSignedOut(onUserSignedOut);
            }
        };
    }, [manager]);

    return(
        <AuthContext.Provider value={{ role, id, name, isAuthenticated, setId, setName, setRole, setAuth }}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthProvider;