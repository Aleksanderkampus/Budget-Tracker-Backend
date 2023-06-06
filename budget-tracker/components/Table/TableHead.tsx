interface ITableHeadProps {
  headings: string[];
}

const TableHead = (props: ITableHeadProps) => {
  return (
    <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
      <tr>
        {props.headings.map((e, i) => {
          return (
            <th key={i} scope="col" className="px-6 py-3">
              {e}
            </th>
          );
        })}
        <th scope="col" className="px-6 py-3">
          Action
        </th>
      </tr>
    </thead>
  );
};

export default TableHead;
