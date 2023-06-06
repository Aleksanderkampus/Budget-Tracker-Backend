interface IProgressBarProps {
  total: number;
  amountSpent: number;
  currency: string;
}

const ProgressBar = (props: IProgressBarProps) => {
  const getPercentage = () => {
    const res = (props.amountSpent * -1) / props.total;
    return res > 1 ? 1 : res;
  };
  return (
    <>
      <div className="text-right text-sm text-white">
        <span>{props.total + props.currency}</span>
      </div>
      <div className="flex justify-between mb-1">
        <span className="text-xs font-medium  text-white">
          {props.amountSpent + props.currency} spent
        </span>
        <span className="text-xs font-medium  text-white">
          <span
            className={
              getPercentage() <= 0.5
                ? "text-secondary"
                : getPercentage() < 0.75
                ? "text-yellow-500"
                : "text-red-600"
            }
          >
            {props.total - props.amountSpent + props.currency + " "}
          </span>
          to spend
        </span>
      </div>
      <div className="w-full rounded-full h-2.5 bg-green-900">
        <div
          className="bg-secondary h-2.5 rounded-full"
          style={{ width: getPercentage() * 100 + "%" }}
        ></div>
      </div>
    </>
  );
};

export default ProgressBar;
