import ModalLayout from "../../Modal/ModalLayout";
import Image from "next/image";
import CategoryWithAmount from "../DetailsUI/CategoryWithAmount";
import CancelButton from "../../UI/Button/CancelButton";
import DetailsHeader from "../DetailsUI/DetailsHeader";
import { useContext, useEffect, useState } from "react";
import TransactionsService from "@/services/TransactionsService";
import { JwtContext } from "@/pages/_app";
import { ITransactionDetailsDTO } from "@/DTO/Transactions/ITransactionDetailsDTO";

interface ITransactionDetailsProps {
  detailsOpen: boolean;
  id: string;
  handleDetailsOpen: () => void;
}

const TransactionDetails = (props: ITransactionDetailsProps) => {
  const [transactionDetails, setTransactionDetails] = useState<
    ITransactionDetailsDTO | undefined
  >(undefined);
  const transactionService = new TransactionsService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);

  useEffect(() => {
    if (props.id && jwtResponse) {
      transactionService.getDetails(jwtResponse.jwt, props.id).then((res) => {
        if (res) {
          setTransactionDetails(res);
        }
      });
    }
  }, [props.id]);

  return (
    <>
      {props.detailsOpen && transactionDetails && (
        <ModalLayout onClick={props.handleDetailsOpen}>
          <>
            <DetailsHeader
              icon={
                transactionDetails!.categoryTransactions.length > 1
                  ? undefined
                  : transactionDetails!.categoryTransactions[0]
                      .financialCategory!.icon
              }
              name={transactionDetails!.categoryTransactions
                .map((e) => e.financialCategory!.name)
                .join(", ")}
              hexValues={transactionDetails!.categoryTransactions.map(
                (e) => e.financialCategory!.hexColor
              )}
              amount={
                transactionDetails!.amount +
                transactionDetails!.currency!.symbol
              }
            />
            <div className="px-6 py-6 lg:px-8">
              <div className="flex items-center text-white mb-5 justify-between">
                <h3>Date</h3>
                <h3>
                  {new Date(transactionDetails!.time).toLocaleDateString(
                    "de-DE",
                    {
                      weekday: "short",
                      day: "2-digit",
                      month: "2-digit",
                      year: "numeric",
                      hour: "2-digit",
                      minute: "2-digit",
                    }
                  )}
                </h3>
              </div>
              <div className="flex items-center justify-between">
                <h3 className="text-white font-bold">Categories</h3>
                <h3 className="text-white font-bold">Amount</h3>
              </div>
              {transactionDetails!.categoryTransactions.map((ct, i) => {
                return (
                  <CategoryWithAmount
                    key={i}
                    amount={ct.amount.toString()}
                    categories={[ct.financialCategory!]}
                    name={ct.financialCategory!.name}
                  />
                );
              })}

              <h2 className="text-white font-bold">Account</h2>
              <div className="flex items-center px-2 py-4 mb-5 text-gray-900 whitespace-nowrap dark:text-white">
                <Image src="./cardIcon.svg" height={25} width={25} alt="" />
                <h2 className="ml-3">Cash Account</h2>
              </div>
              <CancelButton onClick={props.handleDetailsOpen} />
            </div>
          </>
        </ModalLayout>
      )}
    </>
  );
};

export default TransactionDetails;
