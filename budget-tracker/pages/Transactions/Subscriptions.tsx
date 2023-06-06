import PageLayout from "@/components/Layout/PageLayout";
import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../_app";
import { SubscriptionService } from "@/services/SubscriptionService";
import ISimpleSubscription from "@/DTO/Subscriptions/ISimpleSubscription";
import SubscriptionTable from "@/components/Table/SubscriptionTable/SubscriptionTable";
import { ISubscriptionRowProps } from "@/components/Table/SubscriptionTable/SubscriptionRow";
import SubscriptionModal from "@/components/Modal/SubscriptionModal";
import AddButton from "@/components/UI/Button/AddButton";

const Subscriptions = () => {
  const subscriptionService = new SubscriptionService();
  const { jwtResponse, setJwtResponse } = useContext(JwtContext);
  const [subscriptions, setSubscriptions] = useState(
    [] as ISimpleSubscription[]
  );
  const [modalOpen, setModalOpen] = useState(false);
  const [chosenId, setChosenId] = useState("");

  useEffect(() => {
    if (jwtResponse) {
      getSubscriptions();
    }
  }, [jwtResponse]);

  const handleDelete = (subscriptionId: string) => {
    if (jwtResponse) {
      subscriptionService
        .delete(jwtResponse.jwt, subscriptionId)
        .then((res) => {
          setSubscriptions(
            subscriptions.filter((s) => s.id !== subscriptionId)
          );
        });
    }
  };

  const updateTabel = () => {
    getSubscriptions();
  };

  const getSubscriptions = () => {
    subscriptionService
      .getAllSimpleSubscriptions(jwtResponse!.jwt)
      .then((res) => {
        setSubscriptions(res || []);
      });
  };

  return (
    <PageLayout heading="Subscriptions">
      <>
        <SubscriptionModal
          handelOpening={(bool: boolean) => {
            setModalOpen(bool);
          }}
          modalOpen={modalOpen}
          id={chosenId}
          updateTabel={updateTabel}
        >
          <AddButton
            onClick={() => {
              setChosenId("");
              setModalOpen(true);
            }}
          />
        </SubscriptionModal>

        <SubscriptionTable
          search={""}
          handleSearch={() => {}}
          handleEditOpening={(id?: string) => {
            setChosenId(id!);
            setModalOpen(true);
          }}
          handleDelete={handleDelete}
          headers={["Name", "Amount", "Type", "Started", "Next payment"]}
          data={subscriptions.map((e) => {
            return {
              id: e.id!,
              values: [
                e.name,
                e.amount,
                e.subscriptionType.name,
                new Date(e.dateStarted).toDateString(),
                new Date(e.nextPayment).toDateString(),
              ],
            } as ISubscriptionRowProps;
          })}
        />
      </>
    </PageLayout>
  );
};

export default Subscriptions;
