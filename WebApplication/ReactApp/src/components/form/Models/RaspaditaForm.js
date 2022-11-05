import { Formik, Form } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import FormSelect from '../form-groups/FormSelect'
import { Button } from '@windmill/react-ui'
import axios from 'axios';
import { urlParamsOptions } from '../../../utils/http/endpoints'


export default function RaspaditaForm(props) {

    const [options, setOptions] = useState([{ id: "1", name: "1" }, { id: "2", name: "2" }, { id: "3", name: "3" }, { id: "4", name: "4" }
    , { id: "5", name: "5" }, { id: "6", name: "6" }, { id: "7", name: "7" }, { id: "8", name: "8" }])

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
                    <div className="grid md:grid-cols-3 md:gap-6">
                        <FormSelect disabled={props.isEdit} options={options} campo="agencia" label="Agencia" />
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

