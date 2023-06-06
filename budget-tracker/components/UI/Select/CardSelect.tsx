import Account from "@/domain/Account";
import { JwtContext } from "@/pages/_app";
import { AccountsService } from "@/services/AccountsService";
import Image from "next/image";
import { useContext, useEffect, useState } from "react";
interface ICardSelectProps {
  handleAccountChange: (account: Account) => void;
  value?: string;
}

const CardSelect = (props: ICardSelectProps) => {
  const [accounts, setAccounts] = useState([] as Account[]);
  const accountService = new AccountsService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);

  useEffect(() => {
    if (jwtResponse) {
      accountService.getAll(jwtResponse.jwt).then((res) => {
        setAccounts(res ?? []);
      });
    }
  }, [jwtResponse]);

  const handleChange = (e: EventTarget & HTMLSelectElement) => {
    if (e.value !== "") {
      props.handleAccountChange(accounts.find((a) => a.id === e.value)!);
    }
  };

  return (
    <>
      <label
        htmlFor="countries"
        className="block  text-sm font-medium text-gray-900 dark:text-white"
      >
        Account
      </label>
      <div className="flex">
        <div>
          <div className="flex-shrink-0 z-10 inline-flex items-center py-2.5 px-4  text-sm font-medium text-center border-2 rounded-l-lg  bg-background text-white border-white">
            <Image src="../cardIcon.svg" height={35} width={35} alt="" />
          </div>
        </div>

        <select
          id="countries"
          onChange={(e) => handleChange(e.target)}
          value={props.value !== "" ? props.value : ""}
          className="border-2 text-sm rounded-r-lg block w-full p-2.5 border-l-0 bg-background border-white placeholder-gray-400 text-white"
        >
          <option value="">Choose an account</option>

          {accounts.map((e, i) => {
            return (
              <option key={i} value={e.id}>
                {e.name}
              </option>
            );
          })}
        </select>
      </div>
    </>
  );
};

export default CardSelect;
