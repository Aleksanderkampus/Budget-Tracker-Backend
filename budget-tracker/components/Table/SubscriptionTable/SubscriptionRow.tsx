export interface ISubscriptionRowProps {
  values: string[];
  id: string;
  handleEditOpening?: (id?: string) => void;
  handleDelete: (id: string) => void;
}

const SubscriptionRow = (props: ISubscriptionRowProps) => {
  return (
    <tr className="border-b bg-background border-gray-600  hover:bg-gray-800">
      {props.values.map((e, i) => {
        return (
          <td key={i} className="px-6 py-3 text-white">
            {e}
          </td>
        );
      })}
      <td className="px-6 py-4 text-white ">
        <div
          onClick={() => props.handleEditOpening!(props.id)}
          className="font-medium text-blue-600 dark:text-blue-500 hover:underline"
        >
          Edit
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

export default SubscriptionRow;
