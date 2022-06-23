import { OidcProvider, OidcSecure } from '@axa-fr/react-oidc';
import React from 'react';
import ReactDOM from 'react-dom/client';
import { App } from './app';
import { ReactComponent as Loading } from "./assets/images/loading.svg";
import './index.css';
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <OidcProvider
    configuration={{
      client_id: "spa",
      redirect_uri: `${window.location.origin}/authentication/callback`,
      scope: "openid profile",
      authority: "https://localhost:44310"
    }}
    loadingComponent={() => <Loading />}
    authenticatingComponent={() => <Loading />}
  >
    <React.StrictMode>
      <OidcSecure>
        <App />
      </OidcSecure>
    </React.StrictMode>
  </OidcProvider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
