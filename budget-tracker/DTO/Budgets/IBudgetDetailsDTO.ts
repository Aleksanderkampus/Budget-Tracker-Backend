import { IBudgetCategoriesDTO } from "./IBudgetCategoriesDTO";
import ITransactionsDateGroupsDTO from "../Transactions/ITransactionsDateGroupsDTO";
import ISimpleBudgetDTO from "./SimpleBudgetDTO";

export default interface IBudgetDetailsDTO {
  simpleBudget: ISimpleBudgetDTO;
  budgetCategories: IBudgetCategoriesDTO[];
  budgetTransactions: ITransactionsDateGroupsDTO[];
}
