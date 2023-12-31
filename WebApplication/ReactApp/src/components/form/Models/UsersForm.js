import { Formik, Form } from "formik";
import React, { useEffect, useState } from "react";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import FormSelect from '../form-groups/FormSelect'

import { Button } from '@windmill/react-ui'


export default function UserForm(props) {
    const [options, setOptions] = useState([{ id: 1, name: "Administrador" }, { id: 2, name: "Funcionario" }, { id: 3, name: "Gerente" }])
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
                    .max(30, 'La longitud máxima es de 30 caracteres'),
                userName: Yup.string().required('Este campo es requerido')
                    .max(30, 'La longitud máxima es de 30 caracteres'),
                password: Yup.string().required('Este campo es requerido')
                    .max(200, 'La longitud máxima es de 200 caracteres'),
                email: Yup.string().email('email inválido').required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres'),
                address: Yup.string().required('Este campo es requerido')
                    .max(100, 'La longitud máxima es de 100 caracteres'),
                phone: Yup.string().required('Este campo es requerido')
                    .max(15, 'La longitud máxima es de 15 caracteres'),
                idRole: Yup.string().required('Este campo es requerido'),

            })}
        >
            {(formikProps) => (
                <Form>
                    <div className="grid md:grid-cols-2 md:gap-6">
                        <FormText campo="name" label="Nombre" />
                        <FormText campo="phone" label="Teléfono" />
                    </div>
                    <FormText campo="address" label="Dirección" />
                    <FormText campo="email" label="Correo Electrónico" />
                    <div className="grid md:grid-cols-2 md:gap-6">
                        <FormText disabled={isEditing} campo="userName" label="Nombre de Usuario" />
                        <FormSelect options={options} campo="idRole" label="Rol" />
                    </div>
                    <div className="grid md:grid-cols-2 md:gap-6">
                        <FormText campo="password" label="Contraseña" />
                    </div>

                    <div className="flex mt-2 sm:mt-auto sm:justify-end">

                        <Button  disabled={formikProps.isSubmitting}
                        type="submit">GUARDAR</Button>
                    </div>

                    <br />
                   

                </Form>
            )}

        </Formik>
    )
}

