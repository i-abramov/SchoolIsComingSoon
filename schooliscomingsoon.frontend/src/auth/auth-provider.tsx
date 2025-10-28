import React, { FC, useEffect, useRef, useState, createContext } from 'react';
import { User, UserManager } from 'oidc-client';
import { setAuthHeader, setIdToken, setRefreshToken, setUserId, setUserName, setUserRole } from './auth-headers';
import { signinRedirect } from './user-service';

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
    setId: () => {},
    setName: () => {},
    setRole: () => {},
    setAuth: () => {}
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
        localStorage.getItem('isAuthenticated') === 'true'
    );

    const userManager = useRef<UserManager>(manager);

    useEffect(() => {
        const onUserLoaded = (user: User) => {
            setAuthHeader(user.access_token);
            setIdToken(user.id_token);
            setRefreshToken(user.refresh_token ?? '');
            setUserRole(user.access_token);
            setUserName(user.access_token);
            setUserId(user.access_token);
            localStorage.setItem('isAuthenticated', 'true');

            setId(localStorage.getItem('user_id') || '');
            setRole(localStorage.getItem('user_role') || '');
            setName(localStorage.getItem('user_name') || '');
            setAuth(true);
        };

        const onUserUnloaded = () => {
            setAuthHeader(null);
            setRefreshToken(null);
            setUserRole(null);
            setUserName(null);
            setUserId(null);
            localStorage.setItem('isAuthenticated', 'false');
        };

        const onAccessTokenExpiring = async () => {
            try {
                const newUser = await userManager.current?.signinSilent();
                if (newUser) {
                    console.log('Token silently renewed successfully:', newUser);
                    setAuthHeader(newUser.access_token);
                    setIdToken(newUser.id_token);
                    setRefreshToken(newUser.refresh_token ?? '');
                    localStorage.setItem('access_token', newUser.access_token);
                    localStorage.setItem('refresh_token', newUser.refresh_token ?? '');
                }
            } catch (error) {
                localStorage.setItem('isAuthenticated', 'false');
                await signinRedirect();
            }
        };

        const onAccessTokenExpired = async () => {
            await signinRedirect();
        };

        const onUserSignedOut = async () => {
            localStorage.setItem('isAuthenticated', 'false');
            await signinRedirect();
        };

        userManager.current.events.addUserLoaded(onUserLoaded);
        userManager.current.events.addUserUnloaded(onUserUnloaded);
        userManager.current.events.addAccessTokenExpiring(onAccessTokenExpiring);
        userManager.current.events.addAccessTokenExpired(onAccessTokenExpired);
        userManager.current.events.addUserSignedOut(onUserSignedOut);

        (async () => {
            try {
                const user = await userManager.current?.getUser();
                if (user && !user.expired) {
                    onUserLoaded(user);
                }
            } catch (err) {
                console.error('Failed to restore session:', err);
            }
        })();

        return () => {
            if (userManager.current) {
                userManager.current.events.removeUserLoaded(onUserLoaded);
                userManager.current.events.removeUserUnloaded(onUserUnloaded);
                userManager.current.events.removeAccessTokenExpiring(onAccessTokenExpiring);
                userManager.current.events.removeAccessTokenExpired(onAccessTokenExpired);
                userManager.current.events.removeUserSignedOut(onUserSignedOut);
            }
        };
    }, [manager]);

    return (
        <AuthContext.Provider value={{ role, id, name, isAuthenticated, setId, setName, setRole, setAuth }}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthProvider;