import { ISimpleCategory } from "../Categories/ISimpleCategory";
import { ISimpleTransactionDTO } from "./ISimpleTransactionDTO";

export default interface ITransactionWithCategories {
  transaction: ISimpleTransactionDTO;
  categories: ISimpleCategory[];
}
