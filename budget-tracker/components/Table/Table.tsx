import Dropdown from "../UI/Dropdown/Dropdown";
import Search from "./Search";
import TableHead from "./TableHead";
import TableRow, { ITableRowProps } from "./TableRow";

interface ITableProps {
  data: ITableRowProps[];
  headers: string[];
  handleSearch: (target: EventTarget & HTMLInputElement) => void;
  search: string;
  handleEditOpening?: (id?: string) => void;
  handleDetailsOpening: (id?: string) => void;
  handleDelete?: (id: string) => void;
}

const Table = (props: ITableProps) => {
  return (
    <div className="p-5 bg-background shadow-md sm:rounded-lg">
      <div className="flex items-center overflow-y-visible justify-between  pb-4">
        <Search search={props.search} handleSearch={props.handleSearch} />
        <Dropdown />
      </div>
      <div className="relative overflow-x-auto ">
        <table
          id="example-table"
          className="w-full text-sm text-left text-gray-500 dark:text-gray-400"
        >
          <TableHead headings={props.headers} />
          <tbody>
            {props.data.map((e, i) => {
              return (
                <TableRow
                  icon={e.icon}
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
                  handleDelete={
                    props.handleDelete ? props.handleDelete : undefined
                  }
                />
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Table;
