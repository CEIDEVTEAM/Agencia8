import { Field, ErrorMessage } from "formik";
import React from "react";
import ShowFieldError from "./ShowFieldError";
import { Label } from '@windmill/react-ui'

const FormSelect = (props) => {
  return (
    <Label className="mt-4">
      {props.label ? <label htmlFor={props.campo}>{props.label}</label> : null}
      <Field as="select" name={props.campo} className={props.className}
        disabled={props.disabled} placeholder={props.placeholder} type={props.type}>
        <option value="0">--Seleccione--</option>
        {props.options ? props.options.map(op => <option key={op.id}
          value={op.id}>{op.name}</option>) : null}
      </Field>
      <ErrorMessage name={props.campo}>{mensaje =>
        <ShowFieldError mensaje={mensaje} />}</ErrorMessage>

    </Label>
  )
}
FormSelect.defaultProps = {
  className: "bg-white-200 border-2 border-gray-200 rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:bg-white focus:border-purple-500"
}
export default FormSelect