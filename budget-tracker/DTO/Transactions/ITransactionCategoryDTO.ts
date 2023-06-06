import { IBaseEntity } from "@/DTO/IBaseEntity";
import { ISimpleCategory } from "../Categories/ISimpleCategory";

export interface ITransactionCategoryDTO extends IBaseEntity {
  financialCategoryId: string;
  financialCategory?: ISimpleCategory;
  amount: string;
}
