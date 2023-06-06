import { FinancialCategory } from "@/domain/FinancialCategory";
import { BaseEntityService } from "./BaseEntityService";
import { ISimpleCategory } from "@/DTO/Categories/ISimpleCategory";
import { ICategoryWithTransactionsAndBudget } from "@/DTO/Categories/ICategoryWithTransactionAndBudget";
import { ICategoryDetailsDTO } from "@/DTO/Categories/ICategoryDetailsDTO";
import IPieChartData from "@/DTO/Categories/IPieChartData";

export class FinancialCategoryService extends BaseEntityService<ICategoryWithTransactionsAndBudget> {
  constructor() {
    super("v1/FinancialCategory");
  }

  async getAllSimpleCategories(
    jwt: string
  ): Promise<ISimpleCategory[] | undefined> {
    try {
      const response = await this.axios.get<ISimpleCategory[]>("simple", {
        headers: {
          Authorization: "Bearer " + jwt,
        },
      });

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

  async getAllCategoryWithTransactionAndBudget(
    jwt: string
  ): Promise<ICategoryWithTransactionsAndBudget[] | undefined> {
    try {
      const response = await this.axios.get<
        ICategoryWithTransactionsAndBudget[]
      >("", {
        headers: {
          Authorization: "Bearer " + jwt,
        },
      });

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

  async getDetails(
    jwt: string,
    id: string
  ): Promise<ICategoryDetailsDTO | undefined> {
    try {
      const response = await this.axios.get<ICategoryDetailsDTO>(
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

  async getPieChartData(jwt: string): Promise<IPieChartData[] | undefined> {
    try {
      const response = await this.axios.get<IPieChartData[]>("piechart", {
        headers: {
          Authorization: "Bearer " + jwt,
        },
      });

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
