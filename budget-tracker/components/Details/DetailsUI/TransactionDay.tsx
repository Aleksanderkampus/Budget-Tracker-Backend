import { ICategoryTransactionDetailsDTO } from "@/DTO/Categories/ICategoryTransactionDetailsDTO";
import CategoryWithAmount from "./CategoryWithAmount";
interface ITransactionDayProps {
  date: string;
  transactions: ICategoryTransactionDetailsDTO[];
}

const TransactionDay = (props: ITransactionDayProps) => {
  return (
    <>
      {props.transactions.length > 0 && (
        <>
          <h3 className="text-white text-xs">
            {new Date(props.date).toDateString()}
          </h3>
          <div className="border-2 rounded-md p-2 border-black">
            {props.transactions.map((e, i) => (
              <CategoryWithAmount
                key={i}
                amount={
                  e.amount === e.transaction.transaction.amount
                    ? e.amount + e.transaction.transaction.currencySymbol
                    : e.amount +
                      e.transaction.transaction.currencySymbol +
                      "/" +
                      e.transaction.transaction.amount
                }
                name={e.transaction.transaction.senderReceiver}
                categories={e.transaction.categories}
              />
            ))}
          </div>
        </>
      )}
    </>
  );
};

export default TransactionDay;
