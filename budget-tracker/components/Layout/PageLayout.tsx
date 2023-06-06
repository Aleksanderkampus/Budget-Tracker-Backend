interface IPageLayout {
  children: JSX.Element;
  heading: string;
}

const PageLayout = ({ children, heading }: IPageLayout) => {
  return (
    <>
      <h1 className="text-white font-bold text-3xl mb-3">{heading}</h1>
      <div className="flex items-center mb-4">
        <h2 className="text-white font-bold text-base mr-3">
          Current card: Cash Account
        </h2>
      </div>
      {children}
    </>
  );
};

export default PageLayout;
