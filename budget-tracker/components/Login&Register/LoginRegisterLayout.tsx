import Image from "next/image";

interface ILoginRegisterProps {
  children: JSX.Element;
}

const LoginRegisterLayout = ({ children }: ILoginRegisterProps) => {
  return (
    <div className="max-w-screen-xl m-0 sm:m-20 bg-background shadow sm:rounded-lg flex justify-center flex-1">
      <div className=" lg:w-1/2 xl:w-5/12 p-6 sm:p-12 bg-background rounded-2xl">
        {children}
      </div>
      <div className="flex-1 bg-background flex-col items-center  justify-center hidden lg:flex rounded-2xl">
        <Image src="/frontpage_animation.gif" alt="" width={500} height={10} />
      </div>
    </div>
  );
};

export default LoginRegisterLayout;
