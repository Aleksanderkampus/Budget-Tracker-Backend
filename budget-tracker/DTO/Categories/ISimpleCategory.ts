import { IBaseEntity } from "@/DTO/IBaseEntity";

export interface ISimpleCategory extends IBaseEntity {
  name: string;
  hexColor: string;
  icon: string;
}
