import { MouseEvent, useEffect, useState } from "react";
import DefaultAddButton from "./DefaultAddButton";
import ChoosenCategoryItem from "./ChoosenCategoryItem";
import Image from "next/image";
import CategoriesDropdown from "./CategoriesDropdown/CategoriesDropdown";
import { ISimpleCategory } from "@/DTO/Categories/ISimpleCategory";

export interface IChosenCategory {
  id?: string;
  category: ISimpleCategory;
  amount?: string;
}

interface IChooseCategoriesProps {
  handleCategoryChange: (categories: IChosenCategory[]) => void;
  amount: string;
  value: IChosenCategory[];
  withInputs: boolean;
}

const ChooseCategories = (props: IChooseCategoriesProps) => {
  const [isOpen, setIsOpen] = useState(false);

  const [indexToChange, setIndexToChange] = useState(0);

  const handelOpen = (i?: number) => {
    if (i === undefined) {
      setIndexToChange(props.value.length);
    } else {
      setIndexToChange(i);
    }
    setIsOpen(!isOpen);
  };

  useEffect(() => {
    if (props.value.length > 0) {
      const prev = [...props.value];
      props.handleCategoryChange(changeFirstAmount(prev));
    }
  }, [props.amount]);

  const handleCategoryChoice = (e: IChosenCategory) => {
    if (indexToChange !== props.value.length) {
      handleCategoryChange(e, indexToChange);
    } else {
      const copy = { ...e };
      if (props.value.length === 0) {
        copy.amount = props.amount;
      }
      props.handleCategoryChange([...props.value, copy]);
    }
    handelOpen();
  };

  const handleAmountChange = (
    target: EventTarget & HTMLInputElement,
    i: number
  ) => {
    const prev = [...props.value];
    prev[i].amount = target.value;

    props.handleCategoryChange(changeFirstAmount(prev));
  };

  const changeFirstAmount = (prev: IChosenCategory[]) => {
    prev[0].amount = (
      parseFloat(props.amount) -
      props.value.slice(1).reduce((a, b) => a + parseFloat(b.amount!), 0)
    )
      .toFixed(2)
      .toString();
    return prev;
  };

  const handleDelete = (i: number) => {
    const prev = [...props.value];
    const removedElement = prev.splice(i, 1);
    prev[0].amount = (
      parseFloat(prev[0].amount!) +
      parseFloat(removedElement[0].amount ? removedElement[0].amount : "0")
    ).toString();
    props.handleCategoryChange(prev);
  };

  const handleCategoryChange = (e: IChosenCategory, i: number) => {
    const prev = [...props.value];
    const copy = { ...e };
    copy.amount = prev[i].amount;
    copy.id = prev[i].id;
    console.log(copy);
    prev.splice(i, 1, copy);
    props.handleCategoryChange(prev);
  };

  return (
    <div>
      {props.value.map((e, i) => (
        <ChoosenCategoryItem
          key={i}
          name={e.category.name}
          hexColor={e.category.hexColor}
          icon={e.category.icon}
          amount={e.amount ? e.amount : undefined}
          index={i}
          handleAmountChange={handleAmountChange}
          handleDelete={handleDelete}
          handleOpen={handelOpen}
          withInputs={props.withInputs}
        />
      ))}
      <DefaultAddButton handleOpen={handelOpen}>
        <>
          {props.value.length < 1 ? (
            <>
              <Image
                className="mr-5"
                width={22}
                src="../plus_icon.svg"
                height={22}
                alt="Add"
              ></Image>
              Choose a category
            </>
          ) : (
            <>
              <Image
                className="mr-5"
                width={22}
                src="../scissorIcon.svg"
                height={22}
                alt="Add"
              ></Image>
              {props.withInputs ? "Split transaction" : "Add category"}
            </>
          )}
        </>
      </DefaultAddButton>
      <CategoriesDropdown
        isOpen={isOpen}
        handleCategoriesChoice={handleCategoryChoice}
        handleOpening={setIsOpen}
      />
    </div>
  );
};

export default ChooseCategories;
