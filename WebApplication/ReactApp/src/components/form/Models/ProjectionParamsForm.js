import { Formik, Form, FormikHelpers } from "formik";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import FormSelect from '../form-groups/FormSelect'
import { Label, Button } from '@windmill/react-ui'
import SectionTitle from '../../../components/Typography/SectionTitle'


export default function ProjectionParamsForm(props) {

    const [optionsType, setOptionsType] = useState([{ id: "Matemático", name: "Matemático" }, { id: "Conceptual", name: "Conceptual" }, { id: "General", name: "General" }])
    const [optionsUsage, setOptionsUsage] = useState([{ id: "General", name: "General" }, { id: "Quiniela", name: "Quiniela" }, { id: "Tombola", name: "Tombola" }
        , { id: "Oro", name: "Oro" }, { id: "Deportivo", name: "Deportivo" }, { id: "Pin", name: "Pin" }])
    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                name: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres'),
                description: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 100 caracteres'),
                actualDefaultValue: Yup.number().required('Este campo es requerido'),
                type: Yup.string().required('Este campo es requerido'),
                usage: Yup.string().required('Este campo es requerido'),

            })}
        >
            {(formikProps) => (
                <Form>
                    <hr />

                    <div className="grid md:grid-cols-2 md:gap-6">
                        <FormText campo="name" label="Nombre" />
                        <FormText campo="description" label="Descripción" />
                        <FormSelect options={optionsType} campo="type" label="Tipo" />
                        <FormSelect options={optionsUsage} campo="usage" label="Uso" />
                        <FormText campo="actualDefaultValue" label="Valor" />
                    </div>
                    <br />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">Ingresar</Button>

                </Form>
            )}

        </Formik>
    )
}

