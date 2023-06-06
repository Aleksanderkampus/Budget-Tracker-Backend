import ISimpleSubscription from "@/DTO/Subscriptions/ISimpleSubscription";
import { BaseEntityService } from "./BaseEntityService";
import ISubscriptionDetails from "@/DTO/Subscriptions/ISubscriptionDetails";

export class SubscriptionService extends BaseEntityService<ISubscriptionDetails> {
  constructor() {
    super("v1/Subscriptions");
  }

  async getAllSimpleSubscriptions(
    jwt: string
  ): Promise<ISimpleSubscription[] | undefined> {
    try {
      const response = await this.axios.get<ISimpleSubscription[]>("simple", {
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
  ): Promise<ISubscriptionDetails | undefined> {
    try {
      const response = await this.axios.get<ISubscriptionDetails>(
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
}
