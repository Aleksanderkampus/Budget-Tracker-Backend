import Navbar from "./NavBar/Navbar";
import classes from "./Layout.module.css";

type Props = {
  children: JSX.Element;
};

const Layout = ({ children }: Props) => {
  return (
    <div className={classes.page}>
      <Navbar></Navbar>
      <main className="w-full ml-10 mr-10">{children}</main>
    </div>
  );
};

export default Layout;
