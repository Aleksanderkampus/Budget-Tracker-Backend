import IPieChartData from "@/DTO/Categories/IPieChartData";
import { useEffect, useState } from "react";
import { PieChart, Pie, Cell, Label } from "recharts";

const EMPTYDATA = [
  { name: "Empty", totalAmount: 1, hexColor: "#5A5A5A" },
] as IPieChartData[];

interface IDashPieProps {
  data: IPieChartData[];
  name: string;
}

const DashPie = (props: IDashPieProps) => {
  const [data, setData] = useState<IPieChartData[]>(EMPTYDATA);

  useEffect(() => {
    console.log(props.data);
    setData(props.data.length > 0 ? props.data : EMPTYDATA);
  }, [props.data]);

  return (
    <PieChart width={400} height={200}>
      <Pie
        stroke="transparent"
        data={
          props.name === "Expenses" && data[0].name !== "Empty"
            ? data.map((e) => {
                return { ...e, ["totalAmount"]: e.totalAmount * -1 };
              })
            : props.data
        }
        innerRadius={70}
        outerRadius={100}
        fill="#8884d8"
        paddingAngle={0}
        dataKey="totalAmount"
      >
        {data.map((entry, index) => (
          <Cell key={`cell-${index}`} fill={entry.hexColor} />
        ))}
        <Label
          textAnchor="middle"
          value={props.name}
          position="center"
          dy={-10}
          fill="white"
          style={{ fontSize: "1rem", color: "white", fontWeight: "bold" }}
        />
        <Label
          textAnchor="middle"
          value={
            data[0].name !== "Empty"
              ? data
                  .map((e) => e.totalAmount)
                  .reduce((a, b) => a + b)
                  .toString() + "€"
              : "0€"
          }
          position="center"
          dy={15}
          fill="white"
          style={{ fontSize: "1rem", color: "white" }}
        />
      </Pie>
    </PieChart>
  );
};

export default DashPie;
