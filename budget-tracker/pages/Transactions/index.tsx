import PageLayout from "@/components/Layout/PageLayout";
import Table from "@/components/Table/Table";
import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../_app";
import TransactionsService from "@/services/TransactionsService";
import AddButton from "@/components/UI/Button/AddButton";
import TransactionModal from "@/components/Modal/TransactionModal";
import { ITransactionDetailsDTO } from "@/DTO/Transactions/ITransactionDetailsDTO";
import TransactionDetails from "@/components/Details/TransactionDetails/TransactionDetails";
import ITransactionWithCategories from "@/DTO/Transactions/ITransactionWithCategories";
import { ISimpleTransactionDTO } from "@/DTO/Transactions/ISimpleTransactionDTO";
import { ISimpleCategory } from "@/DTO/Categories/ISimpleCategory";

const Transactions = () => {
  const [transactions, setTransactions] = useState(
    [] as ITransactionWithCategories[]
  );
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const transactionsService = new TransactionsService();
  const [modalOpen, setModalOpen] = useState(false);
  const [detailsOpen, setDetailsOpen] = useState(false);
  const [choosenId, setChoosenId] = useState("");

  const openModal = (id?: string) => {
    if (id) {
      setChoosenId(id);
    } else {
      setChoosenId("");
    }
    setModalOpen(true);
  };

  const handleDetailsOpen = (id?: string) => {
    if (id) {
      setChoosenId(id);
    }

    setDetailsOpen(!detailsOpen);
  };

  useEffect(() => {
    if (jwtResponse) {
      transactionsService
        .getTransactionWithCategory(jwtResponse.jwt)
        .then((res) => {
          setTransactions(res ?? []);
        });
    }
  }, [jwtResponse]);

  const handleSearch = (target: EventTarget & HTMLInputElement) => {};

  const updateTable = (td: ITransactionDetailsDTO) => {
    const transaction = transactions.findIndex(
      (t) => t.transaction.id === td.id
    );
    const converted = convertDTO(td);
    if (transaction > -1) {
      const transactionCopy = [...transactions];
      transactionCopy.splice(transaction, 1, converted);
      setTransactions(transactionCopy);
    } else {
      setTransactions([...transactions, converted]);
    }
  };

  const convertDTO = (td: ITransactionDetailsDTO) => {
    return {
      transaction: {
        id: td.id,
        senderReceiver: td.senderReceiver,
        amount: td.amount,
        time: td.time,
        currencySymbol: td.currency!.symbol,
      } as ISimpleTransactionDTO,
      categories: td.categoryTransactions.map((e) => {
        return {
          id: e.financialCategory!.id,
          name: e.financialCategory!.name,
          hexColor: e.financialCategory!.hexColor,
          icon: e.financialCategory!.icon,
        } as ISimpleCategory;
      }),
    } as ITransactionWithCategories;
  };

  const handleDelete = (transactionId: string) => {
    if (jwtResponse) {
      transactionsService.delete(jwtResponse.jwt, transactionId).then((res) => {
        console.log(res);
        setTransactions(
          transactions.filter((t) => t.transaction.id !== transactionId)
        );
      });
    }
  };

  return (
    <PageLayout heading="Transactions">
      <>
        <TransactionModal
          modalOpen={modalOpen}
          id={choosenId}
          handelOpening={setModalOpen}
          updateTable={updateTable}
        >
          <AddButton onClick={openModal} />
        </TransactionModal>
        <TransactionDetails
          id={choosenId}
          detailsOpen={detailsOpen}
          handleDetailsOpen={handleDetailsOpen}
        />
        <Table
          data={transactions.map((e) => {
            return {
              id: e.transaction.id!,
              icon: e.categories.length > 1 ? undefined : e.categories[0].icon,
              hexValue: e.categories.map((e) => e.hexColor),
              values: [
                e.transaction.senderReceiver,
                e.categories.map((e) => e.name).join(", "),
                new Date(e.transaction.time).toDateString(),
                e.transaction.amount.toString(),
              ],
            };
          })}
          headers={["Sender/Receiver", "Category", "Time", "Amount"]}
          handleSearch={handleSearch}
          handleEditOpening={openModal}
          handleDetailsOpening={handleDetailsOpen}
          handleDelete={handleDelete}
          search=""
        />
      </>
    </PageLayout>
  );
};

export default Transactions;
