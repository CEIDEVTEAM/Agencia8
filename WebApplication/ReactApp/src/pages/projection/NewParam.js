import React, { useEffect, useState } from "react";
import axios from "axios";
import { useHistory } from "react-router-dom";
import { urlNewParam } from "../../utils/http/endpoints";
import ProjectionParamsForm from "../../components/form/Models/ProjectionParamsForm";
import PageTitle from '../../components/Typography/PageTitle';
import ToastyErrors from "../../utils/generals/ToastyErrors";
import { toast } from 'react-toastify';

function NewParam() {

    const history = useHistory();
    const [errors, setErrors] = useState([]);
    const [model, setModel] = useState({ name: '', description: '', type: '', usage: '', actualDefaultValue: ''});
        
    async function New(values) {
        try {
            console.log(values)

            const response = await axios.post(urlNewParam, values);
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
            <PageTitle>Crear Parámetro</PageTitle>
            <ToastyErrors errors={errors}/>
            <ProjectionParamsForm model={model}
                onSubmit={async (values, { resetForm }) => {
                    let response = await New(values);
                    if (response) {
                        toast.success("Guardado correctamente")
                        setErrors([])
                        resetForm()
                    }
                }}
            />
        </>
    )
}
export default NewParam