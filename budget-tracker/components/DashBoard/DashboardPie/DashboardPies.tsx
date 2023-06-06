import DashPie from "./DashPie";
import { JwtContext } from "@/pages/_app";
import { FinancialCategoryService } from "@/services/FinancialCategoryService";
import { useContext, useEffect, useState } from "react";
import IPieChartData from "@/DTO/Categories/IPieChartData";

const DashboardPies = () => {
  const categoryService = new FinancialCategoryService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const [data, setData] = useState<IPieChartData[]>([]);

  useEffect(() => {
    if (jwtResponse) {
      categoryService.getPieChartData(jwtResponse.jwt).then((res) => {
        console.log(res);
        setData(res || []);
      });
    }
  }, [jwtResponse]);

  return (
    <div className="p-5 bg-background w-96 flex flex-col items-center shadow-md sm:rounded-lg">
      <h3 className="mb-4 text-xl font-bold text-white">All time overview</h3>
      <div>
        <DashPie
          name="Expenses"
          data={data.filter((pcd) => pcd.totalAmount < 0)}
        />
        <DashPie
          name="Income"
          data={data.filter((pcd) => pcd.totalAmount > 0)}
        />
      </div>
    </div>
  );
};

export default DashboardPies;
