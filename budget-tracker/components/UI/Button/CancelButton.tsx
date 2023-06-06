interface ICancelButtonProps {
  onClick: (isOpen: boolean) => void;
}

const CancelButton = (props: ICancelButtonProps) => {
  return (
    <button
      type="button"
      onClick={() => props.onClick(false)}
      className="focus:outline-none text-white bg-red-700 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 "
    >
      Cancel
    </button>
  );
};

export default CancelButton;
