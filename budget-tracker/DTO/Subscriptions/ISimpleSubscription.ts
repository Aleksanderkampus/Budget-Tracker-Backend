import { IBaseEntity } from "@/DTO/IBaseEntity";
import ISubscriptionType from "./ISubscriptionType";

export default interface ISimpleSubscription extends IBaseEntity {
  name: string;
  amount: number;
  dateStarted: string;
  nextPayment: string;
  subscriptionTypeId: string;
  subscriptionType: ISubscriptionType;
}
