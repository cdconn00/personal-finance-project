import '../styles/globals.css';
import React from 'react';
import { config } from '@fortawesome/fontawesome-svg-core';
import '@fortawesome/fontawesome-svg-core/styles.css';

config.autoAddCss = false;

function MyApp({ Component, pageProps }) {
  return <Component pageProps={pageProps} />;
}

export default MyApp;
