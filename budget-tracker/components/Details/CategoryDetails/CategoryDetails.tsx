import ModalLayout from "@/components/Modal/ModalLayout";
import DetailsHeader from "../DetailsUI/DetailsHeader";
import { BarChart, Bar, XAxis, LabelList, ResponsiveContainer } from "recharts";
import AnalyticsGraph from "../DetailsUI/AnalyticsGraph";
import CategoryWithAmount from "../DetailsUI/CategoryWithAmount";
import TransactionDay from "../DetailsUI/TransactionDay";
import { JwtContext } from "@/pages/_app";
import TransactionsService from "@/services/TransactionsService";
import { useContext, useEffect, useState } from "react";
import { FinancialCategoryService } from "@/services/FinancialCategoryService";
import Helper from "@/helpers/Helper";
import { IGraphData } from "@/DTO/Categories/IGraphData";
import { ICategoryDetailsDTO } from "@/DTO/Categories/ICategoryDetailsDTO";
import { ISimpleCategory } from "@/DTO/Categories/ISimpleCategory";

interface ICategoryDetailsProps {
  id: string;
  detailsOpen: boolean;
  handleDetailsOpen: () => void;
}

const data = [
  {
    name: "Jan",
    pv: 2400,
  },
  {
    name: "Feb",
    pv: 1398,
  },
  {
    name: "Mar",
    pv: 8,
  },
  {
    name: "Apr",
    pv: 3908,
  },
  {
    name: "May",
    pv: 4800,
  },
  {
    name: "Jun",
    pv: 2800,
  },
];

const CategoryDetails = (props: ICategoryDetailsProps) => {
  const categoryService = new FinancialCategoryService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const [categoryDetails, setCategoryDetails] = useState<
    ICategoryDetailsDTO | undefined
  >(undefined);

  useEffect(() => {
    if (props.id && jwtResponse) {
      categoryService.getDetails(jwtResponse.jwt, props.id).then((res) => {
        if (res) {
          console.log(res);
          res.graphData = Helper.getLastSixMonths()
            .map((e) => {
              return {
                month: e,
                amount:
                  res.graphData.find((g) => g.month === e) === undefined
                    ? 0
                    : res.graphData.find((g) => g.month === e)!.amount,
              } as IGraphData;
            })
            .reverse();
          setCategoryDetails(res);
        }
      });
    }
  }, [props.id]);

  return (
    <>
      {props.detailsOpen && categoryDetails && (
        <ModalLayout onClick={() => props.handleDetailsOpen()}>
          <>
            <DetailsHeader
              amount={categoryDetails.amountSpent.toString()}
              hexValues={[categoryDetails.hexColor]}
              name={categoryDetails.name}
              icon={categoryDetails.icon}
            />
            <div className="px-6 py-6 lg:px-8">
              <div className="flex items-center justify-between">
                <h3 className="text-white font-bold">Categories</h3>
                <h3 className="text-white font-bold">Amount</h3>
              </div>
              <CategoryWithAmount
                amount={categoryDetails.amountSpent.toString()}
                name={categoryDetails.name}
                categories={[
                  {
                    name: categoryDetails.name,
                    hexColor: categoryDetails.hexColor,
                    icon: categoryDetails.icon,
                  } as ISimpleCategory,
                ]}
              />
              <AnalyticsGraph
                data={categoryDetails.graphData}
                total={categoryDetails.amountSpent.toString()}
              />
              <h3 className="text-white text-lg font-bold">Transactions</h3>
              {categoryDetails.categoryTransactionsDateGroups.map((e, i) => (
                <TransactionDay
                  key={i}
                  date={e.date}
                  transactions={e.categoryTransactions}
                />
              ))}
            </div>
          </>
        </ModalLayout>
      )}
    </>
  );
};

export default CategoryDetails;
