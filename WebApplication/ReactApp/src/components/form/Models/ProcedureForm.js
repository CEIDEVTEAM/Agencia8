import { Formik, Form, FormikHelpers } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import FormSelect from "../form-groups/FormSelect";
import { Button } from '@windmill/react-ui'

export default function ParamsForm(props) {

    const options = props.model.colStepTypes

    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                description: Yup.string().required('Este campo es requerido')
                    .max(200, 'La longitud máxima es de 200 caracteres'),
                value: Yup.string().required('Este campo es requerido')
            })}
        >
            {(formikProps) => (
                <Form>
                    <hr />

                    <div >
                        <div className="grid md:grid-cols-2 md:gap-6">
                            <FormSelect options={options} campo="stepType" label="Nuevo Estado del Trámite"/> 
                            <FormText campo="description" label="Descripción" />                            
                        </div>
                    </div>
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">Ingresar</Button>

                </Form>
            )}

        </Formik>
    )
}
