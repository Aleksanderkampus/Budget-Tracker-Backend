import { IJWTResponse } from "@/DTO/Identity/IJWTResponse";
import Axios from "axios";

export abstract class RefreshTokenService {
  private static hostBaseURL =
    process.env.NEXT_PUBLIC_BACKEND_URL + "/v1/identity/account/refreshtoken";

  protected static axios = Axios.create({
    baseURL: RefreshTokenService.hostBaseURL,
    headers: {
      common: {
        "Content-Type": "application/json",
      },
    },
  });

  public static refreshToken = async (jwt: IJWTResponse) => {
    try {
      const response = await RefreshTokenService.axios.post<IJWTResponse>(
        "",
        jwt
      );
      return response;
    } catch (error) {
      console.error("Error refreshing token:", error);
      // Handle the error here (e.g. logout the user)
    }
  };
}
