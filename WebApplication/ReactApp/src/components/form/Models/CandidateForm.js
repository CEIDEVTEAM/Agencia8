import React, { useEffect, useState } from 'react';
import * as Yup from 'yup';
import Wizard from '../form-groups/Wizard';
import CandidatePersonalData from './CandidateSteps/CandidatePersonalData';
import CandidateShopData from './CandidateSteps/CandidateShopData';
import CandidateContactPerson from './CandidateSteps/CandidateContactPerson';
import NoShopDataRequired from './CandidateSteps/NoShopDataRequired';



const WizardStep = ({ children }) => children;
const stepsNames = [
    "Información Personal",
    "Persona de Contacto",
    "Información del Comercio"
];


export default function CandidateForm(props) {
    const [conditionState, setconditionState] = useState();
    useEffect(() => {
        setconditionState("SubAgente");

    }, [])

    return (
        <div>
            <Wizard
                initialValues={props.model}
                onSubmit={props.onSubmit}
                stepsNames={stepsNames}
            >
                <WizardStep
                    onSubmit={(values) => {
                        console.log('Step1 onSubmit')
                        setconditionState(values["condition"])                        
                    }}
                    validationSchema={Yup.object({
                        name: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        lastName: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        personalAddress: Yup.string().required('Campo Requerido').max(100, 'La longitud máxima es de 30 caracteres'),
                        personalDocument: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        birthDate: Yup.string().required('Campo Requerido'),
                        gender: Yup.string().required('Campo Requerido').max(15, 'La longitud máxima es de 30 caracteres'),
                        phone: Yup.string().required('Campo Requerido').max(15, 'La longitud máxima es de 30 caracteres'),
                        maritalStatus: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        condition: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                    })}
                >
                    <br />
                    <CandidatePersonalData isEdit={props.isEdit}/>
                    <br />
                </WizardStep>
                <WizardStep
                    onSubmit={() => console.log('Step2 onSubmit')}
                    validationSchema={Yup.object({
                        cpName: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        cpLastName: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        cpPhone: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        bond: Yup.string().required('Campo Requerido').max(100, 'La longitud máxima es de 30 caracteres'),

                    })}
                >
                    <br />
                    <CandidateContactPerson />
                    <br />
                </WizardStep>
                {conditionState === "SubAgente" ?
                    <WizardStep
                        onSubmit={() => console.log('Step3 onSubmit')}
                        validationSchema={Yup.object({
                            cName: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                            cPhone: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                            address: Yup.string().required('Campo Requerido').max(100, 'La longitud máxima es de 30 caracteres'),
                            neighborhood: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                            shopType: Yup.string().required('Campo Requerido'),
                            latitude: Yup.number().required('Ingrese el punto en el mapa'),
                            longitude: Yup.number().required('Ingrese el punto en el mapa')
                        })}
                    >
                        <br />
                        <CandidateShopData props={props.model}/>
                        <br />
                    </WizardStep> : <WizardStep
                        onSubmit={() => console.log('Step3 onSubmit')}
                    >
                        <br />
                        <NoShopDataRequired></NoShopDataRequired>
                        <br />
                    </WizardStep>
                }
            </Wizard>
        </div>
    )
}

