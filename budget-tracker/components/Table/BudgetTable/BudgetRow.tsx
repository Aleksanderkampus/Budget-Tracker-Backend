import IconWithName from "../../UI/IconWithName/IconWithName";
import ProgressBar from "../../UI/ProgressBar/ProgressBar";
export interface IBudgetRowProps {
  icon?: string;
  hexValue: string[];
  values: IBudgetRowValue;
  id: string;
  handleEditOpening?: (id?: string) => void;
  handleDetailsOpen?: (id?: string) => void;
  handleDelete: (id: string) => void;
}

export interface IBudgetRowValue {
  name: string;
  category: string;
  amountSpent: number;
  total: number;
  fromDate: string;
  toDate: string;
  currencySymbol: string;
}

const BudgetRow = (props: IBudgetRowProps) => {
  console.log(props);
  return (
    <tr className="border-b bg-background border-gray-600  hover:bg-gray-800">
      <th
        scope="row"
        className="flex items-center px-6 py-4 text-gray-900 whitespace-nowrap dark:text-white"
      >
        <IconWithName
          icon={props.icon ? props.icon : undefined}
          hexColor={props.hexValue}
          name={props.values.name}
        />
      </th>
      <td className="px-6 py-4 text-white">{props.values.category}</td>
      <td className="px-6 py-4 text-white">
        <ProgressBar
          total={props.values.total}
          amountSpent={props.values.amountSpent}
          currency={props.values.currencySymbol}
        />
      </td>

      <td className="px-6 py-4 text-white">{props.values.fromDate}</td>
      <td className="px-6 py-4 text-white">{props.values.toDate}</td>
      <td className="px-6 py-4 text-white">
        {props.handleEditOpening && (
          <div
            onClick={() => props.handleEditOpening!(props.id)}
            className="font-medium text-blue-600 dark:text-blue-500 hover:underline"
          >
            Edit
          </div>
        )}
        <div
          onClick={() => props.handleDetailsOpen!(props.id)}
          className="font-medium text-blue-600 dark:text-blue-500 hover:underline"
        >
          Details
        </div>
        <div
          onClick={() => props.handleDelete!(props.id)}
          className="font-medium text-red-600 dark:text-red-500 hover:underline"
        >
          Delete
        </div>
      </td>
    </tr>
  );
};

export default BudgetRow;
