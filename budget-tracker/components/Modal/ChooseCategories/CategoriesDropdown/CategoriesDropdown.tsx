import { useContext, useEffect, useState } from "react";
import { IChosenCategory } from "../ChooseCategories";
import { JwtContext } from "@/pages/_app";
import { FinancialCategoryService } from "@/services/FinancialCategoryService";
import DropdownItem from "./DropdownItem";
import { ISimpleCategory } from "@/DTO/Categories/ISimpleCategory";
import Popup from "../Popup/Popup";
import Search from "@/components/Table/Search";

interface ICategoriesDropdownProps {
  isOpen: boolean;
  handleOpening: (isOpen: boolean) => void;
  handleCategoriesChoice: (e: IChosenCategory) => void;
}

const CategoriesDropdown = (props: ICategoriesDropdownProps) => {
  const [categories, setCategories] = useState([] as ISimpleCategory[]);
  const [filteredCategories, setFilteredCategories] = useState(
    [] as ISimpleCategory[]
  );
  const [search, setSearch] = useState("");
  const categoryService = new FinancialCategoryService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);

  useEffect(() => {
    if (jwtResponse) {
      categoryService.getAllSimpleCategories(jwtResponse.jwt).then((res) => {
        setCategories(res ?? []);
        setFilteredCategories(res ?? []);
      });
    }
  }, [jwtResponse]);

  const handleSearch = (target: EventTarget & HTMLInputElement) => {
    setFilteredCategories(
      categories.filter((c) =>
        c.name.toLowerCase().includes(target.value.toLowerCase())
      )
    );
    setSearch(target.value);
  };

  return (
    <Popup isOpen={props.isOpen} handleOpening={props.handleOpening}>
      <>
        <Search search={search} handleSearch={handleSearch} />
        <ul className="p-3 space-y-1 text-sm text-gray-700 dark:text-gray-200">
          {filteredCategories.map((e, i) => {
            return (
              <DropdownItem
                key={i}
                category={
                  {
                    name: e.name,
                    icon: e.icon,
                    hexColor: e.hexColor,
                    id: e.id,
                  } as ISimpleCategory
                }
                handleCategoryChange={props.handleCategoriesChoice}
              />
            );
          })}
        </ul>
      </>
    </Popup>
  );
};

export default CategoriesDropdown;
