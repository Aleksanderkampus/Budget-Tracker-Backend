import Table from "@/components/Table/Table";
import { FinancialCategoryService } from "@/services/FinancialCategoryService";
import { useState, useContext, useEffect } from "react";
import { JwtContext } from "../_app";
import PageLayout from "@/components/Layout/PageLayout";
import { ICategoryWithTransactionsAndBudget } from "@/DTO/Categories/ICategoryWithTransactionAndBudget";
import CategoryDetails from "@/components/Details/CategoryDetails/CategoryDetails";

const Categories = () => {
  const [categories, setCategories] = useState(
    [] as ICategoryWithTransactionsAndBudget[]
  );
  const [filteredCategories, setFilteredCategories] = useState(
    [] as ICategoryWithTransactionsAndBudget[]
  );
  const [search, setSearch] = useState("");
  const [detailsOpen, setDetailsOpen] = useState(false);
  const [choosenId, setChoosenId] = useState("");

  const categoryService = new FinancialCategoryService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);

  useEffect(() => {
    if (jwtResponse) {
      categoryService.getAll(jwtResponse.jwt).then((res) => {
        console.log(res);
        setCategories(res ?? []);
        setFilteredCategories(res ?? []);
      });
    }
  }, [jwtResponse]);

  const handleSearch = (target: EventTarget & HTMLInputElement) => {
    setFilteredCategories(
      categories.filter((e) =>
        e.name.toLocaleLowerCase().includes(target.value.toLocaleLowerCase())
      )
    );
    setSearch(target.value);
  };

  const handleDetailsOpening = (id?: string) => {
    if (id) {
      setChoosenId(id);
      setDetailsOpen(true);
    } else {
      setChoosenId("");
      setDetailsOpen(false);
    }
  };

  return (
    <PageLayout heading="Categories">
      <>
        <Table
          data={filteredCategories.map((e) => {
            return {
              icon: e.icon,
              hexValue: [e.hexColor],
              values: [
                e.name,
                e.numberOfTransactions.toString(),
                e.amountSpent.toString(),
                e.numberOfBudgets.toString(),
              ],
              id: e.id!,
            };
          })}
          headers={["Name", "Transactions", "Total spent", "Budgets"]}
          handleSearch={handleSearch}
          handleDetailsOpening={handleDetailsOpening}
          search={search}
        ></Table>
        <CategoryDetails
          detailsOpen={detailsOpen}
          id={choosenId}
          handleDetailsOpen={handleDetailsOpening}
        />
      </>
    </PageLayout>
  );
};

export default Categories;
