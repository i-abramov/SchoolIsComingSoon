import { UserManager, UserManagerSettings } from 'oidc-client';

const userManagerSettings: UserManagerSettings = {
    client_id: 'schooliscomingsoon-web-app',
    redirect_uri: 'http://localhost:3000/signin-oidc',
    response_type: 'code',
    scope: 'openid profile SchoolIsComingSoonWebAPI',
    authority: 'https://localhost:44344/',
    post_logout_redirect_uri: 'http://localhost:3000/signout-oidc',
};

const userManager = new UserManager(userManagerSettings);

export const signinRedirect = () => userManager.signinRedirect();

export const signinRedirectCallback = () => 
    userManager.signinRedirectCallback();

export const signoutRedirect = (args?: any) => {
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirect(args);
};

export const signoutRedirectCallback = () => {
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirectCallback();
};

export default userManager;