import Helper from "@/helpers/Helper";
import Image from "next/image";
interface IIconWithNameProps {
  hexColor: string[];
  icon?: string;
  name: string;
  onClick?: () => void;
}

const IconWithName = (props: IIconWithNameProps) => {
  return (
    <div
      className="flex cursor-pointer items-center px-2 py-4 text-gray-900 whitespace-nowrap dark:text-white"
      onClick={props.onClick ? () => props.onClick!() : () => {}}
    >
      <div
        className="p-2 rounded"
        style={{
          backgroundImage: Helper.getLinearGradient(props.hexColor),
        }}
      >
        {props.icon ? (
          <Image
            width={20}
            height={20}
            src={Helper.convertBase64toURI(props.icon)}
            alt=""
          />
        ) : (
          <div className="w-5 h-5"></div>
        )}
      </div>
      <div className="pl-3">
        <div className="text-base font-semibold">{props.name}</div>
      </div>
    </div>
  );
};

export default IconWithName;
