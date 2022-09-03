import React,{ useEffect, useState } from "react";
import axios from "axios";
import {useHistory } from "react-router-dom";
import ShowErrors from "../../utils/generals/ShowErrors";
import Loading from "../generals/Loading"
import { toast } from 'react-toastify';



export default function Edit(props) {

    const id  = props.id;
    const [entity, setEntity] = useState();
    const [errors, setErrors] = useState([]);
    const [success, setSuccess] = useState();
    const history = useHistory();
    console.log(entity)

    useEffect(() => {
        axios.get(`${props.url}/${id}`)
            .then((response) => {
                setEntity(response.data);
            })        
    }, [])

    async function edit(entity) {
        try {            
            const response = await axios.put(`${props.url}/${id}`, entity);
            if(response.data.successful) {
                toast.success("Cambios realizados correctamente") 
                setErrors([])
            }else{
                setErrors(response.data.errors)
            }           
            console.log(response)          
                       
            //history.push(props.urlIndice);
        }
        catch (error) {
            setErrors(error.response.data)
        }
    }

    return (
        <>
            <ShowErrors errors={errors}/>            
            {entity ? props.children(entity, edit) : <Loading/>}
        </>

    )
}
