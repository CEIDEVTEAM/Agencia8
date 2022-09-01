import React, { useEffect, useState } from 'react';
import * as Yup from 'yup';
import Wizard from '../form-groups/Wizard';
import DependentPersonalData from './DependentSteps/DependentPersonalData';
import DependentShopData from './DependentSteps/DependentShopData';
import DependentContactPerson from './DependentSteps/DependentContactPerson';
import NoShopDataRequired from './CandidateSteps/NoShopDataRequired';



const WizardStep = ({ children }) => children;
const stepsNames = [
    "Información Personal",
    "Persona de Contacto",
    "Información del Comercio"
];


export default function DependentForm(props) {
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
                        console.log("stado" + conditionState)
                    }}
                    validationSchema={Yup.object({
                        name: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        lastName: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        personalAddress: Yup.string().required('Campo Requerido').max(100, 'La longitud máxima es de 100 caracteres'),
                        personalDocument: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        birthDate: Yup.string().required('Campo Requerido'),
                        gender: Yup.string().required('Campo Requerido').max(15, 'La longitud máxima es de 12 caracteres'),
                        phone: Yup.string().required('Campo Requerido').max(15, 'La longitud máxima es de 12 caracteres'),
                        maritalStatus: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        condition: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                    })}
                >
                    <br />
                    <DependentPersonalData isEdit={props.isEdit}/>
                    <br />
                </WizardStep>
                <WizardStep
                    onSubmit={() => console.log('Step2 onSubmit')}
                    validationSchema={Yup.object({
                        nameContactPerson: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        lastNameContactPerson: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        phoneContactPerson: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                        bond: Yup.string().required('Campo Requerido').max(100, 'La longitud máxima es de 30 caracteres'),

                    })}
                >
                    <br />
                    <DependentContactPerson />
                    <br />
                </WizardStep>
                {conditionState === "SubAgente" ?
                    <WizardStep
                        onSubmit={() => console.log('Step3 onSubmit')}
                        validationSchema={Yup.object({
                            nameShopData: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                            phoneShopData: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                            address: Yup.string().required('Campo Requerido').max(100, 'La longitud máxima es de 30 caracteres'),
                            neighborhood: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                            shopType: Yup.string().required('Campo Requerido'),
                            latitude: Yup.number().required('Ingrese el punto en el mapa'),
                            longitude: Yup.number().required('Ingrese el punto en el mapa')
                        })}
                    >
                        <br />
                        <DependentShopData props={props.model}/>
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