import Input from "../UI/Input/Input";
import { IRegsiterData } from "@/DTO/Identity/IRegisterData";
import { IIdentityProps } from "@/DTO/Identity/IIdentityProps";
import Link from "next/link";

interface IRegisterProps extends IIdentityProps {
  values: IRegsiterData;
  error?: string;
}

const RegisterView = (props: IRegisterProps) => {
  return (
    <form onSubmit={props.onSubmit}>
      <h1 className="text-2xl xl:text-3xl text-white font-extrabold mb-5">
        Sign up for Cashtrack
      </h1>
      <Input
        label="Email"
        name="email"
        type="email"
        value={props.values.email}
        onChange={props.handleChange}
      />
      <Input
        label="Password"
        name="password"
        type="password"
        value={props.values.password}
        onChange={props.handleChange}
      />
      <Input
        label="Confirm password"
        name="confirmPassword"
        type="password"
        value={props.values.confirmPassword}
        onChange={props.handleChange}
      />
      {props.error && (
        <p className="mt-2 text-sm text-red-600 dark:text-red-500">
          <span className="font-medium">Oh, snapp!</span> {props.error}
        </p>
      )}
      <div className="flex items-start mb-6">
        <div className="flex items-center h-5">
          <input
            id="remember"
            type="checkbox"
            value=""
            className="w-4 h-4 border border-gray-600 rounded bg-background focus:ring-3 focus:ring-blue-300 dark:bg-gray-700 dark:border-gray-600 dark:focus:ring-blue-600 dark:ring-offset-gray-800"
            required
          />
        </div>
        <label
          htmlFor="remember"
          className="ml-2 text-sm font-medium text-gray-900 dark:text-gray-300"
        >
          I agree with the{" "}
          <a
            href="#"
            className="text-blue-600 hover:underline dark:text-blue-500"
          >
            terms and conditions
          </a>
          .
        </label>
      </div>
      <button className="mb-5 tracking-wide font-semibold bg-primary text-gray-100 w-full py-4 rounded-lg hover:bg-indigo-600 transition-all duration-300 ease-in-out flex items-center justify-center focus:shadow-outline focus:outline-none">
        <svg
          className="w-6 h-6 -ml-2"
          fill="none"
          stroke="currentColor"
          strokeWidth="2"
          strokeLinecap="round"
          strokeLinejoin="round"
        >
          <path d="M16 21v-2a4 4 0 00-4-4H5a4 4 0 00-4 4v2" />
          <circle cx="8.5" cy="7" r="4" />
          <path d="M20 8v6M23 11h-6" />
        </svg>
        <span className="ml-3">Sign Up</span>
      </button>
      <p className="text-sm font-light text-gray-500 dark:text-gray-400">
        Already have an account?{" "}
        <Link
          href="/Login"
          className="font-medium text-primary-600 hover:underline dark:text-primary-500"
        >
          Sign in
        </Link>
      </p>
    </form>
  );
};

export default RegisterView;
