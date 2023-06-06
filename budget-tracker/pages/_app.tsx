import { IJWTResponse } from "@/DTO/Identity/IJWTResponse";
import Layout from "@/components/Layout/Layout";
import LoginRegisterLayout from "@/components/Login&Register/LoginRegisterLayout";
import RouteGuard from "@/components/RouteGuard";
import "@/styles/globals.css";
import type { AppProps } from "next/app";
import { useState } from "react";
import { createContext } from "react";

export const JwtContext = createContext<{
  jwtResponse: IJWTResponse | null;
  setJwtResponse: ((data: IJWTResponse | null) => void) | null;
}>({ jwtResponse: null, setJwtResponse: null });

export default function App({ Component, pageProps }: AppProps) {
  const [jwtResponse, setJwtResponse] = useState(null as IJWTResponse | null);
  return (
    <JwtContext.Provider value={{ jwtResponse, setJwtResponse }}>
      <RouteGuard>
        <>
          {!jwtResponse && (
            <LoginRegisterLayout>
              <Component {...pageProps} />
            </LoginRegisterLayout>
          )}
          {jwtResponse && (
            <Layout>
              <Component {...pageProps} />
            </Layout>
          )}
        </>
      </RouteGuard>
    </JwtContext.Provider>
  );
}
