import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import userManager from './auth/user-service';
import AuthProvider from './auth/auth-provider';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <AuthProvider userManager={userManager}>
    <App/>
  </AuthProvider>
);