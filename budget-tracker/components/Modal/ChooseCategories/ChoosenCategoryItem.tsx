import SVGHelper from "@/helpers/Helper";
import Image from "next/image";
import { useEffect, useState } from "react";
import CategoriesDropdown from "./CategoriesDropdown/CategoriesDropdown";
import { IChosenCategory } from "./ChooseCategories";

interface IChoosenCategoryItemProps {
  name: string;
  icon: string;
  withInputs: boolean;
  hexColor: string;
  amount?: string;
  handleAmountChange: (
    target: EventTarget & HTMLInputElement,
    i: number
  ) => void;
  index: number;
  handleDelete: (i: number) => void;
  handleOpen: (i?: number) => void;
}

const ChoosenCategoryItem = (props: IChoosenCategoryItemProps) => {
  const [error, setError] = useState<string | undefined>(undefined);
  const handleAmountChange = (
    target: EventTarget & HTMLInputElement,
    i: number
  ) => {
    const pattern = /^\d+(\.\d+)?$/;
    if (!pattern.test(target.value) && target.value !== "") {
      setError("Amount must be a positive number");
      return;
    }
    props.handleAmountChange(target, i);
  };
  return (
    <>
      <div className="flex items-center justify-between">
        {/* TODO MAKE THIS INTO AN COMPONENT*/}
        <div
          className="flex cursor-pointer items-center px-2 py-4 text-gray-900 whitespace-nowrap dark:text-white"
          onClick={() => props.handleOpen(props.index)}
        >
          <div
            className="p-2 rounded"
            style={{
              backgroundColor: props.hexColor,
            }}
          >
            <Image
              width={20}
              height={20}
              src={SVGHelper.convertBase64toURI(props.icon)}
              alt=""
            />
          </div>
          <div className="pl-3">
            <div className="text-base font-semibold">{props.name}</div>
          </div>
          {error && (
            <p className="mt-2 text-sm text-red-600 dark:text-red-500">
              <span className="font-medium">Oh, snapp!</span> {error}
            </p>
          )}
        </div>
        <div className="relative flex items-center z-0 w-1/5 pb-4 group">
          {props.withInputs && (
            <>
              <input
                value={props.amount ?? ""}
                readOnly={props.index === 0 ? true : false}
                className="block py-2.5 px-0 text-right w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none dark:text-white dark:border-gray-600 dark:focus:border-blue-500 focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                placeholder="€"
                onChange={(e) =>
                  props.handleAmountChange(e.target, props.index)
                }
                required
              />
            </>
          )}
          {props.index !== 0 && (
            <div
              className="cursor-pointer ml-3 rounded-md hover:bg-gray-800"
              onClick={() => props.handleDelete(props.index)}
            >
              <Image
                src="xIcon.svg"
                className="max-w-none"
                height={20}
                width={20}
                alt="X"
              />
            </div>
          )}
        </div>
      </div>
    </>
  );
};

export default ChoosenCategoryItem;
