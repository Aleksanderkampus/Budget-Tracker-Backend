import { IBaseEntity } from "@/DTO/IBaseEntity";
import { ISimpleCurrency } from "../ISimpleCurrency";
import { ITransactionCategoryDTO } from "./ITransactionCategoryDTO";

export interface ITransactionDetailsDTO extends IBaseEntity {
  senderReceiver: string;
  amount: string;
  time: string;
  accountId: string;
  currencyId: string;
  currency?: ISimpleCurrency;
  categoryTransactions: ITransactionCategoryDTO[];
}
