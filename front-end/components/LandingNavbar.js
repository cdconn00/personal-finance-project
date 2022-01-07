import React from 'react';
import styles from '../styles/LandingNavbar.module.css';

function LandingNavbar() {
  return (
    <nav className="sm:mx-10 lg:mx-40 lg:px-40 pb-6 pt-20">
      <div className="container flex flex-wrap justify-between items-center mx-auto">
        <a href="/" className="flex">
          <span
            className={`self-center text-lg font-semibold whitespace-nowrap dark:text-white text-5xl ${styles.brand}`}
          >
            <div className={styles['brand-flip']}>P</div>FP
          </span>
        </a>
        <div id="mobile-menu">
          <ul className="flex mt-4 md:flex-row space-x-14 md:mt-0 md:text-lg md:text-xl xl:text-xl ">
            <li>
              <a href="/" className="text-white font-bold" aria-current="page">
                Home
              </a>
            </li>
            <li>
              <a href="/about" className="text-white font-bold">
                About
              </a>
            </li>
            <li>
              <a href="/login" className="text-white font-bold">
                Log In
              </a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
}

export default LandingNavbar;
