import Image from "next/image";

interface IModalLayoutProps {
  children: JSX.Element;
  onClick: () => void;
}

const ModalLayout = ({ children, onClick }: IModalLayoutProps) => {
  return (
    <div
      tabIndex={-1}
      aria-hidden="true"
      className="fixed top-0 left-0 right-0 z-40 w-full p-4 overflow-x-hidden bg-black bg-opacity-60  overflow-y-auto md:inset-0 h-[calc(100%)] max-h-full"
    >
      <div className="relative w-full max-w-md max-h-full m-auto z-50">
        <div className="relative bg-background rounded-lg shadow ">
          <button
            type="button"
            onClick={() => onClick()}
            className="absolute top-3 right-2.5 text-gray-400 bg-transparent rounded-lg text-sm p-1.5 ml-auto inline-flex items-center hover:bg-gray-800 hover:text-white"
          >
            <Image src="../../xIcon.svg" height={20} width={20} alt="" />
            <span className="sr-only">Close modal</span>
          </button>
          {children}
        </div>
      </div>
    </div>
  );
};

export default ModalLayout;
