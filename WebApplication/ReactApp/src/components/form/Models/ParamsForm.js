import { Formik, Form, FormikHelpers } from "formik";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import { Button } from '@windmill/react-ui'


export default function ParamsForm(props) {
    const [isEditing, setIsEditing] = useState(false)
    useEffect(() => {
        if (props.isEdit)
            setIsEditing(true)
    }, [])


    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                name: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 30 caracteres'),
                description: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 30 caracteres'),
                value: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres')

            })}
        >
            {(formikProps) => (
                <Form>
                    <div className="grid md:grid-cols-3 md:gap-6">
                        <FormText disabled={isEditing} campo="name" label="Nombre" />
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

