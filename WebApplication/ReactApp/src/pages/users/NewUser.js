import React, { useEffect, useState } from "react";
import axios from "axios";
import { useHistory } from "react-router-dom";
import { urlNewUser } from "../../utils/http/endpoints";
import ShowErrors from "../../utils/generals/ShowErrors";
import UsersForm from "../../components/form/Models/UsersForm";
import PageTitle from '../../components/Typography/PageTitle';
import ShowSuccess from "../../utils/generals/ShowSuccess";


function NewUser() {

    const history = useHistory();
    const [errors, setErrors] = useState([]);
    const [success, setSuccess] = useState();
    const [model, setModel] = useState({ name: '', userName: '', password: '', email: '', address: '', phone: '', idRole: '' });

    async function New(values) {
        try {
            console.log(values)

            const response = await axios.post(urlNewUser, values);
            console.log(response)
            setErrors(response.data.errors)
            if (response.data.successful) {
                setSuccess("Guardado correctamente")
                return true;
            }else{
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
            <div className="grid md:grid-cols-2 md:gap-6">
                <ShowErrors errors={errors} />
                <ShowSuccess success={success} />

            </div>
            <UsersForm model={model}
                onSubmit={async (values, {resetForm}) => {
                    let response = await New(values); 
                    if (response)
                         resetForm()
                    
                }}
            />
        </>
    )
}
export default NewUser