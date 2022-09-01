import { Formik, Form, FormikHelpers } from "formik";
import React, { useEffect, useState } from "react";
import FormText from '../../form-groups/FormText'


export default function DependentContactPerson(props) {
         
    return (
        <div >
            <div className="grid md:grid-cols-2 md:gap-6">
                <FormText campo="nameContactPerson" label="Nombres" />
                <FormText campo="lastNameContactPerson" label="Apellidos" /> 
                <FormText campo="phoneContactPerson" label="Teléfono" />
                <FormText campo="bond" label="Lazo" />
            </div>  
        </div>
    )
}