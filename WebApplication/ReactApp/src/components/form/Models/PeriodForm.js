import { Formik, Form } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import { Button } from '@windmill/react-ui'
import FormDate from "../form-groups/FormDate";

export default function PeriodForm(props) {
      
    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                description: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres'),
                referenceDate: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 100 caracteres'),               

            })}
        >
            {(formikProps) => (
                <Form>
                    <hr />
                    <div className="grid md:grid-cols-2 md:gap-6">                        
                        <FormText campo="description" label="Descripción" />
                        <FormDate campo="referenceDate" label="Fecha de Referencia" />                      
                    </div>
                    <br />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">Ingresar</Button>

                </Form>
            )}

        </Formik>
    )
}

