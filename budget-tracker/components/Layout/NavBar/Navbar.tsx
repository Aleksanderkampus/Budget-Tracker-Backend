import Link from "next/link";
import { useRouter } from "next/router";
import classes from "./Navbar.module.css";
import Image from "next/image";
import { JwtContext } from "@/pages/_app";
import { IdentityService } from "@/services/IdentityService";
import { useContext } from "react";

const Navbar = () => {
  const router = useRouter();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const identityService = new IdentityService();

  const logout = () => {
    if (jwtResponse)
      identityService.logout(jwtResponse).then((response) => {
        if (setJwtResponse) setJwtResponse(null);
        router.push("/Login");
      });
  };

  return (
    <header>
      <nav className={classes.my_navbar}>
        <div className={classes.my_menu}>
          <Image src="/logo.svg" width={180} height={50} alt="Budget tracker" />
          <ul>
            <li
              className={router.pathname === "/" ? classes.my_active_link : ""}
            >
              <Link href="/">
                <Image
                  src="/navbar_icons/dashboard_icon.svg"
                  width={20}
                  height={20}
                  alt=""
                />
                Dashboard
              </Link>
            </li>
            <li
              className={
                router.pathname === "/Transactions"
                  ? classes.my_active_link
                  : router.pathname.includes("/Transactions/")
                  ? classes.my_active_icon
                  : ""
              }
            >
              <Link href="/Transactions">
                <Image
                  src="/navbar_icons/transactions_icon.svg"
                  width={30}
                  height={30}
                  alt=""
                />
                Transactions
              </Link>
              <ul>
                <li
                  className={
                    router.pathname === "/Transactions/Budgets"
                      ? classes.my_active_link
                      : ""
                  }
                >
                  <Link href="/Transactions/Budgets">Budgets</Link>
                </li>
                <li
                  className={
                    router.pathname === "/Transactions/Subscriptions"
                      ? classes.my_active_link
                      : ""
                  }
                >
                  <Link href="/Transactions/Subscriptions">Subscriptions</Link>
                </li>
                <li
                  className={
                    router.pathname === "/Transactions/Categories"
                      ? classes.my_active_link
                      : ""
                  }
                >
                  <Link href="/Transactions/Categories">Categories</Link>
                </li>
              </ul>
            </li>
          </ul>
        </div>
        <div className={classes.my_logout}>
          <button
            onClick={() => {
              logout();
            }}
          >
            Sign out
          </button>
        </div>
      </nav>
    </header>
  );
};

export default Navbar;
