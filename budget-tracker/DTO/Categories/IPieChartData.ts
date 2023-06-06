import { IBaseEntity } from "@/DTO/IBaseEntity";

export default interface IPieChartData extends IBaseEntity {
  name: string;
  hexColor: string;
  totalAmount: number;
}
