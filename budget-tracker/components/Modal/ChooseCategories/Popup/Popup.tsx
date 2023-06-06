import Image from "next/image";

interface IPopupProps {
  children: JSX.Element;
  isOpen: boolean;
  handleOpening: (isOpen: boolean) => void;
}

const Popup = (props: IPopupProps) => {
  return (
    <div
      id="dropdown"
      className={
        "z-10 bg-white rounded-lg overflow-y-scroll h-1/2 bottom-0 left-0 pt-3 pl-2.5 pr-2.5 absolute shadow w-full dark:bg-gray-800" +
        (props.isOpen ? "" : " hidden")
      }
    >
      <button
        type="button"
        onClick={() => props.handleOpening(false)}
        className="absolute z-40 top-3 right-2.5 text-gray-400 bg-transparent rounded-lg text-sm p-1.5 ml-auto inline-flex items-center hover:bg-background hover:text-white"
      >
        <Image src="../xIcon.svg" height={20} width={20} alt="" />
        <span className="sr-only">Close modal</span>
      </button>

      {props.children}
    </div>
  );
};

export default Popup;
