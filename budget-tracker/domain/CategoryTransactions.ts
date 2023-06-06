import { FinancialCategory } from "./FinancialCategory";
import { IBaseEntity } from "../DTO/IBaseEntity";
import { Transaction } from "./Transaction";

export interface CategoryTransaction extends IBaseEntity {
  transactionId?: string;
  transaction?: Transaction;
  financialCategoryId?: string;
  financialCategory?: FinancialCategory;
}
