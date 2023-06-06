import { Transaction } from "@/domain/Transaction";
import { BaseEntityService } from "./BaseEntityService";
import { ITransactionDetailsDTO } from "@/DTO/Transactions/ITransactionDetailsDTO";
import ITransactionGraphData from "@/DTO/Transactions/TransactionGraphData";
import ITransactionWithCategories from "@/DTO/Transactions/ITransactionWithCategories";

export default class TransactionsService extends BaseEntityService<ITransactionDetailsDTO> {
  constructor() {
    super("v1/Transactions");
  }

  async getDetails(
    jwt: string,
    id: string
  ): Promise<ITransactionDetailsDTO | undefined> {
    try {
      const response = await this.axios.get<ITransactionDetailsDTO>(
        "/details/" + id,
        {
          headers: {
            Authorization: "Bearer " + jwt,
          },
        }
      );

      console.log("Get details", response);
      if (response.status === 200) {
        return response.data;
      }
      return undefined;
    } catch (e) {
      /* Kirjuta axios interseptor */
      console.log("error: ", (e as Error).message);
      return undefined;
    }
  }

  async getTransactionWithCategory(
    jwt: string
  ): Promise<ITransactionWithCategories[] | undefined> {
    try {
      const response = await this.axios.get<ITransactionWithCategories[]>("", {
        headers: {
          Authorization: "Bearer " + jwt,
        },
      });

      console.log("Get details", response);
      if (response.status === 200) {
        return response.data;
      }
      return undefined;
    } catch (e) {
      /* Kirjuta axios interseptor */
      console.log("error: ", (e as Error).message);
      return undefined;
    }
  }

  async getGraphData(
    jwt: string
  ): Promise<ITransactionGraphData[] | undefined> {
    try {
      const response = await this.axios.get<ITransactionGraphData[]>(
        "graphdata",
        {
          headers: {
            Authorization: "Bearer " + jwt,
          },
        }
      );

      console.log("Get simple all", response);
      if (response.status === 200) {
        return response.data;
      }
      return undefined;
    } catch (e) {
      /* Kirjuta axios interseptor */
      console.log("error: ", (e as Error).message);
      return undefined;
    }
  }
}
