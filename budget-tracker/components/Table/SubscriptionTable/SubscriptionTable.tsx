import Dropdown from "@/components/UI/Dropdown/Dropdown";
import Search from "../Search";
import TableHead from "../TableHead";
import SubscriptionRow, { ISubscriptionRowProps } from "./SubscriptionRow";

interface ISubscriptionTableProps {
  search: string;
  handleSearch: () => void;
  headers: string[];
  data: ISubscriptionRowProps[];
  handleEditOpening?: (id?: string) => void;
  handleDelete: (id: string) => void;
}

const SubscriptionTable = (props: ISubscriptionTableProps) => {
  return (
    <div className="p-5 bg-background shadow-md sm:rounded-lg">
      <div className="flex items-center overflow-y-visible justify-between p- pb-4">
        <Search search={props.search} handleSearch={props.handleSearch} />
        <Dropdown />
      </div>
      <div className="relative overflow-x-auto ">
        <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
          <TableHead headings={props.headers} />
          <tbody>
            {props.data.map((e, i) => {
              return (
                <SubscriptionRow
                  values={e.values}
                  id={e.id}
                  handleEditOpening={
                    props.handleEditOpening
                      ? props.handleEditOpening
                      : undefined
                  }
                  key={i}
                  handleDelete={props.handleDelete}
                />
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default SubscriptionTable;
