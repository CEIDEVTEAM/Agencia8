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
                nombre: Yup.string().required('Este campo es requerido')
                .max(50, 'La longitud máxima es de 50 caracteres'),
                apellido: Yup.string().required('Este campo es requerido')
                .max(50, 'La longitud máxima es de 50 caracteres'),
                userName: Yup.string().required('Este campo es requerido')
                .max(50, 'La longitud máxima es de 50 caracteres')                      
            })}
        >
            {(formikProps) => (
                <Form>
                    <FormText campo="nombre" label="Nombre" />
                    <FormText campo="apellido" label="Apellido" />
                    <FormText campo="userName" label="Nombre de Usuario" />
                    
                    <br/>
                    {/* <Button className="blue" disabled={formikProps.isSubmitting} 
                        type="submit">Salvar</Button> */}
                        <Button disabled={formikProps.isSubmitting}
                                type="submit">Ingresar</Button>  
                        
                        <CustomButton className="red">Cancelar</CustomButton>  
                    
                </Form>
            )}

        </Formik>
    )
}

