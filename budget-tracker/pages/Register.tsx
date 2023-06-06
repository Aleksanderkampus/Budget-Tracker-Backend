import { IRegsiterData } from "@/DTO/Identity/IRegisterData";
import RegisterView from "@/components/Login&Register/RegisterView";
import { IdentityService } from "@/services/IdentityService";
import { FormEvent, useContext, useEffect, useState } from "react";
import { JwtContext } from "./_app";
import { useRouter } from "next/router";

const Register = () => {
  const router = useRouter();
  const identityService = new IdentityService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);

  const [error, setError] = useState<string | undefined>(undefined);

  const [values, setValues] = useState({
    email: "",
    password: "",
    confirmPassword: "",
  } as IRegsiterData);

  const handleChange = (target: EventTarget & HTMLInputElement) => {
    setValues({ ...values, [target.name]: target.value });
  };

  useEffect(() => {
    setError(undefined);
  }, [values]);

  const onSubmit = async (target: FormEvent) => {
    target.preventDefault();
    //TODO! check errors
    if (values.confirmPassword !== values.password) {
      setError("Password and confirm password don't match");
      return;
    }

    const jwtData = await identityService.register(
      values,
      (errorMessage: string) => {
        setError(errorMessage);
      }
    );

    if (!jwtData) {
      return;
    }

    if (setJwtResponse) {
      setJwtResponse(jwtData);
      router.push("/");
    }
  };

  return (
    <RegisterView
      values={values}
      handleChange={handleChange}
      onSubmit={onSubmit}
      error={error}
    />
  );
};

export default Register;
