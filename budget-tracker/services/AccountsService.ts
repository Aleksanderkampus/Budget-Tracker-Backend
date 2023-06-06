import Account from "@/domain/Account";
import { BaseEntityService } from "./BaseEntityService";

export class AccountsService extends BaseEntityService<Account> {
  constructor() {
    super("v1/Accounts");
  }
}
