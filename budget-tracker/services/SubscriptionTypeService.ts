import { ISimpleCurrency } from "@/DTO/ISimpleCurrency";
import { BaseEntityService } from "./BaseEntityService";
import ISubscriptionType from "@/DTO/Subscriptions/ISubscriptionType";

export class SubscriptionTypeService extends BaseEntityService<ISubscriptionType> {
  constructor() {
    super("v1/SubscriptionTypes");
  }
}
