import { Formik, Form } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import { Button } from '@windmill/react-ui'
import FormMap from "../form-groups/FormMap";


export default function ExternalDependentForm(props) {
    const [isEditing, setIsEditing] = useState(false)
    useEffect(() => {
        if (props.isEdit)
            setIsEditing(true)
    }, [])

    function transformarCoordenada() {
        if (props.model.latitude === undefined || props.model.latitude === null || props.model.longitude === undefined || props.model.longitude === null) {
            return undefined;
        }
        const respuesta = {
            lat: props.model.latitude,
            lng: props.model.longitude
        }
        return [respuesta];


    }


    return (
        <Formik initialValues={props.model}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                name: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 30 caracteres'),
                address: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 50 caracteres'),
                latitude: Yup.number().required('Ingrese el punto en el mapa'),
                longitude: Yup.number().required('Ingrese el punto en el mapa')

            })}
        >
            {(formikProps) => (
                <Form>

                    <FormText campo="name" label="Nombre y Apellido" />
                    <FormText campo="address" label="Dirección" />
                    <br />
                    <div className="grid md:grid-cols-1 md:gap-6">
                        <FormMap coordenadas={transformarCoordenada()} campoLat="latitude" campoLng="longitude" />
                    </div>

                    <br />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">Ingresar</Button>

                </Form>
            )}

        </Formik>
    )
}

