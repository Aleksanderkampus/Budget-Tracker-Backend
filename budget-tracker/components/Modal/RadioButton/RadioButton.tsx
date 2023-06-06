interface IRadioButtonProps {
  value: string;
  handleChange: (target: EventTarget & HTMLInputElement) => void;
}

const RadioButton = (props: IRadioButtonProps) => {
  return (
    <>
      <ul className="grid w-full mb-2 mt-2 gap-6 md:grid-cols-2">
        <li>
          <input
            type="radio"
            id="hosting-small"
            name="hosting"
            value="-"
            className="hidden peer"
            onChange={(e) => props.handleChange(e.target)}
            checked={props.value === "-"}
          />
          <label
            htmlFor="hosting-small"
            className="inline-flex items-center justify-between w-full p-2 text-white border-2 rounded-lg cursor-pointer hover:text-gray-300 border-white peer-checked:text-white peer-checked:border-primary dark:peer-checked:bg-primary bg-background hover:border-gray-700 hover:bg-gray-700"
          >
            <div className="block">
              <div className="w-full text-lg font-semibold">Expense -</div>
            </div>
          </label>
        </li>
        <li>
          <input
            type="radio"
            id="hosting-big"
            name="hosting"
            value="+"
            className="hidden peer"
            onChange={(e) => props.handleChange(e.target)}
            checked={props.value === "+"}
          />
          <label
            htmlFor="hosting-big"
            className="inline-flex items-center justify-between w-full p-2 text-white border-2 rounded-lg cursor-pointer hover:text-gray-300 border-white peer-checked:text-white peer-checked:border-primary dark:peer-checked:bg-primary bg-background hover:border-gray-700 hover:bg-gray-700"
          >
            <div className="block">
              <div className="w-full text-lg font-semibold">Income +</div>
            </div>
          </label>
        </li>
      </ul>
    </>
  );
};

export default RadioButton;
