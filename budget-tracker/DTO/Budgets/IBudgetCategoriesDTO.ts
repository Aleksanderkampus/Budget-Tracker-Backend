import { ISimpleCategory } from "../Categories/ISimpleCategory";

export interface IBudgetCategoriesDTO {
  category: ISimpleCategory;
  totalAmount: number;
}
