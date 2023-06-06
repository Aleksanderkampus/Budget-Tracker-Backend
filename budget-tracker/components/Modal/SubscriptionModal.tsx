import { FormEvent, useContext, useEffect, useState } from "react";
import CancelButton from "../UI/Button/CancelButton";
import SubmitButton from "../UI/Button/SubmitButton";
import Input from "../UI/Input/Input";
import CardSelect from "../UI/Select/CardSelect";
import TimePicker from "../UI/TimePicker/TimePicker";
import ChooseCurrency from "./ChooseCurrency/ChooseCurrency";
import ModalLayout from "./ModalLayout";
import ISubscriptionDetails from "@/DTO/Subscriptions/ISubscriptionDetails";
import { ISimpleCurrency } from "@/DTO/ISimpleCurrency";
import Account from "@/domain/Account";
import { SubscriptionService } from "@/services/SubscriptionService";
import { JwtContext } from "@/pages/_app";
import SubscriptionTypeSelect from "../UI/Select/SubscriptionTypeSelect";

interface ISubscriptionModalProps {
  id?: string;
  handelOpening: (bool: boolean) => void;
  children: JSX.Element;
  modalOpen: boolean;
  updateTabel: () => void;
}

const emptyState = {
  name: "",
  amount: 0,
  dateStarted: new Date().toISOString(),
  subscriptionTypeId: "",
  accountId: "",
  currencyId: "",
  currency: {} as ISimpleCurrency,
} as ISubscriptionDetails;

const SubscriptionModal = (props: ISubscriptionModalProps) => {
  const [subscription, setSubscription] =
    useState<ISubscriptionDetails>(emptyState);
  const subscriptionService = new SubscriptionService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const [errors, setErrors] = useState<string | undefined>(undefined);

  useEffect(() => {
    if (props.id === "") {
      setSubscription(emptyState);
    } else if (jwtResponse && props.id) {
      subscriptionService.getDetails(jwtResponse.jwt, props.id).then((res) => {
        console.log(res);
        setSubscription(res!);
      });
    }
  }, [jwtResponse, props.id]);

  const handleChange = (target: EventTarget & HTMLInputElement) => {
    if (target.name === "amount") {
      const pattern = /^\d+(\.\d+)?$/;
      if (!pattern.test(target.value) && target.value !== "") {
        setErrors("Amount must be a positive number");
        return;
      }
    }
    setSubscription({ ...subscription, [target.name]: target.value });
  };

  const formValidated = (): boolean => {
    var toReturn = true;
    if (subscription.name === "") {
      setErrors("Subscription must have a name");
      toReturn = false;
    } else if (subscription.amount === 0) {
      setErrors("Amount cannot be 0");
      toReturn = false;
    } else if (subscription.accountId === "") {
      setErrors("Must choose an account");
      toReturn = false;
    } else if (subscription.currencyId === "") {
      setErrors("Must have a currency");
      toReturn = false;
    } else if (subscription.subscriptionTypeId === "") {
      setErrors("Subscription must have a type");
      toReturn = false;
    }

    return toReturn;
  };
  const handleCurrencyChange = (c: ISimpleCurrency) => {
    setSubscription({
      ...subscription,
      ["currencyId"]: c.id!,
      ["currency"]: c,
    });
  };

  const handleAccountChange = (account: Account) => {
    setSubscription({
      ...subscription,
      ["accountId"]: account.id!,
    });
  };

  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault();
    if (!formValidated()) {
      return;
    }
    const copy = { ...subscription };
    copy.currency = undefined;
    var res;
    if (props.id === "") {
      res = await subscriptionService.add(jwtResponse!.jwt, copy);
    } else {
      res = await subscriptionService.put(jwtResponse!.jwt, copy);
    }
    if (res !== undefined) {
      props.updateTabel();
      setErrors(undefined);
      props.handelOpening(false);
    }
  };

  const handleDateFrom = (date?: Date, time?: string) => {
    const copyValues = { ...subscription };
    const dateObj = new Date(subscription.dateStarted);
    dateObj.setDate(date!.getDate());
    copyValues.dateStarted = dateObj.toISOString();
    setSubscription({ ...copyValues });
  };

  const handleTypeChange = (id: string) => {
    setSubscription({ ...subscription, ["subscriptionTypeId"]: id });
  };

  return (
    <>
      {props.modalOpen && (
        <ModalLayout
          onClick={() => {
            props.handelOpening(false);
          }}
        >
          <>
            <div className="px-6 py-6 lg:px-8">
              <h3 className="mb-4 text-xl font-medium  text-white">
                {props.id !== "" ? "Edit subscription" : "Add subscription"}
              </h3>
              <form className="space-y-4" onSubmit={handleSubmit}>
                <Input
                  label="Name"
                  name="name"
                  type="text"
                  onChange={handleChange}
                  value={subscription.name}
                />

                <Input
                  label="Amount"
                  name="amount"
                  type="text"
                  value={subscription.amount.toString()}
                  onChange={handleChange}
                >
                  <ChooseCurrency
                    handleCurrencyChange={handleCurrencyChange}
                    value={subscription.currency}
                  />
                </Input>
                <CardSelect
                  value={subscription.accountId}
                  handleAccountChange={handleAccountChange}
                />
                <SubscriptionTypeSelect
                  value={subscription.subscriptionTypeId}
                  handleChange={handleTypeChange}
                />
                <TimePicker
                  handleDateTimeChange={handleDateFrom}
                  value={subscription.dateStarted}
                  heading="Starting date"
                  withTime={false}
                />
                {errors && (
                  <p className="mt-2 text-sm text-red-600 dark:text-red-500">
                    <span className="font-medium">Oh, snapp!</span> {errors}
                  </p>
                )}
                <div className="flex justify-between">
                  <CancelButton
                    onClick={() => {
                      props.handelOpening(false);
                    }}
                  />
                  <SubmitButton />
                </div>
              </form>
            </div>
          </>
        </ModalLayout>
      )}
      {props.children}
    </>
  );
};

export default SubscriptionModal;
