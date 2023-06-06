interface IDropdownRowProps {
  value: string;
}

const DropdownRow = (props: IDropdownRowProps) => {
  return (
    <li>
      <div className="flex items-center p-2 rounded hover:bg-gray-800">
        <input
          id="filter-radio-example-1"
          type="radio"
          value=""
          name="filter-radio"
          className="w-4 h-4  ring-offset-gray-800 focus:ring-offset-gray-800 focus:ring-2 bg-gray-700 border-gray-600"
        />
        <label
          htmlFor="filter-radio-example-1"
          className="w-full ml-2 text-sm font-medium rounded text-gray-200"
        >
          {props.value}
        </label>
      </div>
    </li>
  );
};

export default DropdownRow;
