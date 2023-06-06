import PageLayout from "@/components/Layout/PageLayout";
import { BudgetService } from "@/services/BudgetService";
import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../_app";
import ISimpleBudgetDTO from "@/DTO/Budgets/SimpleBudgetDTO";
import {
  IBudgetRowValue,
  IBudgetRowProps,
} from "@/components/Table/BudgetTable/BudgetRow";
import BudgetTable from "@/components/Table/BudgetTable/BudgetTable";
import BudgetDetails from "@/components/Details/BudgetDetails/BudgetDetails";
import AddButton from "@/components/UI/Button/AddButton";
import BudgetModal from "@/components/Modal/BudgetModal";

const Budgets = () => {
  const budgetService = new BudgetService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const [budgets, setBudgets] = useState([] as ISimpleBudgetDTO[]);
  const [detailsOpen, setDetailsOpen] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [chosenId, setChosenId] = useState("");

  useEffect(() => {
    if (jwtResponse) {
      getBudgets();
    }
  }, [jwtResponse]);

  const getBudgetTableData = () => {
    return budgets.map((b) => {
      return {
        icon:
          b.simpleBudgetCategories.length > 1
            ? undefined
            : b.simpleBudgetCategories[0].icon,
        hexValue: b.simpleBudgetCategories.map((sbc) => sbc.hexColor),
        id: b.id,
        handleDetailsOpen: () => {},
        handleEditOpening: () => {},
        values: {
          name: b.name,
          category: b.simpleBudgetCategories.map((sbc) => sbc.name).join(", "),
          total: b.amountToSave,
          amountSpent: b.amountSpent,
          currencySymbol: b.currencySymbol,
          fromDate: new Date(b.dateFrom).toDateString(),
          toDate: new Date(b.dateTo).toDateString(),
        } as IBudgetRowValue,
      } as IBudgetRowProps;
    });
  };

  const getBudgets = () => {
    budgetService.getAllSimpleBudgets(jwtResponse!.jwt).then((res) => {
      setBudgets(res || []);
    });
  };

  const updateBudgets = () => {
    getBudgets();
  };

  const handleDelete = (budgetId: string) => {
    if (jwtResponse) {
      console.log(budgetId);
      console.log(budgets);
      budgetService.delete(jwtResponse.jwt, budgetId).then((res) => {
        setBudgets((prev) => prev.filter((b) => b.id !== budgetId));
      });
    }
  };

  return (
    <PageLayout heading="Budgets">
      <>
        {detailsOpen && (
          <BudgetDetails
            handleClose={() => {
              setChosenId("");
              setDetailsOpen(false);
            }}
            id={chosenId}
          />
        )}
        <BudgetModal
          id={chosenId}
          modalOpen={modalOpen}
          handelOpening={setModalOpen}
          updateBudgets={updateBudgets}
        >
          <AddButton
            onClick={() => {
              setChosenId("");
              setModalOpen(true);
              console.log(chosenId);
            }}
          />
        </BudgetModal>
        <BudgetTable
          data={getBudgetTableData()}
          headers={["Name", "Category", "Progress", "From", "To"]}
          handleSearch={() => {}}
          search={""}
          handleDelete={handleDelete}
          handleEditOpening={(id?: string) => {
            setChosenId(id!);
            setModalOpen(true);
          }}
          handleDetailsOpening={(id?: string) => {
            setChosenId(id!);
            setDetailsOpen(true);
          }}
        />
      </>
    </PageLayout>
  );
};

export default Budgets;
