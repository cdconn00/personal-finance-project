import Head from 'next/head';
import Image from 'next/image';
import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowRight } from '@fortawesome/free-solid-svg-icons';
import LandingNavbar from '../components/LandingNavbar';
import styles from '../styles/Home.module.css';

export default function Home() {
  return (
    <div>
      <Head>
        <title>Personal Finance Project</title>
        <meta
          name="description"
          content="A way to organize finances and savings."
        />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main className="royal-blue min-h-screen overlay-container overflow-hidden">
        <LandingNavbar />
        <div className="container mx-20 xl:mx-40 xl:px-40 pt-40">
          <p className="text-md mb-2 text-white">Get ready to</p>
          <h1 className="text-7xl text-white font-bold my-5">SAVE</h1>
          <p className="text-lg text-white font-medium max-w-sm mb-3">
            An all in one solution to manage your personal finances — without
            the hassel. Easily manage budgets, savings, transactions and more.
          </p>

          <a href="/register" className={styles['cta-button']}>
            Get Started <FontAwesomeIcon icon={faArrowRight} className="ml-3" />
          </a>

          <div className={styles['image-container']}>
            <Image
              className="valut-image"
              src="/img/vault.svg"
              layout="fill"
              objectFit="contain"
            />
          </div>
        </div>

        <div className={styles['overlay-container']}>
          <div className={styles['triangle-topleft']} />
          <div className={styles.overlay} />
        </div>
      </main>

      <footer className={styles.footer} />
    </div>
  );
}
