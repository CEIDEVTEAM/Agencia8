import React, { useEffect, useState } from "react";
import PageTitle from '../../components/Typography/PageTitle'
import CandidateForm from "../../components/form/Models/CandidateForm";
import { toast } from 'react-toastify';
import ToastyErrors from "../../utils/generals/ToastyErrors";



export default function NewCandidate() {
    const [errors, setErrors] = useState([]);
    async function New(values) {

        console.log(values)


    }

    return (
        <>
            <PageTitle>Registro de Aspirante</PageTitle>
            <ToastyErrors errors={errors}/>
            <CandidateForm model={{ name: '',
                    lastName:'',
                    personalAddress:'',
                    personalDocument:'',
                    birthDate:'',
                    gender:'',
                    phone: '',
                    email: '',
                    maritalStatus:'',
                    cName:'',
                    cPhone:'',
                    address:'',
                    neighborhood:'',
                    shopType:'', 
                    cpName:'', 
                    cpLastName:'', 
                    cpPhone:'',
                    bond:''       
                    }}
                onSubmit={async (values, { resetForm }) => {
                    let response = await New(values);
                    toast.success("Guardado correctamente")
                    setErrors([])
                    resetForm()
                }} />
        </>
    )
}
