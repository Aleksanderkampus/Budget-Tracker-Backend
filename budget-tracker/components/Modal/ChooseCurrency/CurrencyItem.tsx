import { ISimpleCurrency } from "@/DTO/ISimpleCurrency";

interface ICurrencyItemProps {
  currency: ISimpleCurrency;
  handleCurrencyChange: (c: ISimpleCurrency) => void;
}

const CurrencyItem = (props: ICurrencyItemProps) => {
  return (
    <li
      onClick={() => props.handleCurrencyChange(props.currency)}
      className="flex justify-between cursor-pointer items-center"
    >
      <div className="ml-2 text-sm">
        <div className="font-medium text-gray-900 dark:text-gray-300">
          {props.currency.name}
        </div>
        <p
          id="helper-radio-text"
          className="text-xs font-normal text-gray-500 dark:text-gray-300"
        >
          {props.currency.abbreviation}
        </p>
      </div>
      <div className="text-right">{props.currency.symbol}</div>
    </li>
  );
};

export default CurrencyItem;
