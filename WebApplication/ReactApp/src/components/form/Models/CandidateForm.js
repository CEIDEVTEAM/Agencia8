import React, { useState } from 'react';
import * as Yup from 'yup';
import Wizard, { sleep } from '../form-groups/Wizard';
import FormText from '../form-groups/FormText'
import CandidatePersonalData from './CandidateSteps/CandidatePersonalData';
import CandidateShopData from './CandidateSteps/CandidateShopData';
import CandidateContactPerson from './CandidateSteps/CandidateContactPerson';


const WizardStep = ({ children }) => children;
const stepsNames = [
    "Información Personal",
    "Persona de Contacto",
    "Información del Comercio"
];

const CandidateForm = (props) => (
    <div>
        <Wizard
            initialValues={props.model}
            onSubmit={props.onSubmit}
            stepsNames={stepsNames}
        >
            <WizardStep
                onSubmit={() => console.log('Step1 onSubmit')}
                validationSchema={Yup.object({
                    name: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                    lastName: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                    personalAddress: Yup.string().required('Campo Requerido').max(100, 'La longitud máxima es de 30 caracteres'),
                    personalDocument: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                    birthDate: Yup.string().required('Campo Requerido'),
                    gender: Yup.string().required('Campo Requerido').max(15, 'La longitud máxima es de 30 caracteres'),
                    phone: Yup.string().required('Campo Requerido').max(15, 'La longitud máxima es de 30 caracteres'),
                    maritalStatus: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                })}
            >
                <br />
                <CandidatePersonalData />
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
            <WizardStep
                onSubmit={() => console.log('Step3 onSubmit')}
                validationSchema={Yup.object({
                    cName: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                    cPhone: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                    address: Yup.string().required('Campo Requerido').max(100, 'La longitud máxima es de 30 caracteres'),
                    neighborhood: Yup.string().required('Campo Requerido').max(30, 'La longitud máxima es de 30 caracteres'),
                    shopType: Yup.string().required('Campo Requerido'),
                    latitude: Yup.number().required('Ingrese el punto en el mapa'),
                    longitude: Yup.number().required()
                })}
            >
                <br />
                <CandidateShopData />
                <br />
            </WizardStep>
        </Wizard>
    </div>
);

export default CandidateForm;