import axios, { AxiosError } from "axios";
import { BaseService } from "./BaseService";
import { IJWTResponse } from "@/DTO/Identity/IJWTResponse";
import { IRegsiterData } from "@/DTO/Identity/IRegisterData";
import { ILoginData } from "@/DTO/Identity/ILoginData";
import { IJwtError } from "@/DTO/Identity/IJwtError";

export class IdentityService extends BaseService {
  constructor() {
    super("v1/identity/account/");
  }

  async register(
    data: IRegsiterData,
    errorHandle: (errorMessage: string) => void
  ): Promise<IJWTResponse | undefined> {
    try {
      const response = await this.axios.post<IJWTResponse>("register", data);

      console.log("register response", response);
      if (response.status === 200) {
        return response.data;
      }
      return undefined;
    } catch (e) {
      const resData = (e as AxiosError).response!.data as unknown as IJwtError;
      errorHandle(resData.error);
      return undefined;
    }
  }

  async login(
    data: ILoginData,
    errorHandle: (errorMessage: string) => void
  ): Promise<IJWTResponse | undefined> {
    try {
      const response = await this.axios.post<IJWTResponse>("login", data);

      console.log("login response", response);
      if (response.status === 200) {
        return response.data;
      }
      return undefined;
    } catch (e) {
      console.log(e);
      const resData = (e as AxiosError).response!.data as unknown as IJwtError;
      errorHandle(resData.error);
      return undefined;
    }
  }

  async logout(data: IJWTResponse): Promise<true | undefined> {
    console.log(data);

    try {
      const response = await this.axios.post("logout", data, {
        headers: {
          Authorization: "Bearer " + data.jwt,
        },
      });

      console.log("logout response", response);
      if (response.status === 200) {
        return true;
      }
      return undefined;
    } catch (e) {
      console.log("error: ", (e as Error).message);
      return undefined;
    }
  }
}
