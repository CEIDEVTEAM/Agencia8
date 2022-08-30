import React, { useEffect, useState } from "react";
import PageTitle from '../../components/Typography/PageTitle'
import CandidateForm from "../../components/form/Models/CandidateForm";
import { toast } from 'react-toastify';
import ToastyErrors from "../../utils/generals/ToastyErrors";
import {urlNewcandidate} from "../../utils/http/endpoints"
import axios from 'axios';


export default function NewCandidate() {
    const [errors, setErrors] = useState([]);
    async function New(values) {
        try {
            console.log(values)

            const response = await axios.post(urlNewcandidate, values);
            console.log(response)
            
            if (response.data.successful) {
                return true;
            }else{
                setErrors(response.data.errors )
                return false;
            } 
        }
        catch (error) {
            setErrors(error.errors);
            return false;
        }
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
                        condition: '',
                        maritalStatus:'',
                        cName:'',
                        cPhone:'',
                        address:'',
                        neighborhood:'',
                        shopType:'', 
                        cpName:'', 
                        cpLastName:'', 
                        cpPhone:'',
                        bond:'',
                             
                    }}
                   
                onSubmit={async (values, { resetForm }) => {
                    let response = await New(values);
                    if (response){
                        toast.success("Guardado correctamente")
                        setErrors([])
                        resetForm()
                    }
                }} />
        </>
    )
}
