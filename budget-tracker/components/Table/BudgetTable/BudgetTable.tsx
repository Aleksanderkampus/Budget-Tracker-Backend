import Dropdown from "@/components/UI/Dropdown/Dropdown";
import Search from "../Search";
import TableHead from "../TableHead";
import BudgetRow, { IBudgetRowProps } from "./BudgetRow";

interface IBudgetTableProps {
  data: IBudgetRowProps[];
  headers: string[];
  handleSearch: (target: EventTarget & HTMLInputElement) => void;
  search: string;
  handleEditOpening?: (id?: string) => void;
  handleDetailsOpening: (id?: string) => void;
  handleDelete: (id: string) => void;
}

const BudgetTable = (props: IBudgetTableProps) => {
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
                <BudgetRow
                  icon={e.icon ?? undefined}
                  hexValue={e.hexValue}
                  values={e.values}
                  id={e.id}
                  handleEditOpening={
                    props.handleEditOpening
                      ? props.handleEditOpening
                      : undefined
                  }
                  handleDetailsOpen={props.handleDetailsOpening}
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

export default BudgetTable;
