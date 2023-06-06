import { IBaseEntity } from "@/DTO/IBaseEntity";

export default interface ISimpleAccount extends IBaseEntity {
  name: string;
  bank: string;
}
