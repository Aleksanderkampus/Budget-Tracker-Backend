import Image from "next/image";

interface IAddButtonProps {
  onClick: () => void;
}

const AddButton = (props: IAddButtonProps) => {
  return (
    <button
      onClick={() => props.onClick()}
      className="flex transition-all ease-in-out hover:scale-110 items-center drop-shadow-xl justify-center fixed bottom-4 right-4 w-16 h-16 rounded-full bg-primary text-white text-lg font-bold focus:outline-none hover:bg-blue-700"
    >
      <Image width={22} src="../plus_icon.svg" height={22} alt="Add"></Image>
    </button>
  );
};

export default AddButton;
