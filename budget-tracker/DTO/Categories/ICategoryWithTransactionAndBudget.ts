import { ISimpleCategory } from "./ISimpleCategory";

export interface ICategoryWithTransactionsAndBudget extends ISimpleCategory {
  amountSpent: number;
  numberOfBudgets: number;
  numberOfTransactions: number;
}
