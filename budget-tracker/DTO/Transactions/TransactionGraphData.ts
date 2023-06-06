import { IBaseEntity } from "@/DTO/IBaseEntity";

export default interface ITransactionGraphData extends IBaseEntity {
  weekDay: string;
  totalAmount: number;
}
