import { IBaseEntity } from "@/DTO/IBaseEntity";
import { ICategoryTransactionDetailsDTO } from "../Categories/ICategoryTransactionDetailsDTO";

export interface ISimpleTransactionDTO extends IBaseEntity {
  senderReceiver: string;
  amount: string;
  time: string;
  currencySymbol: string;
}
