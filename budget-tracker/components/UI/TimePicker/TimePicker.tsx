import { useEffect, useState } from "react";
import Datepicker from "tailwind-datepicker-react";

interface ITimePickerProps {
  handleDateTimeChange: (date?: Date, time?: string) => void;
  value: string;
  heading: string;
  withTime: boolean;
}

const options = {
  title: "Demo Title",
  autoHide: true,
  todayBtn: false,
  clearBtn: false,
  maxDate: new Date("2030-01-01"),
  minDate: new Date("1950-01-01"),
  theme: {
    background: "bg-gray-700 dark:bg-gray-800",
    todayBtn: "",
    clearBtn: "",
    icons: "",
    text: "",
    disabledText: "dark:text-gray-400",
    input:
      "dark:bg-background bg-background border-2 border-white dark:border-white rounded-none rounded-l-lg border-r-1",
    inputIcon: "dark:text-white",
    selected: "",
  },
  icons: {
    // () => ReactElement | JSX.Element
    prev: () => <span>-</span>,
    next: () => <span>-</span>,
  },
  datepickerClassNames: "top-12",
  defaultDate: new Date(),
  language: "en",
};

const TimePicker = (props: ITimePickerProps) => {
  const [optionState, setOptionState] = useState({
    ...options,
  });
  const [show, setShow] = useState<boolean>(false);
  const handleDateChange = (selectedDate: Date) => {
    console.log(selectedDate);
    props.handleDateTimeChange(selectedDate);
  };

  const handleTimeChange = (target: EventTarget & HTMLInputElement) => {
    props.handleDateTimeChange(undefined, target.value);
  };

  useEffect(() => {
    if (props.value !== "") {
      setOptionState({
        ...options,
        ["defaultDate"]: new Date(props.value),
      });
    }
  }, [props.value]);

  const handleClose = (state: boolean) => {
    setShow(state);
  };

  const getHours = (date: Date) => {
    return (
      date.getHours() +
      ":" +
      (date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes())
    );
  };

  return (
    <>
      <label
        htmlFor="countries"
        className="block  text-sm font-medium text-gray-900 dark:text-white"
      >
        {props.heading}
      </label>
      <div className="flex">
        <Datepicker
          options={optionState}
          onChange={handleDateChange}
          show={show}
          setShow={handleClose}
        />
        {props.withTime && (
          <input
            type="time"
            className="bg-background border-2 rounded-r-lg pt-2 [color-scheme:dark] pl-3 pr-3 w-3/5 text-white border-white"
            value={getHours(props.value ? new Date(props.value) : new Date())}
            onChange={(e) => handleTimeChange(e.target)}
          />
        )}
      </div>
    </>
  );
};

export default TimePicker;
