import Link from "next/link";
import Input from "../UI/Input/Input";
import { ILoginData } from "@/DTO/Identity/ILoginData";
import { IIdentityProps } from "@/DTO/Identity/IIdentityProps";

interface ILoginProps extends IIdentityProps {
  values: ILoginData;
  error?: string;
}

const LoginView = (props: ILoginProps) => {
  return (
    <form onSubmit={props.onSubmit}>
      <h1 className="text-2xl xl:text-3xl text-white font-extrabold mb-5">
        Sign in to your account
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
      {props.error && (
        <p className="mt-2 text-sm text-red-600 dark:text-red-500">
          <span className="font-medium">Oh, snapp!</span> {props.error}
        </p>
      )}
      <button className="mb-5 tracking-wide font-semibold bg-primary text-gray-100 w-full py-4 rounded-lg hover:bg-indigo-600 transition-all duration-300 ease-in-out flex items-center justify-center focus:shadow-outline focus:outline-none">
        <span className="ml-3">Sign In</span>
      </button>
      <p className="text-sm font-light text-gray-500 dark:text-gray-400">
        Don’t have an account yet?{" "}
        <Link
          href="/Register"
          className="font-medium text-primary-600 hover:underline dark:text-primary-500"
        >
          Sign up
        </Link>
      </p>
    </form>
  );
};

export default LoginView;
