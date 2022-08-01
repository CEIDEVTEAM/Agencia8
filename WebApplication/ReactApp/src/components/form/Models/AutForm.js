import React from 'react'
import { Formik, Form, FormikHelpers } from "formik";
import * as Yup from 'yup'
import ImageLight from '../../../assets/img/bolillero.jpg'
import ImageDark from '../../../assets/img/maxresdefault.jpg'
import { Button } from '@windmill/react-ui'
import FormText from '../form-groups/FormText'
import MostrarErrores from "../../../utils/generals/MostrarErrores";
import ShowFieldError from "../form-groups/ShowFieldError"

const AutForm = (props) => {
    return (       
        <main className="flex items-center justify-center p-6 sm:p-12 md:w-1/2">
            <div className="w-full">
                <h1 className="mb-4 text-xl font-semibold text-gray-700 dark:text-gray-200">Acceso al sistema</h1>
                <Formik initialValues={props.modelo}
                    onSubmit={props.onSubmit}

                    validationSchema={Yup.object({
                        user: Yup.string().required('Debe Ingresar Nombre de Usuario'),
                        password: Yup.string().required('De Ingresar su Contraseña')                                    
                    })}
                >
                    {(formikProps) => (
                        <Form>
                            <ShowFieldError mensaje={props.errores} />
                            <FormText campo="user" label="Usuario" />
                            <FormText campo="password" label="Contraseña" 
                                type="password" placeholder="***************"/>
                            
                            <br />
                            <Button block primary disabled={formikProps.isSubmitting}
                                type="submit">Ingresar</Button>                               
                        </Form>
                    )}

                </Formik>

                <hr className="my-8" />

            </div>
        </main>
       
    )
}

export default AutForm

