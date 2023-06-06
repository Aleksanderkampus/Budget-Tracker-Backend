import { ISimpleCurrency } from "@/DTO/ISimpleCurrency";
import { BaseEntityService } from "./BaseEntityService";
import ISimpleBudgetDTO from "@/DTO/Budgets/SimpleBudgetDTO";
import IBudgetDetailsDTO from "@/DTO/Budgets/IBudgetDetailsDTO";
import IBudgetToEdit from "@/DTO/Budgets/IBudgetToEdit";

export class BudgetService extends BaseEntityService<IBudgetToEdit> {
  constructor() {
    super("v1/Budgets");
  }

  async getAllSimpleBudgets(
    jwt: string
  ): Promise<ISimpleBudgetDTO[] | undefined> {
    try {
      const response = await this.axios.get<ISimpleBudgetDTO[]>("simple", {
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

  async getBudgetDetails(
    jwt: string,
    id: string
  ): Promise<IBudgetDetailsDTO | undefined> {
    try {
      const response = await this.axios.get<IBudgetDetailsDTO>(
        "details/" + id,
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

  async getBudgetToEdit(
    jwt: string,
    id: string
  ): Promise<IBudgetToEdit | undefined> {
    try {
      const response = await this.axios.get<IBudgetToEdit>("edit/" + id, {
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
