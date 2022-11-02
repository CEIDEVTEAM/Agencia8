import { Formik, Form } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import FormSelect from '../form-groups/FormSelect'
import { Button } from '@windmill/react-ui'
import axios from 'axios';
import { urlParamsOptions } from '../../../utils/http/endpoints'


export default function ConceptForm(props) {

    const [params, setParams] = useState()
    useEffect(() => {

        axios.get(urlParamsOptions)
            .then((response) => {
                setParams(response.data);

            })

    }, [])
    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                idParam: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres'),
                value: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 100 caracteres'),               

            })}
        >
            {(formikProps) => (
                <Form>
                    <hr />
                    <div className="grid md:grid-cols-2 md:gap-6">                        
                        <FormSelect options={params} campo="idParam" label="Parámetro" />                        
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

