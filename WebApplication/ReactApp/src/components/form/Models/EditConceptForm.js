import { Formik, Form } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import FormSelect from '../form-groups/FormSelect'
import { Button } from '@windmill/react-ui'
import SectionTitle from '../../../components/Typography/SectionTitle'

export default function EditConceptForm(props) {

    const [params, setParams] = useState(props.params)
    
    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                paramId: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres'),
                value: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 100 caracteres'),               

            })}
        >
            {(formikProps) => (
                <Form>
                    <hr />
                    <SectionTitle>Nombre: {props.model.name}</SectionTitle>                        
                    <div className="grid md:grid-cols-2 md:gap-6">                        
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

