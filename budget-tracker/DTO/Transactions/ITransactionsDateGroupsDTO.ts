import { ICategoryTransactionDetailsDTO } from "../Categories/ICategoryTransactionDetailsDTO";

export default interface ITransactionsDateGroupsDTO {
  date: string;
  categoryTransactions: ICategoryTransactionDetailsDTO[];
}
