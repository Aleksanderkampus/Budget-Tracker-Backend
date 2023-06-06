import Search from "@/components/Table/Search";
import Popup from "../ChooseCategories/Popup/Popup";
import { ISimpleCurrency } from "@/DTO/ISimpleCurrency";
import { JwtContext } from "@/pages/_app";
import { useState, useContext, useEffect } from "react";
import { CurrencyService } from "@/services/CurrencyService";
import CurrencyItem from "./CurrencyItem";

interface IChooseCurrencyPopupProps {
  isOpen: boolean;
  handleOpening: (isOpen: boolean) => void;
  handleCurrencyChoice: (e: ISimpleCurrency) => void;
}

const ChooseCurrencyPopup = (props: IChooseCurrencyPopupProps) => {
  const [currencies, setCurrencires] = useState([] as ISimpleCurrency[]);
  const [filteredCurrenicies, setFilteredCurrencies] = useState(
    [] as ISimpleCurrency[]
  );
  const [search, setSearch] = useState("");
  const currencyService = new CurrencyService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);

  useEffect(() => {
    if (jwtResponse) {
      currencyService.getAll(jwtResponse.jwt).then((res) => {
        setCurrencires(res ?? []);
        setFilteredCurrencies(res ?? []);
      });
    }
  }, [jwtResponse]);

  const handleSearch = (target: EventTarget & HTMLInputElement) => {
    setFilteredCurrencies(
      currencies.filter(
        (c) =>
          c.name.toLowerCase().includes(target.value.toLowerCase()) ||
          c.abbreviation.toLowerCase().includes(target.value.toLowerCase())
      )
    );
    setSearch(target.value);
  };

  return (
    <Popup isOpen={props.isOpen} handleOpening={props.handleOpening}>
      <>
        <Search search={search} handleSearch={handleSearch} />
        <ul className="p-3 space-y-1 text-sm text-gray-700 dark:text-gray-200">
          {filteredCurrenicies.map((e, i) => {
            return (
              <CurrencyItem
                key={i}
                currency={e}
                handleCurrencyChange={props.handleCurrencyChoice}
              />
            );
          })}
        </ul>
      </>
    </Popup>
  );
};

export default ChooseCurrencyPopup;
