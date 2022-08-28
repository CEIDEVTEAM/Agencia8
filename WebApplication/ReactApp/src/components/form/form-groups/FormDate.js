import React from "react";
import ShowFieldError from "./ShowFieldError";
import {Label} from '@windmill/react-ui'
import { useFormikContext } from "formik";
import { ErrorMessage } from "formik";


export default function FormDate(props) {
    const { values, validateForm, touched, errors } = useFormikContext();
    console.log('values', values);
    console.log('props.campo', values[props.campo]);
    const date = new Date(values[props.campo])
    return (
        
            <Label className="mt-4" htmlFor={props.campo}>{props.label}
            <input type="date" className="bg-white-200 appearance-none border-2 border-gray-200 rounded w-full py-2 px-4 text-gray-700 leading-tight focus:outline-none focus:bg-white focus:border-purple-500"
                id={props.campo}
                name={props.campo}
                defaultValue={date.toLocaleDateString('en-CA')}
                onChange={e => {
                    const fecha = new Date(e.currentTarget.value + 'T00:00:00');
                    values[props.campo] = fecha;
                    validateForm();
                }}
            />
            <ErrorMessage name={props.campo}>{mensaje =>
                 <ShowFieldError mensaje={mensaje} />}</ErrorMessage>
           </Label>
    )
}


