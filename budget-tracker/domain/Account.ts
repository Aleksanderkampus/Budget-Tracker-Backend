import { IBaseEntity } from "../DTO/IBaseEntity";
import { Transaction } from "./Transaction";

export default interface Account extends IBaseEntity {
  bank: string;
  name: string;
  transactions?: Transaction[];
  userId: string;
}
