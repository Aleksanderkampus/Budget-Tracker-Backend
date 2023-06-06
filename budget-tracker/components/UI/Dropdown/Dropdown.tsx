import { useState } from "react";
import DropdownRow from "./DropdownRow";

const Dropdown = () => {
  const [isOpen, setIsOpen] = useState(false);
  return (
    <div>
      <button
        id="dropdownRadioButton"
        onClick={() => setIsOpen(!isOpen)}
        className="inline-flex items-center border font-medium rounded-lg text-sm px-3 py-1.5 bg-background text-white border-gray-600 hover:bg-gray-800 hover:border-gray-600 focus:ring-gray-700"
        type="button"
      >
        <svg
          className="w-4 h-4 mr-2 text-gray-400"
          aria-hidden="true"
          fill="currentColor"
          viewBox="0 0 20 20"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            fillRule="evenodd"
            d="M10 18a8 8 0 100-16 8 8 0 000 16zm1-12a1 1 0 10-2 0v4a1 1 0 00.293.707l2.828 2.829a1 1 0 101.415-1.415L11 9.586V6z"
            clipRule="evenodd"
          ></path>
        </svg>
        Last 30 days
        <svg
          className="w-3 h-3 ml-2"
          aria-hidden="true"
          fill="none"
          stroke="currentColor"
          viewBox="0 0 24 24"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth="2"
            d="M19 9l-7 7-7-7"
          ></path>
        </svg>
      </button>
      {/*Dropdown menu */}
      <div
        id="dropdownRadio"
        className={
          isOpen
            ? "z-10 w-48 divide-y rounded-lg shadow border border-gray-600 dark:bg-background divide-gray-600"
            : "z-10 w-48 hidden divide-y rounded-lg  shadow dark:bg-background divide-gray-600"
        }
        style={{
          position: "absolute",
          inset: "auto auto auto auto",
          marginTop: "10px",
        }}
      >
        <ul
          className="p-3 space-y-1 text-sm text-gray-700 dark:text-gray-200"
          aria-labelledby="dropdownRadioButton"
        >
          <DropdownRow value="Last day" />
          <DropdownRow value="Last 7 days" />
          <DropdownRow value="Last 30 days" />
          <DropdownRow value="Last month" />
          <DropdownRow value="Last year" />
        </ul>
      </div>
    </div>
  );
};

export default Dropdown;
