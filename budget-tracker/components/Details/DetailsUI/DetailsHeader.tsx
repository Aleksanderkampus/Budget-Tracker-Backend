import Helper from "@/helpers/Helper";
import Image from "next/image";
interface IDetailsHeaderProps {
  amount: string;
  hexValues: string[];
  icon?: string;
  name: string;
}

const DetailsHeader = (props: IDetailsHeaderProps) => {
  return (
    <>
      <div
        className="p-2 rounded flex flex-col items-center space-y-4 pt-10 pb-10 "
        style={{
          backgroundImage: Helper.getLinearGradient(props.hexValues),
        }}
      >
        {props.icon && (
          <Image
            width={40}
            height={40}
            src={Helper.convertBase64toURI(props.icon)}
            alt=""
          />
        )}
        <h1 className="text-white font-bold text-lg">{props.name}</h1>
        <h2 className="text-white font-semibold text-base">{props.amount}</h2>
      </div>
    </>
  );
};

export default DetailsHeader;
