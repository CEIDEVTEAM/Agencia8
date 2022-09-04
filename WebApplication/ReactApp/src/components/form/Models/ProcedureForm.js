import { Formik, Form, FormikHelpers } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import FormSelect from "../form-groups/FormSelect";
import { Button, Card, CardBody } from '@windmill/react-ui'

export default function ParamsForm(props) {

    let options;
    if (props.options !== undefined) {
        options = props.options
    } else {
        options = [{ id: 1, name: "Barrio 1" }]
    }

    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                stepType: Yup.string().required('Este campo es requerido'),
                description: Yup.string().required('Este campo es requerido')
                    .max(200, 'La longitud máxima es de 200 caracteres'),

            })}
        >
            {(formikProps) => (
                <Form>
                    
                    <Card>
                        <CardBody>
                            <p className="mb-4 font-bold">DATOS</p>
                            <p className="mb-4 font-semibold">Titular: {props.info.name} {props.info.lastName} </p>
                            <p className="mb-4 font-semibold">C.I: {props.info.personalDocument}  </p>
                            <p className="mb-4 font-semibold">Dirección: {props.info.address}  </p>
                            <p className="mb-4 font-semibold">Barrio: {props.info.neighborhood}  </p>
                            <hr />
                            <p className="mb-4 font-bold">HISTORIAL</p>
                            {props.steps.map((step, i) => <><p key={i}> # {step.addRow} - {step.stepType} - {step.description} </p><hr /></>)}







                        </CardBody>
                    </Card>

                    <div >
                        <div className="grid md:grid-cols-2 md:gap-6">
                            <FormSelect options={options} campo="stepType" label="Nuevo Estado del Trámite" />
                            <FormText campo="description" label="Descripción" />
                        </div>
                    </div>
                    <br />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">Ingresar</Button>

                </Form>
            )}

        </Formik>
    )
}
