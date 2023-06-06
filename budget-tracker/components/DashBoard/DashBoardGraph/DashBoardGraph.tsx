import ITransactionGraphData from "@/DTO/Transactions/TransactionGraphData";
import { JwtContext } from "@/pages/_app";
import TransactionsService from "@/services/TransactionsService";
import { useContext, useEffect, useState } from "react";
import {
  BarChart,
  Bar,
  XAxis,
  LabelList,
  ResponsiveContainer,
  YAxis,
  CartesianGrid,
} from "recharts";

const EXAMPLEDATA = [
  { weekDay: "Mon", totalAmount: 0 },
  { weekDay: "Tue", totalAmount: 0 },
  { weekDay: "Wed", totalAmount: 0 },
  { weekDay: "Thu", totalAmount: 0 },
  { weekDay: "Fri", totalAmount: 0 },
  { weekDay: "Sat", totalAmount: 0 },
  { weekDay: "Sun", totalAmount: 0 },
];

const renderCustomizedLabel = (props: any) => {
  const { x, y, width, value } = props;
  const radius = 10;
  console.log(value);
  return (
    <text
      x={x + width / 2}
      y={y - radius}
      fill="#fff"
      textAnchor="middle"
      dominantBaseline="middle"
      fontSize={12}
    >
      {value + "€"}
    </text>
  );
};

const DashBoardGraph = () => {
  const [data, setData] = useState<ITransactionGraphData[]>(EXAMPLEDATA);
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const transactionsService = new TransactionsService();

  useEffect(() => {
    if (jwtResponse) {
      transactionsService.getGraphData(jwtResponse.jwt).then((res) => {
        if (res) {
          const isPositiveAmount =
            res.filter((e) => e.totalAmount > 0).length > 0 ? true : false;
          const newData = EXAMPLEDATA.map((e) => {
            const gd = res.find((gd) => gd.weekDay === e.weekDay);
            if (gd !== undefined) {
              e.totalAmount = gd.totalAmount * (isPositiveAmount ? 1 : -1);
            }
            return e;
          });
          setData(newData);
        }
      });
    }
  }, [jwtResponse]);

  return (
    <div className="p-5 bg-background ml-5 relative shadow-md sm:rounded-lg">
      <h3 className="mb-4 text-xl font-bold text-white">This week expenses</h3>
      <ResponsiveContainer width={700} height={400}>
        <BarChart data={data}>
          <YAxis
            stroke="#fff"
            fontSize={12}
            tickLine={false}
            domain={[0, "dataMax + 10"]}
            axisLine={{ strokeOpacity: 0 }}
          />
          <XAxis
            domain={[0, "dataMax + 50"]}
            dataKey="weekDay"
            stroke="#fff"
            tickLine={false}
            fontSize={12}
          />
          <CartesianGrid vertical={false} horizontal strokeDasharray="5 5" />
          <Bar
            isAnimationActive={false}
            dataKey="totalAmount"
            fill="#3B00E3"
            barSize={15}
            maxBarSize={1}
          >
            <LabelList dataKey="totalAmount" content={renderCustomizedLabel} />
          </Bar>
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
};

export default DashBoardGraph;
