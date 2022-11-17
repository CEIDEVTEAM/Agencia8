import { Formik, Form } from "formik";
import React, { useState } from "react";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import FormSelect from '../form-groups/FormSelect'
import { Button } from '@windmill/react-ui'
import SectionTitle from '../../../components/Typography/SectionTitle'



export default function RaspaditaForm(props) {

   
    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                agencia: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres'),
                apuestas: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 100 caracteres'),
                aciertos: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 100 caracteres'),

            })}
        >
            {(formikProps) => (
                <Form>
                    <hr />
                    <SectionTitle>Agencia: {props.model.agencia}</SectionTitle>
                    <div className="grid md:grid-cols-3 md:gap-6">
                        <FormText campo="apuestas" label="Apuestas" />
                        <FormText campo="aciertos" label="Aciertos" />
                    </div>
                    <br />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">Ingresar</Button>

                </Form>
            )}

        </Formik>
    )
}

