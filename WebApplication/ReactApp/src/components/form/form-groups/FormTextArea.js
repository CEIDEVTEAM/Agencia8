import { Field, ErrorMessage } from "formik";
import React from "react";
import ShowFieldError from "./ShowFieldError";
import {Label} from '@windmill/react-ui'


export default function FormText(props) {
    return (
        <Label className="mt-4">
          {props.label ? <label htmlFor={props.campo}>{props.label}</label> : null}
          <Field as="textarea"name={props.campo} className={props.className}
            placeholder={props.placeholder} type={props.type} disabled={props.disabled}/>
           <ErrorMessage name={props.campo}>{mensaje =>                
                 <ShowFieldError mensaje={mensaje} />
            }</ErrorMessage>
          
        </Label>
    
    )
}

FormText.defaultProps = {
    className :"bg-white-200 appearance-none border-2 border-gray-200 rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:bg-white focus:border-purple-500"     
}
