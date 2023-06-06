import { useEffect, useState } from "react";
import ChooseCurrencyPopup from "./ChooseCurrencyPopup";
import { ISimpleCurrency } from "@/DTO/ISimpleCurrency";

interface IChooseCurrencyProps {
  handleCurrencyChange: (c: ISimpleCurrency) => void;
  value?: ISimpleCurrency;
}

const ChooseCurrency = (props: IChooseCurrencyProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const [currentSymbol, setCurrenctSymbol] = useState(
    props.value?.symbol ? props.value.symbol : "€"
  );

  const handleCurrencyChoice = (c: ISimpleCurrency) => {
    setCurrenctSymbol(c.symbol);
    props.handleCurrencyChange(c);
    setIsOpen(false);
  };

  return (
    <>
      <button
        type="button"
        onClick={() => setIsOpen(true)}
        className=" z-40  absolute right-12 !mt-2 text-white  rounded-lg text-sm p-1.5 ml-auto inline-flex items-center hover:bg-gray-700 hover:text-white"
      >
        {currentSymbol}
      </button>
      <ChooseCurrencyPopup
        isOpen={isOpen}
        handleOpening={setIsOpen}
        handleCurrencyChoice={handleCurrencyChoice}
      />
    </>
  );
};

export default ChooseCurrency;
