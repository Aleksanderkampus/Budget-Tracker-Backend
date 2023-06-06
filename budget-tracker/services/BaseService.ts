import SVGHelper from "@/helpers/Helper";
import { JwtContext } from "@/pages/_app";
import Axios, { AxiosInstance } from "axios";
import { useContext } from "react";
import { RefreshTokenService } from "./RefreshTokenService";
import { IJWTResponse } from "@/DTO/Identity/IJWTResponse";

export abstract class BaseService {
  private static hostBaseURL = process.env.NEXT_PUBLIC_BACKEND_URL;

  protected axios: AxiosInstance;

  private jwt = useContext(JwtContext);

  constructor(baseURL: string) {
    this.axios = Axios.create({
      baseURL: BaseService.hostBaseURL + baseURL,
      headers: {
        common: {
          "Content-Type": "application/json",
        },
      },
    });

    this.axios.interceptors.request.use(async (request) => {
      console.log("Starting Request", JSON.stringify(request.url, null, 2));
      if (
        this.jwt.jwtResponse &&
        this.isAccessTokenExpired(this.jwt.jwtResponse.jwt)
      ) {
        const newAccessToken = await RefreshTokenService.refreshToken(
          this.jwt.jwtResponse
        );
        request.headers.Authorization = `Bearer ${newAccessToken!.data.jwt}`;
        this.jwt.setJwtResponse!(newAccessToken!.data);
        console.log("we made it here");
      } else if (this.jwt.jwtResponse) {
        request.headers.Authorization = `Bearer ${this.jwt.jwtResponse.jwt}`;
      }
      return request;
    });
  }

  private isAccessTokenExpired = (accessToken: string) => {
    const decodedTime = SVGHelper.parseJwt(accessToken).exp;
    return decodedTime * 1000 < Date.now();
  };
}
