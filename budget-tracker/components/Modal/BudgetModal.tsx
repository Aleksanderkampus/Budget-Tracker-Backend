import { ISimpleCurrency } from "@/DTO/ISimpleCurrency";
import CancelButton from "../UI/Button/CancelButton";
import SubmitButton from "../UI/Button/SubmitButton";
import Input from "../UI/Input/Input";
import CardSelect from "../UI/Select/CardSelect";
import TimePicker from "../UI/TimePicker/TimePicker";
import ChooseCategories, {
  IChosenCategory,
} from "./ChooseCategories/ChooseCategories";
import ChooseCurrency from "./ChooseCurrency/ChooseCurrency";
import ModalLayout from "./ModalLayout";
import { BudgetService } from "@/services/BudgetService";
import { JwtContext } from "@/pages/_app";
import { FormEvent, useContext, useEffect, useState } from "react";
import IBudgetToEdit from "@/DTO/Budgets/IBudgetToEdit";
import ISimpleAccount from "@/DTO/ISimpleAccount";
import { ISimpleCategory } from "@/DTO/Categories/ISimpleCategory";
import Account from "@/domain/Account";

interface IBudgetModalProps {
  id?: string;
  handelOpening: (bool: boolean) => void;
  children: JSX.Element;
  modalOpen: boolean;
  updateBudgets: () => void;
}

const emptyState = {
  name: "",
  amountToSave: 0,
  currencyId: "",
  currency: {} as ISimpleCurrency,
  account: [] as ISimpleAccount[],
  dateFrom: new Date().toISOString(),
  dateTo: new Date().toISOString(),
  simpleBudgetCategories: [] as ISimpleCategory[],
} as IBudgetToEdit;

const BudgetModal = (props: IBudgetModalProps) => {
  const budgetService = new BudgetService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const [budget, setBudget] = useState<IBudgetToEdit>(emptyState);
  const [errors, setErrors] = useState<string | undefined>(undefined);

  useEffect(() => {
    if (props.id === "") {
      console.log("yes");
      setBudget(emptyState);
    } else if (jwtResponse && props.id) {
      budgetService.getBudgetToEdit(jwtResponse.jwt, props.id).then((res) => {
        setBudget(res!);
      });
    }
  }, [jwtResponse, props.id]);

  useEffect(() => {
    setErrors(undefined);
  }, [budget]);

  const formValidated = (): boolean => {
    var toReturn = true;
    if (budget.name === "") {
      setErrors("Budget must have an name");
      toReturn = false;
    } else if (budget.simpleBudgetCategories.length === 0) {
      setErrors("Must choose an category");
      toReturn = false;
    } else if (budget.account.length === 0) {
      setErrors("Must choose an account");
      toReturn = false;
    } else if (budget.currencyId === "") {
      setErrors("Must have an currency");
      toReturn = false;
    } else if (budget.amountToSave === 0) {
      setErrors("Amount cannot be 0");
      toReturn = false;
    }

    return toReturn;
  };

  const handleChange = (target: EventTarget & HTMLInputElement) => {
    if (target.name === "amountToSave") {
      const pattern = /^\d+(\.\d+)?$/;
      if (!pattern.test(target.value) && target.value !== "") {
        setErrors("Amount must be a positive number");
        return;
      }
    }
    setBudget({ ...budget, [target.name]: target.value });
  };

  const handleCurrencyChange = (c: ISimpleCurrency) => {
    setBudget({ ...budget, ["currencyId"]: c.id!, ["currency"]: c });
  };

  const handleAccountChange = (account: Account) => {
    console.log(account);
    setBudget({
      ...budget,
      ["account"]: [
        {
          id: account.id,
          name: account.name,
          bank: account.bank,
        } as ISimpleAccount,
      ],
    });
  };

  const handleCategoryChange = (categories: IChosenCategory[]) => {
    const simpleBudgetCategories = categories.map((e) => {
      return {
        id: e.category.id,
        name: e.category.name,
        icon: e.category.icon,
        hexColor: e.category.hexColor,
      } as ISimpleCategory;
    });
    setBudget({
      ...budget,
      ["simpleBudgetCategories"]: simpleBudgetCategories,
    });
  };

  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault();
    if (!formValidated()) {
      return;
    }
    const copy = { ...budget };
    console.log(copy);
    copy.currency = undefined;
    var res;
    if (props.id === "") {
      res = await budgetService.add(jwtResponse!.jwt, copy);
    } else {
      res = await budgetService.put(jwtResponse!.jwt, copy);
    }
    if (res !== undefined) {
      props.updateBudgets();
      props.handelOpening(false);
    }
  };

  const handleDateFrom = (date?: Date, time?: string) => {
    const copyValues = { ...budget };
    const dateObj = new Date(budget.dateFrom);
    dateObj.setDate(date!.getDate());
    copyValues.dateFrom = dateObj.toISOString();
    setBudget({ ...copyValues });
  };

  const handleDateTo = (date?: Date, time?: string) => {
    const copyValues = { ...budget };
    const dateObj = new Date(budget.dateTo);
    dateObj.setDate(date!.getDate());
    copyValues.dateTo = dateObj.toISOString();
    setBudget({ ...copyValues });
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
                Add budget
              </h3>
              <form className="space-y-4" onSubmit={handleSubmit}>
                <Input
                  label="Name"
                  name="name"
                  type="text"
                  onChange={handleChange}
                  value={budget.name}
                />

                <Input
                  label="Amount"
                  name="amountToSave"
                  type="text"
                  value={budget.amountToSave.toString()}
                  onChange={handleChange}
                >
                  <ChooseCurrency
                    handleCurrencyChange={handleCurrencyChange}
                    value={budget.currency}
                  />
                </Input>
                <CardSelect
                  value={
                    budget.account.length > 0 ? budget.account[0].id : undefined
                  }
                  handleAccountChange={handleAccountChange}
                />
                <ChooseCategories
                  handleCategoryChange={handleCategoryChange}
                  withInputs={false}
                  amount={""}
                  value={budget.simpleBudgetCategories.map((e) => {
                    return { category: e } as IChosenCategory;
                  })}
                />
                <TimePicker
                  handleDateTimeChange={handleDateFrom}
                  value={budget.dateFrom}
                  heading="Starting date"
                  withTime={false}
                />
                <TimePicker
                  handleDateTimeChange={handleDateTo}
                  value={budget.dateTo}
                  heading="Ending date"
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

export default BudgetModal;
