import { IBaseEntity } from "@/DTO/IBaseEntity";
import ITransactionWithCategories from "../Transactions/ITransactionWithCategories";

export interface ICategoryTransactionDetailsDTO extends IBaseEntity {
  transaction: ITransactionWithCategories;
  amount: string;
}
