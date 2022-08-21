import React from 'react';
import axios from "axios";
import { useState } from "react";
import { useHistory } from "react-router-dom";
import { urlNewUser } from "utils/endpoints";
import ShowErrors from "../utils/generals/ShowErrors";
import FormularioGeneros from "./FormularioGeneros";


export default function NewUser() {
     const history = useHistory();
     const [errors, setErrors] = useState([]);

    async function New(){
        try{
            await axios.post(urlNewUser);            
        }
        catch (error){
            setErrors(error.response.data);
        }
    }

    return (
        <>
            <ShowErrors errores={errors} />
            <FormularioGeneros modelo={{nombre: ''}} 
                 onSubmit={async valores => {
                    await New(valores);
                 }}
            />
        </>
    )
}