import { Formik, Form, FormikHelpers } from "formik";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import { Label,Button } from '@windmill/react-ui'
import SectionTitle from '../../../components/Typography/SectionTitle'


export default function ParamsForm(props) {
    
    
    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                // name: Yup.string().required('Este campo es requerido')
                //     .max(50, 'La longitud máxima es de 50 caracteres'),
                description: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 100 caracteres'),
                value: Yup.number().required('Este campo es requerido')
                
            })}
        >
            {(formikProps) => (
                <Form>
                    <hr/>
                    <SectionTitle>Nombre: {props.model.name}</SectionTitle>                    
                    <div className="grid md:grid-cols-2 md:gap-6">
                        {/* <FormText disabled={isEditing} campo="name" label="Nombre" /> */}
                        <FormText campo="description" label="Descripción" />
                        <FormText campo="value" label="Valor" />
                    </div>
                    <br />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">Ingresar</Button>

                </Form>
            )}

        </Formik>
    )
}

