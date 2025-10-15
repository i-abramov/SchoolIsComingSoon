import { UserManager, UserManagerSettings, WebStorageStateStore } from 'oidc-client';

const userManagerSettings: UserManagerSettings = {
  client_id: 'schooliscomingsoon-web-app',
  redirect_uri: `${process.env.REACT_APP_CLIENT_URL}/signin-oidc`,
  response_type: 'code',
  scope: 'openid profile SchoolIsComingSoonWebAPI offline_access',
  authority: process.env.REACT_APP_AUTHORITY_URL,
  post_logout_redirect_uri: `${process.env.REACT_APP_CLIENT_URL}/signout-oidc`,
  silent_redirect_uri: `${process.env.REACT_APP_CLIENT_URL}/silent-renew.html`,
  automaticSilentRenew: true,
  includeIdTokenInSilentRenew: true,
  userStore: new WebStorageStateStore({ store: window.localStorage })
};

const userManager = new UserManager(userManagerSettings);

export const signinRedirect = () => userManager.signinRedirect();

export const signinRedirectCallback = () => userManager.signinRedirectCallback();


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