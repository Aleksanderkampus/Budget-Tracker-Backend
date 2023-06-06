import { IBaseEntity } from "@/DTO/IBaseEntity";
import { BaseService } from "./BaseService";
import { Transaction } from "@/domain/Transaction";

export abstract class BaseEntityService<
  TEntity extends IBaseEntity
> extends BaseService {
  constructor(baseUrl: string) {
    super(baseUrl);
  }

  async getAll(jwt: string): Promise<TEntity[] | undefined> {
    try {
      const response = await this.axios.get<TEntity[]>("", {
        headers: {
          Authorization: "Bearer " + jwt,
        },
      });

      console.log("Get all", response);
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

  async get(jwt: string, id: string): Promise<TEntity | undefined> {
    try {
      const response = await this.axios.get<TEntity>(id, {
        headers: {
          Authorization: "Bearer " + jwt,
        },
      });

      console.log("Get single", response);
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

  async delete(jwt: string, id: string): Promise<TEntity | undefined> {
    try {
      const response = await this.axios.delete<TEntity>(id, {
        headers: {
          Authorization: "Bearer " + jwt,
        },
      });

      console.log("Delete", response);
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

  async add(jwt: string, data: TEntity): Promise<TEntity | undefined> {
    try {
      const response = await this.axios.post<TEntity>("", data, {
        headers: {
          Authorization: "Bearer " + jwt,
          "Content-Type": "application/json",
        },
      });

      console.log("Save", response);
      if (response.status === 200) {
        return response.data;
      }
      return undefined;
    } catch (e) {
      console.log("error: ", (e as Error).message);
      return undefined;
    }
  }

  async put(jwt: string, data: TEntity): Promise<TEntity | undefined> {
    try {
      const response = await this.axios.put<TEntity>(data.id!, data, {
        headers: {
          Authorization: "Bearer " + jwt,
          "Content-Type": "application/json",
        },
      });

      console.log("Save", response);
      if (response.status === 204) {
        return response.data;
      }
      return undefined;
    } catch (e) {
      console.log(e);
      console.log("error: ", (e as Error).message);
      return undefined;
    }
  }
}
