import { CategoryTransaction } from "./CategoryTransactions";
import { IBaseEntity } from "../DTO/IBaseEntity";
import { Transaction } from "./Transaction";

export interface FinancialCategory extends IBaseEntity {
  name: string;
  hexColor: string;
  icon: string;
  categoryTransactions: CategoryTransaction[];
}
