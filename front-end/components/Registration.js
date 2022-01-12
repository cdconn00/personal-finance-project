import React, { useRef } from 'react';
import { useForm } from 'react-hook-form';
import { LoginIcon } from '@heroicons/react/solid';
import styles from '../styles/LandingNavbar.module.css';

function Registration() {
  const {
    handleSubmit,
    register,
    watch,
    formState: { errors },
  } = useForm();

  // Set up watch to ensure password matches its confirm password
  const password = useRef({});
  password.current = watch('password', '');

  const onSubmit = async (data) => {
    const registrationRequest = {
      Email: data.email,
      FirstName: data.firstName,
      LastName: data.lastName,
      Password: data.password,
    };

    await fetch('https://localhost:7220/api/auth/register', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(registrationRequest),
    })
      .then((res) => res.json())
      .then(
        (result) => {
          console.log(result);
        },
        // Note: it's important to handle errors here
        // instead of a catch() block so that we don't swallow
        // exceptions from actual bugs in components.
        (error) => {
          console.log(error);
        }
      );

    console.log(registrationRequest);
  };

  return (
    <>
      {/*
        This example requires updating your template:

        ```
        <html class="h-full bg-gray-50">
        <body class="h-full">
        ```
      */}
      <div className="shadow overflow-hidden sm:rounded-2xl">
        <div className=" bg-white sm:p-6">
          <div className="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
            <div className="max-w-md w-full space-y-8">
              <div>
                <a href="/" className="flex">
                  <span
                    className={`self-center text-lg font-semibold whitespace-nowrap royal-blue text text-5xl ${styles.brand}`}
                  >
                    <div className={styles['brand-flip']}>P</div>FP
                  </span>
                </a>
              </div>
              <div className="col-span-12 font-bold text-2xl royal-blue text">
                <p>Register</p>
              </div>
              <form
                className="mt-8 space-y-6"
                action="#"
                method="POST"
                onSubmit={handleSubmit(onSubmit)}
              >
                {/* <input */}
                {/*   type="hidden" */}
                {/*   name="remember" */}
                {/*   defaultValue="true" */}
                {/* /> */}
                <div className="rounded-md shadow-sm -space-y-px">
                  <div>
                    <label htmlFor="firstName" className="sr-only">
                      First name
                    </label>
                    <input
                      {...register('firstName', {
                        required: 'Required',
                        // pattern: {
                        //   value: /^[A-Z]+/i,
                        //   message: 'invalid name',
                        // },
                      })}
                      id="firstName"
                      name="firstName"
                      type="text"
                      autoComplete="given-name"
                      className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                      placeholder="First name"
                    />
                    {errors.firstName && errors.firstName.message}
                  </div>
                  <div>
                    <label htmlFor="last-name" className="sr-only">
                      Last name
                    </label>
                    <input
                      {...register('lastName', {
                        required: 'Required',
                        // pattern: {
                        //   value: /^[A-Z]+/i,
                        //   message: 'invalid name',
                        // },
                      })}
                      id="lastName"
                      name="lastName"
                      type="text"
                      autoComplete="family-name"
                      className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                      placeholder="Last name"
                    />
                    {errors.lastName && errors.lastName.message}
                  </div>
                  <div>
                    <label htmlFor="email-address" className="sr-only">
                      Email address
                    </label>
                    <input
                      {...register('email', {
                        required: 'Required',
                        pattern: {
                          value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                          message: 'invalid email address',
                        },
                      })}
                      id="email-address"
                      name="email"
                      type="email"
                      autoComplete="email"
                      className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                      placeholder="Email address"
                    />
                    {errors.email && errors.email.message}
                  </div>
                  <div>
                    <label htmlFor="password" className="sr-only">
                      Password
                    </label>
                    <input
                      {...register('password', {
                        required: 'Required',
                        pattern: {
                          value: /^[A-Z]+/i,
                          message: 'invalid password',
                        },
                      })}
                      id="password"
                      name="password"
                      type="password"
                      autoComplete="password"
                      className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                      placeholder="Password"
                    />
                    {errors.password && errors.password.message}
                  </div>
                  <div>
                    <label htmlFor="confirm-password" className="sr-only">
                      Password
                    </label>
                    <input
                      {...register('confirmPassword', {
                        required: 'Required',
                        validate: (value) =>
                          value === password.current ||
                          'The passwords do not match',
                      })}
                      id="confirmPassword"
                      name="confirmPassword"
                      type="password"
                      autoComplete="confirmPassword"
                      className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                      placeholder="Confirm password"
                    />
                    {errors.confirmPassword && errors.confirmPassword.message}
                  </div>
                </div>

                <div className="flex items-center justify-between">
                  <div className="flex items-center">
                    <input
                      {...register('termsAndConditions', {
                        validate: (value) =>
                          value === true ||
                          'You must agree to the terms and conditions to use PFP.',
                      })}
                      id="termsAndConditions"
                      name="termsAndConditions"
                      type="checkbox"
                      className="h-4 w-4 text-indigo-600 focus:ring-indigo-500 border-gray-300 rounded"
                    />
                    {errors.termsAndConditions &&
                      errors.termsAndConditions.message}
                    <label
                      htmlFor="termsAndConditions"
                      className="ml-2 block text-sm text-gray-900"
                    >
                      I agree to the terms & conditions.
                    </label>
                  </div>

                  <div className="text-sm">
                    <a
                      href="#"
                      className="font-medium text-indigo-600 hover:text-indigo-500"
                    >
                      Already registered?
                    </a>
                  </div>
                </div>

                <div>
                  <button
                    type="submit"
                    className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                  >
                    <span className="absolute left-0 inset-y-0 flex items-center pl-3">
                      <LoginIcon
                        className="h-5 w-5 text-indigo-500 group-hover:text-indigo-400"
                        aria-hidden="true"
                      />
                    </span>
                    Register
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default Registration;
