import { IBaseEntity } from "@/DTO/IBaseEntity";
import { ISimpleCategory } from "../Categories/ISimpleCategory";

export default interface ISimpleBudgetDTO extends IBaseEntity {
  name: string;
  amountToSave: number;
  amountSpent: number;
  currencySymbol: string;
  dateFrom: string;
  dateTo: string;
  simpleBudgetCategories: ISimpleCategory[];
}
