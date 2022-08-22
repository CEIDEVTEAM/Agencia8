import React, { useEffect, useState } from "react";
import axios from "axios";
import { useHistory } from "react-router-dom";
import { urlNewUser } from "../../utils/http/endpoints";
import ShowErrors from "../../utils/generals/ShowErrors";
import UsersForm from "../../components/form/Models/UsersForm";
import PageTitle from '../../components/Typography/PageTitle';
import ShowSuccess from "../../utils/generals/ShowSuccess";
import ToastyErrors from "../../utils/generals/ToastyErrors";
import { toast } from 'react-toastify';

function NewUser() {

    const history = useHistory();
    const [errors, setErrors] = useState([]);
    const [model, setModel] = useState({ name: '', userName: '', password: '', email: '', address: '', phone: '', idRole: '' });

    async function New(values) {
        try {
            console.log(values)

            const response = await axios.post(urlNewUser, values);
            console.log(response)
            setErrors(response.data.errors)
            if (response.data.successful) {
                return true;
            } else {
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
            <PageTitle>Registro de Usuarios</PageTitle>
            
            <UsersForm model={model}
                onSubmit={async (values, { resetForm }) => {
                    let response = await New(values);
                    if (response) {
                        toast.success("Guardado correctamente")
                        resetForm()
                    }
                    if (response === false){
                        errors.forEach(element => {                            
                            toast.error(element)
                        });
                    }

                }}
            />
        </>
    )
}
export default NewUser