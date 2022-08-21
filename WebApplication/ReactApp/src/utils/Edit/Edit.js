import React,{ useEffect, useState } from "react";
import axios from "axios";
import { useParams, useHistory } from "react-router-dom";
import ShowErrors from "../../utils/generals/ShowErrors";
import ShowSuccess from "../../utils/generals/ShowSuccess";
import Loading from "../generals/Loading"


export default function Edit(props) {

    const id  = props.id;
    const [entity, setEntity] = useState();
    const [errors, setErrors] = useState([]);
    const [success, setSuccess] = useState();
    const history = useHistory();

    useEffect(() => {
        axios.get(`${props.url}/${id}`)
            .then((response) => {
                setEntity(response.data);
            })        
    }, [])

    async function edit(entity) {
        try {            
            const response = await axios.put(`${props.url}/${id}`, entity);
            if(response.data.successful)            
                setSuccess("Cambios realizados correctamente")            
            console.log(response)          
            //history.push(props.urlIndice);
        }
        catch (error) {
            setErrors(error.response.data)
        }
    }

    return (
        <>
            <ShowErrors errors={errors} />
            <ShowSuccess success={success} />
            {entity ? props.children(entity, edit) : <Loading/>}
        </>

    )
}
