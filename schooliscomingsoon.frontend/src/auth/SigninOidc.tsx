import { useEffect, FC } from 'react';
import { useNavigate } from 'react-router-dom';
import { signinRedirectCallback } from './user-service';

const SigninOidc: FC<{}> = () => {
    const navigate = useNavigate();
    useEffect(() => {
        async function signinAsync() {
            try {
                await signinRedirectCallback();
                navigate('/');
            } catch (error) {
                // Could not load source 'localhost꞉44357/_framework/aspnetcore-browser-refresh.js': Source not found.
            }
        }
        signinAsync();
    }, [navigate]);
    return <div>Redirecting...</div>;
};

export default SigninOidc;