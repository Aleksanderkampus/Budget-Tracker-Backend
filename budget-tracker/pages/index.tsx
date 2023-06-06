import DashBoardGraph from "@/components/DashBoard/DashBoardGraph/DashBoardGraph";
import DashboardPies from "@/components/DashBoard/DashboardPie/DashboardPies";
import PageLayout from "@/components/Layout/PageLayout";

export default function Home() {
  return (
    <PageLayout heading="Transactions">
      <div className="flex">
        <DashboardPies />
        <DashBoardGraph />
      </div>
    </PageLayout>
  );
}
