import { Formik, Form, FormikHelpers } from "formik";
import React from "react";
import { Link } from "react-router-dom";
import * as Yup from 'yup'
import FormText from '../form-groups/FormText'
import CustomButton from '../../../utils/generals/CustomButton'
import { Button } from '@windmill/react-ui'


export default function UserForm(props){
    return(
        <Formik initialValues={props.modelo}
            onSubmit={props.onSubmit}

            validationSchema={Yup.object({
                Name: Yup.string().required('Este campo es requerido')
                .max(30, 'La longitud máxima es de 30 caracteres'),                
                Password: Yup.string().required('Este campo es requerido')
                .max(50, 'La longitud máxima es de 50 caracteres'),
                Email: Yup.string().required('Este campo es requerido')
                .max(50, 'La longitud máxima es de 50 caracteres'),
                Address: Yup.string().required('Este campo es requerido')
                .max(50, 'La longitud máxima es de 50 caracteres'),
                Phone: Yup.string().required('Este campo es requerido')
                .max(50, 'La longitud máxima es de 50 caracteres'),
                IdRole: Yup.string().required('Este campo es requerido')
                .max(50, 'La longitud máxima es de 50 caracteres'),

            })}
        >
            {(formikProps) => (
                <Form>
                    <FormText campo="Name" label="Nombre" />
                    <FormText campo="Password" label="Contraseña" />
                    <FormText campo="Email" label="Correo Electrónico" />
                    <FormText campo="Address" label="Dirección" />
                    <FormText campo="Phone" label="Teléfono" />
                    <FormText campo="IdRole" label="Role" />
                    
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

