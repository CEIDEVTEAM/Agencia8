import { Formik, Form } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import { Button } from '@windmill/react-ui'


export default function ExternalDependentForm(props) {
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
                    .max(100, 'La longitud máxima es de 30 caracteres'),
                address: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 50 caracteres'),

            })}
        >
            {(formikProps) => (
                <Form>
                    
                        <FormText campo="name" label="Nombre y Apellido" />
                    
                    <FormText campo="address" label="Dirección" />
                    
                    <br />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">Ingresar</Button>

                </Form>
            )}

        </Formik>
    )
}

