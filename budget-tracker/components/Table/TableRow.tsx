import Helper from "@/helpers/Helper";
import Image from "next/image";
export interface ITableRowProps {
  icon?: string;
  hexValue: string[];
  values: string[];
  id: string;
  handleEditOpening?: (id?: string) => void;
  handleDetailsOpen?: (id?: string) => void;
  handleDelete?: (id: string) => void;
}

const TableRow = (props: ITableRowProps) => {
  return (
    <tr className="border-b bg-background border-gray-600  hover:bg-gray-800">
      {props.values.map((e, i) => {
        if (i === 0) {
          return (
            <th
              key={i}
              scope="row"
              className="flex items-center px-6 py-4 text-gray-900 whitespace-nowrap dark:text-white"
            >
              <div
                className="p-2 rounded"
                style={{
                  backgroundImage: Helper.getLinearGradient(props.hexValue),
                }}
              >
                {props.icon ? (
                  <Image
                    width={20}
                    height={20}
                    src={Helper.convertBase64toURI(props.icon)}
                    alt=""
                  />
                ) : (
                  <div className="w-5 h-5"></div>
                )}
              </div>
              <div className="pl-3">
                <div className="text-base font-semibold">{e}</div>
              </div>
            </th>
          );
        } else {
          return (
            <td key={i} className="px-6 py-4 text-white">
              {e}
            </td>
          );
        }
      })}
      <td className="px-6 py-4 text-white">
        {props.handleEditOpening && (
          <div
            onClick={() => props.handleEditOpening!(props.id)}
            className="font-medium text-blue-600 dark:text-blue-500 hover:underline"
          >
            Edit
          </div>
        )}
        {props.handleDelete && (
          <div
            onClick={() => props.handleDelete!(props.id)}
            className="font-medium text-red-600 dark:text-red-500 hover:underline"
          >
            Delete
          </div>
        )}
        <div
          onClick={() => props.handleDetailsOpen!(props.id)}
          className="font-medium text-blue-600 dark:text-blue-500 hover:underline"
        >
          Details
        </div>
      </td>
    </tr>
  );
};

export default TableRow;
