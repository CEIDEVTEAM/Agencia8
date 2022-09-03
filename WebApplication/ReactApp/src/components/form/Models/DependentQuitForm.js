import { Formik, Form, FormikHelpers } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from 'yup'
import FormTextArea from '../form-groups/FormTextArea'
import { Button } from '@windmill/react-ui'


export default function DependentQuitForm(props) {
   

    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                description: Yup.string().required('Este campo es requerido')
                    .max(200, 'La longitud máxima es de 200 caracteres')                
            })}
        >
            {(formikProps) => (
                <Form>
                    <div className="grid md:grid-cols-2 md:gap-6">
                        <FormTextArea type={"textarea"} campo="description" label="Motivo" />                        
                    </div>
                    

                    <br />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">Dar Baja</Button>

                </Form>
            )}

        </Formik>
    )
}