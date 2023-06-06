import { IGraphData } from "@/DTO/Categories/IGraphData";
import {
  BarChart,
  Bar,
  XAxis,
  LabelList,
  ResponsiveContainer,
  YAxis,
} from "recharts";

interface IAnalyticsGraphProps {
  data: IGraphData[];
  total: string;
}

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
      {value}
    </text>
  );
};

const AnalyticsGraph = (props: IAnalyticsGraphProps) => {
  return (
    <>
      <h2 className="text-white text-lg font-bold">Analytics</h2>
      <div className="flex items-center text-sm text-white mb-1 justify-between">
        <h3>Last 6 months</h3>
        <h3>{props.total}</h3>
      </div>
      <div className="border-black flex  justify-center rounded-md border-2">
        <ResponsiveContainer width="100%" height={200}>
          <BarChart data={props.data}>
            <YAxis hide domain={[0, "dataMax + 10"]} />
            <XAxis
              domain={[0, "dataMax + 50"]}
              dataKey="month"
              axisLine={{ strokeOpacity: 0 }}
              stroke="#fff"
              tickLine={false}
              fontSize={12}
            />
            <Bar
              dataKey="amount"
              fill="#5EFF8A"
              barSize={15}
              radius={[5, 5, 5, 5]}
              maxBarSize={1}
            >
              <LabelList dataKey="amount" content={renderCustomizedLabel} />
            </Bar>
          </BarChart>
        </ResponsiveContainer>
      </div>
    </>
  );
};

export default AnalyticsGraph;
