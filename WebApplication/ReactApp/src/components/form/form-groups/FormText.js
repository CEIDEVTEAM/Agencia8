import { Field, ErrorMessage } from "formik";
import React from "react";
import ShowFieldError from "./ShowFieldError";
import { Input, HelperText, Label, Select, Textarea } from '@windmill/react-ui'

export default function FormText(props) {
    return (
        <Label className="mt-4">
          {props.label ? <label htmlFor={props.campo}>{props.label}</label> : null}
          <Field name={props.campo} className="bg-white-200 appearance-none border-2 border-gray-200 rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:bg-white focus:border-purple-500" 
            placeholder={props.placeholder} />
           <ErrorMessage name={props.campo}>{mensaje =>
                 <ShowFieldError mensaje={mensaje} />
            }</ErrorMessage>
          
        </Label>
    
    )
}

