import { IBaseEntity } from "@/DTO/IBaseEntity";
import { StringLiteralLike } from "typescript";

export interface ISimpleCurrency extends IBaseEntity {
  name: string;
  abbreviation: string;
  symbol: string;
}
