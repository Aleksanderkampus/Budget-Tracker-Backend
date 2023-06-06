import { ISimpleCurrency } from "@/DTO/ISimpleCurrency";
import { BaseEntityService } from "./BaseEntityService";

export class CurrencyService extends BaseEntityService<ISimpleCurrency> {
  constructor() {
    super("v1/Currency");
  }
}
