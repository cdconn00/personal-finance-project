import '../styles/globals.css';
import React from 'react';

function MyApp({ Component, pageProps }) {
  return <Component pageProps={pageProps} />;
}

export default MyApp;
