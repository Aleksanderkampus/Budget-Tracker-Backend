import ModalLayout from "@/components/Modal/ModalLayout";
import DetailsHeader from "../DetailsUI/DetailsHeader";
import ProgressBar from "@/components/UI/ProgressBar/ProgressBar";
import CategoryWithAmount from "../DetailsUI/CategoryWithAmount";
import { BudgetService } from "@/services/BudgetService";
import { JwtContext } from "@/pages/_app";
import { useContext, useEffect, useState } from "react";
import IBudgetDetailsDTO from "@/DTO/Budgets/IBudgetDetailsDTO";
import TransactionDay from "../DetailsUI/TransactionDay";

interface IBudgetDetails {
  id: string;
  handleClose: () => void;
}

const BudgetDetails = (props: IBudgetDetails) => {
  const budgetService = new BudgetService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const [details, setDetails] = useState<IBudgetDetailsDTO | undefined>();

  useEffect(() => {
    if (jwtResponse && props.id) {
      budgetService.getBudgetDetails(jwtResponse.jwt, props.id).then((res) => {
        setDetails(res);
      });
    }
  }, [jwtResponse]);

  return (
    <>
      {details && (
        <ModalLayout onClick={props.handleClose}>
          <>
            <DetailsHeader
              amount={
                details.simpleBudget.amountToSave +
                details.simpleBudget.currencySymbol
              }
              hexValues={details.simpleBudget.simpleBudgetCategories.map(
                (e) => e.hexColor
              )}
              name={details.simpleBudget.name}
              icon={
                details.simpleBudget.simpleBudgetCategories.length > 1
                  ? undefined
                  : details.simpleBudget.simpleBudgetCategories[0].icon
              }
            />
            <div className="px-6 py-6 lg:px-8">
              <h2 className="text-white text-lg font-bold">Progress</h2>
              <ProgressBar
                total={details.simpleBudget.amountToSave}
                amountSpent={details.simpleBudget.amountSpent}
                currency={details.simpleBudget.currencySymbol}
              />
              <div className="flex mt-3 items-center justify-between">
                <h3 className="text-white font-bold">Categories</h3>
                <h3 className="text-white font-bold">Amount</h3>
              </div>
              {details.budgetCategories.map((e, i) => (
                <CategoryWithAmount
                  amount={e.totalAmount + details.simpleBudget.currencySymbol}
                  key={i}
                  name={e.category.name}
                  categories={[e.category]}
                />
              ))}

              <h3 className="text-white text-lg font-bold">Transactions</h3>
              {details.budgetTransactions.map((e, i) => (
                <TransactionDay
                  key={i}
                  date={e.date}
                  transactions={e.categoryTransactions}
                />
              ))}
              {details.budgetTransactions.length === 0 && (
                <div className="text-white text-center">No transactions</div>
              )}
            </div>
          </>
        </ModalLayout>
      )}
    </>
  );
};

export default BudgetDetails;
