import Image from "next/image";
import { IChosenCategory } from "../ChooseCategories";
import SVGHelper from "@/helpers/Helper";
import IconWithName from "@/components/UI/IconWithName/IconWithName";

interface IDropdownItemProps extends IChosenCategory {
  handleCategoryChange: (e: IChosenCategory) => void;
}

const DropdownItem = (props: IDropdownItemProps) => {
  return (
    <li>
      {/* TODO MAKE THIS INTO AN COMPONENT*/}
      <IconWithName
        hexColor={[props.category.hexColor]}
        name={props.category.name}
        icon={props.category.icon}
        onClick={() =>
          props.handleCategoryChange({
            category: props.category,
            amount: props.amount,
          } as IChosenCategory)
        }
      />
    </li>
  );
};

export default DropdownItem;
