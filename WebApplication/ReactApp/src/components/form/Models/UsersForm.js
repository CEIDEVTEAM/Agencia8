import { Formik, Form, FormikHelpers } from "formik";
import React from "react";
import { Link } from "react-router-dom";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import CustomButton from '../../../utils/generals/CustomButton'
import { Button } from '@windmill/react-ui'


export default function UserForm(props){
    return(
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
                    <FormText campo="name" label="Nombre" />
                    <FormText campo="userName" label="Nombre de Usuario" />
                    <FormText campo="password" label="Contraseña" />
                    <FormText campo="email" label="Correo Electrónico" />
                    <FormText campo="address" label="Dirección" />
                    <FormText campo="phone" label="Teléfono" />
                    <FormText campo="idRole" label="Role" />
                    
                    <br/>
                    {/* <Button className="blue" disabled={formikProps.isSubmitting} 
                        type="submit">Salvar</Button> */}
                        <Button disabled={formikProps.isSubmitting}
                                type="submit">Ingresar</Button>  
                        
                        {/* <CustomButton className="red">Cancelar</CustomButton>   */}
                    
                </Form>
            )}

        </Formik>
    )
}

