import { FormEvent, useContext, useEffect, useState } from "react";
import Input from "../UI/Input/Input";

import CardSelect from "../UI/Select/CardSelect";
import TimePicker from "../UI/TimePicker/TimePicker";
import RadioButton from "./RadioButton/RadioButton";
import ChooseCategories, {
  IChosenCategory,
} from "./ChooseCategories/ChooseCategories";
import SubmitButton from "../UI/Button/SubmitButton";
import CancelButton from "../UI/Button/CancelButton";

import { JwtContext } from "@/pages/_app";
import TransactionsService from "@/services/TransactionsService";
import ChooseCurrency from "./ChooseCurrency/ChooseCurrency";
import { ISimpleCurrency } from "@/DTO/ISimpleCurrency";
import { ITransactionDetailsDTO } from "@/DTO/Transactions/ITransactionDetailsDTO";
import { ITransactionCategoryDTO } from "@/DTO/Transactions/ITransactionCategoryDTO";
import ModalLayout from "./ModalLayout";
import Account from "@/domain/Account";

interface IModalProps {
  id?: string;
  modalOpen: boolean;
  handelOpening: (bool: boolean) => void;
  children: JSX.Element;
  updateTable: (td: ITransactionDetailsDTO) => void;
}

const emptyState = {
  senderReceiver: "",
  amount: "",
  currencyId: "c95992ec-a861-429b-bf61-7258e9c21a1d",
  currency: {} as ISimpleCurrency,
  accountId: "",
  time: new Date().toISOString(),
  categoryTransactions: [] as ITransactionCategoryDTO[],
} as ITransactionDetailsDTO;

const TransactionModal = (props: IModalProps) => {
  const transactionService = new TransactionsService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const [values, setValues] = useState(emptyState);
  const [errors, setErrors] = useState<string | undefined>(undefined);
  const [transactionType, setTransactionType] = useState("-");

  useEffect(() => {
    if (props.id === "") {
      setValues(emptyState);
    }
    if (props.id && jwtResponse) {
      transactionService.getDetails(jwtResponse.jwt, props.id).then((res) => {
        if (res) {
          setValues(res!);
          setTransactionType(parseFloat(res.amount) > 0 ? "+" : "-");
        }
      });
    }
  }, [jwtResponse, props.id]);

  useEffect(() => {
    setErrors(undefined);
  }, [values]);

  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault();
    if (!formValidated()) {
      return;
    }
    let copyValues = { ...values };
    copyValues = setTransactionAmounts(copyValues);
    copyValues.currency = undefined;
    let res;
    console.log(copyValues);
    if (props.id === "") {
      res = await transactionService.add(jwtResponse!.jwt, copyValues);
    } else {
      res = await transactionService.put(jwtResponse!.jwt, copyValues);
    }
    if (res !== undefined) {
      props.updateTable(values);
      props.handelOpening(false);
    }
  };

  const formValidated = (): boolean => {
    var toReturn = true;
    if (values.senderReceiver === "") {
      setErrors("Sender/receiver cannot be empty");
      toReturn = false;
    } else if (values.categoryTransactions.length === 0) {
      setErrors("Must choose an category");
      toReturn = false;
    } else if (values.accountId === "") {
      setErrors("Must choose an account");
      toReturn = false;
    }

    return toReturn;
  };

  const setTransactionAmounts = (copyValues: ITransactionDetailsDTO) => {
    const type = transactionType === "-" ? "-" : "";
    copyValues.amount = type + copyValues.amount;
    copyValues.categoryTransactions = [...copyValues.categoryTransactions].map(
      (e) => {
        return { ...e };
      }
    );
    for (const tc of copyValues.categoryTransactions) {
      tc.amount = type + tc.amount;
      tc.financialCategory = undefined;
    }
    return copyValues;
  };

  const handleChange = (target: EventTarget & HTMLInputElement) => {
    if (target.name === "amount") {
      const pattern = /^\d+(\.\d+)?$/;
      if (!pattern.test(target.value) && target.value !== "") {
        setErrors("Amount must be a positive number");
        return;
      }
    }
    setValues({ ...values, [target.name]: target.value });
  };

  const handleCurrencyChange = (c: ISimpleCurrency) => {
    setValues({ ...values, ["currencyId"]: c.id! });
  };

  const handleExpenseIncome = (target: EventTarget & HTMLInputElement) => {
    setTransactionType(target.value);
  };

  const handleDateTime = (date?: Date, time?: string) => {
    const copyValues = { ...values };
    const dateObj = new Date(values.time);
    if (date === undefined && time !== undefined) {
      const [hh, mm] = time.split(":").map(Number);
      dateObj.setHours(hh, mm);
    } else if (date !== undefined && time === undefined) {
      dateObj.setDate(date.getDate());
    }
    copyValues.time = dateObj.toISOString();
    setValues({ ...copyValues });
  };

  const handleCategoryChange = (categories: IChosenCategory[]) => {
    const categoryTransactions = categories.map((e) => {
      return {
        id: e.id,
        financialCategoryId: e.category.id,
        financialCategory: e.category,
        amount: e.amount,
      } as ITransactionCategoryDTO;
    });
    setValues({ ...values, ["categoryTransactions"]: categoryTransactions });
  };

  const handleAccountChange = (account: Account) => {
    setValues({ ...values, ["accountId"]: account.id! });
  };

  return (
    <>
      {props.modalOpen && (
        <ModalLayout
          onClick={() => {
            props.handelOpening(false);
          }}
        >
          <div className="px-6 py-6 lg:px-8">
            <h3 className="mb-4 text-xl font-medium  text-white">
              Add transaction
            </h3>
            <form className="space-y-4" onSubmit={handleSubmit}>
              <Input
                label="Sender/Receiver"
                name="senderReceiver"
                type="text"
                onChange={handleChange}
                value={values.senderReceiver}
              />

              <Input
                label="Amount"
                name="amount"
                type="text"
                value={values.amount}
                onChange={handleChange}
              >
                <ChooseCurrency
                  handleCurrencyChange={handleCurrencyChange}
                  value={values.currency}
                />
              </Input>
              <RadioButton
                value={transactionType}
                handleChange={handleExpenseIncome}
              />
              <CardSelect
                value={values.accountId}
                handleAccountChange={handleAccountChange}
              />
              <TimePicker
                handleDateTimeChange={handleDateTime}
                value={values.time}
                heading="Time"
                withTime={true}
              />
              <ChooseCategories
                handleCategoryChange={handleCategoryChange}
                withInputs={true}
                amount={values.amount}
                value={values.categoryTransactions.map((e) => {
                  return {
                    id: e.id,
                    category: e.financialCategory,
                    amount: e.amount,
                  } as IChosenCategory;
                })}
              />
              {errors && (
                <p className="mt-2 text-sm text-red-600 dark:text-red-500">
                  <span className="font-medium">Oh, snapp!</span> {errors}
                </p>
              )}
              <div className="flex justify-between">
                <CancelButton onClick={props.handelOpening} />
                <SubmitButton />
              </div>
            </form>
          </div>
        </ModalLayout>
      )}
      {props.children}
    </>
  );
};

export default TransactionModal;
