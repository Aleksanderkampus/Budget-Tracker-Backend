import { CategoryTransaction } from "./CategoryTransactions";
import { IBaseEntity } from "../DTO/IBaseEntity";

export interface Transaction extends IBaseEntity {
  senderReceiver: string;
  amount: string;
  time: string;
  accountId: string;
  currencyId: string;
  categoryTransactions: CategoryTransaction[];
}
