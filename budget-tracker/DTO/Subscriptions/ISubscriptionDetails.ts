import { IBaseEntity } from "@/DTO/IBaseEntity";
import { ISimpleCurrency } from "../ISimpleCurrency";

export default interface ISubscriptionDetails extends IBaseEntity {
  name: string;
  amount: number;
  dateStarted: string;
  subscriptionTypeId: string;
  accountId: string;
  currencyId: string;
  currency?: ISimpleCurrency;
}
