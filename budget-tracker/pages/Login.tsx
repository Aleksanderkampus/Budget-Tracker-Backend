import LoginView from "@/components/Login&Register/LoginView";
import { IdentityService } from "@/services/IdentityService";
import { useRouter } from "next/router";
import { useContext, useState, FormEvent, useEffect } from "react";
import { JwtContext } from "./_app";
import { ILoginData } from "@/DTO/Identity/ILoginData";

const Login = () => {
  const router = useRouter();
  const identityService = new IdentityService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);

  const [values, setValues] = useState({
    email: "",
    password: "",
  } as ILoginData);

  const [error, setError] = useState<string | undefined>(undefined);

  const handleChange = (target: EventTarget & HTMLInputElement) => {
    setValues({ ...values, [target.name]: target.value });
  };

  useEffect(() => {
    setError(undefined);
  }, [values]);

  const onSubmit = async (event: FormEvent) => {
    event.preventDefault();

    if (values.email.length === 0 || values.password.length === 0) {
      setError("Password/email can't be empty");
      return;
    }

    // remove errors

    var jwtData = await identityService.login(values, (error: string) => {
      setError(error);
    });

    if (jwtData == undefined) {
      // TODO: get error info
      return;
    }

    if (setJwtResponse) {
      setJwtResponse(jwtData);
      router.push("/");
    }
  };

  return (
    <LoginView
      values={values}
      handleChange={handleChange}
      onSubmit={onSubmit}
      error={error}
    />
  );
};

export default Login;
