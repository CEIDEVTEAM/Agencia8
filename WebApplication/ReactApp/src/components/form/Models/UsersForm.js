import { Formik, Form, FormikHelpers } from "formik";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import FormSelect from '../form-groups/FormSelect'
import CustomButton from '../../../utils/generals/CustomButton'
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
                    .max(50, 'La longitud máxima es de 50 caracteres'),
                email: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres'),
                address: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres'),
                phone: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres'),
                idRole: Yup.string().required('Este campo es requerido')
                    .max(50, 'La longitud máxima es de 50 caracteres'),

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



                    <br />
                    <Button disabled={formikProps.isSubmitting}
                        type="submit">Ingresar</Button>

                </Form>
            )}

        </Formik>
    )
}

