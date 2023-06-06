import { FormEvent } from "react";

export interface IIdentityProps {
  handleChange: (target: EventTarget & HTMLInputElement) => void;
  onSubmit: (target: FormEvent) => void;
}
