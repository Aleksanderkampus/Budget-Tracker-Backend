import ISubscriptionType from "@/DTO/Subscriptions/ISubscriptionType";
import { JwtContext } from "@/pages/_app";
import { SubscriptionTypeService } from "@/services/SubscriptionTypeService";
import { useState, useContext, useEffect } from "react";

interface ISubscriptionTypeSelectProps {
  value: string;
  handleChange: (id: string) => void;
}

const SubscriptionTypeSelect = (props: ISubscriptionTypeSelectProps) => {
  const [types, setTypes] = useState([] as ISubscriptionType[]);
  const typeService = new SubscriptionTypeService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);

  useEffect(() => {
    if (jwtResponse) {
      console.log(props.value);
      typeService.getAll(jwtResponse.jwt).then((res) => {
        setTypes(res ?? []);
        console.log(types);
      });
    }
  }, [jwtResponse]);

  return (
    <>
      <label
        htmlFor="countries"
        className="block  text-sm font-medium text-gray-900 dark:text-white"
      >
        Type
      </label>
      <div className="flex">
        <select
          id="countries"
          onChange={(e) => props.handleChange(e.target.value)}
          value={props.value}
          className="border-2 text-sm rounded-lg block w-full p-2.5  bg-background border-white placeholder-gray-400 text-white"
        >
          <option value="">Choose a type</option>
          {types.map((e, i) => {
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

export default SubscriptionTypeSelect;
