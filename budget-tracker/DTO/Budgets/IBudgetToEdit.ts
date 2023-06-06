import ISimpleAccount from "../ISimpleAccount";
import { ISimpleCategory } from "../Categories/ISimpleCategory";
import { ISimpleCurrency } from "../ISimpleCurrency";
import { IBaseEntity } from "@/DTO/IBaseEntity";

export default interface IBudgetToEdit extends IBaseEntity {
  name: string;
  amountToSave: number;
  currencyId: string;
  currency?: ISimpleCurrency;
  account: ISimpleAccount[];
  dateFrom: string;
  dateTo: string;
  simpleBudgetCategories: ISimpleCategory[];
}
