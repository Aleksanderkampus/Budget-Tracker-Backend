interface IDefaultAddButtonProps {
  handleOpen: (i?: number) => void;
  children: JSX.Element;
}

const DefaultAddButton = (props: IDefaultAddButtonProps) => {
  return (
    <button
      id="dropdownDefaultButton"
      className="text-white w-full font-semibold rounded-lg text-sm px-2 py-2.5 text-center inline-flex items-center bg-background"
      type="button"
      onClick={() => props.handleOpen()}
    >
      {props.children}
    </button>
  );
};

export default DefaultAddButton;
