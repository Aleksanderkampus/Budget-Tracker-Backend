import { IBaseEntity } from "@/DTO/IBaseEntity";
import { IGraphData } from "./IGraphData";
import ITransactionsDateGroupsDTO from "../Transactions/ITransactionsDateGroupsDTO";

export interface ICategoryDetailsDTO extends IBaseEntity {
  name: string;
  hexColor: string;
  icon: string;
  amountSpent: number;
  graphData: IGraphData[];
  categoryTransactionsDateGroups: ITransactionsDateGroupsDTO[];
}
