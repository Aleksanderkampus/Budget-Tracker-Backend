interface IInputProps {
  label: string;
  value: string;
  name: string;
  type: string;
  onChange: (target: EventTarget & HTMLInputElement) => void;
  children?: JSX.Element;
}

const Input = (props: IInputProps) => {
  return (
    <>
      {props.children}
      <div className="relative mb-5 flex items-center">
        <input
          type={props.type}
          name={props.name}
          className="block px-2.5 pb-2.5 pt-4 w-full text-sm text-white bg-transparent rounded-lg border-2 border-white appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
          placeholder=" "
          value={props.value}
          onChange={(e) => props.onChange(e.target)}
        />

        <label
          htmlFor={props.name}
          className="absolute text-sm text-gray-400 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-background  px-2 peer-focus:px-2 peer-focus:text-blue-600  peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 left-1"
        >
          {props.label}
        </label>
      </div>
    </>
  );
};

export default Input;
