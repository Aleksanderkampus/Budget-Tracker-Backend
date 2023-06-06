import { ISimpleCategory } from "@/DTO/Categories/ISimpleCategory";
import IconWithName from "../../UI/IconWithName/IconWithName";

interface ICategoryWithNameProps {
  amount: string;
  name: string;
  categories: ISimpleCategory[];
}

const CategoryWithAmount = (props: ICategoryWithNameProps) => {
  return (
    <div className="flex items-center justify-between">
      <IconWithName
        name={props.name}
        icon={
          props.categories.length > 1 ? undefined : props.categories[0].icon
        }
        hexColor={props.categories.map((e) => e.hexColor)}
      />
      <div className="text-white text-sm">{props.amount}</div>
    </div>
  );
};

export default CategoryWithAmount;
