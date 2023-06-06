import { JwtContext } from "@/pages/_app";
import { useRouter } from "next/router";
import { useContext, useEffect, useState } from "react";

interface IRouteProps {
  children: JSX.Element;
}

const RouteGuard = ({ children }: IRouteProps) => {
  const router = useRouter();
  const [auth, setAuth] = useState(false);
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);

  useEffect(() => {
    checkAuth(router.asPath);

    const hideContent = () => setAuth(false);
    router.events.on("routeChangeStart", hideContent);

    // on route change complete - run auth check
    router.events.on("routeChangeComplete", checkAuth);

    // unsubscribe from events in useEffect return function
    return () => {
      router.events.off("routeChangeStart", hideContent);
      router.events.off("routeChangeComplete", checkAuth);
    };
  }, [jwtResponse]);

  const checkAuth = (url: string) => {
    const publicPaths = ["/Login", "/Register", "/Comptest"];
    if (!jwtResponse && !publicPaths.includes(url)) {
      setAuth(false);
      router.push("/Login");
    } else {
      setAuth(true);
    }
  };

  return <>{auth && children}</>;
};

export default RouteGuard;
