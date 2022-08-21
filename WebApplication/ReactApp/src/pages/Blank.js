import React,{ useEffect, useState } from "react";
import axios from "axios";
import { useHistory } from "react-router-dom";
import { urlNewUser } from "../utils/http/endpoints";
import ShowErrors from "../utils/generals/ShowErrors";
import NewUser from "../components/form/Models/UsersForm";
import PageTitle from '../components/Typography/PageTitle'



export default function Blank() {
     const history = useHistory();
     const [errors, setErrors] = useState([]);    

    async function New(valors){       
        try{
          console.log(valors)
            
          const response = await axios.post(urlNewUser, valors);
          console.log(response)
            setErrors(response.data.errors)  
                    
        }
        catch (error){
            setErrors(error.errors);
        }
    }

    return (
        <>
            <PageTitle>Registro de Usuarios</PageTitle>
            <div className="grid md:grid-cols-2 md:gap-6">
            <ShowErrors errors={errors} />
            </div>
            <NewUser model={{name:'',userName:'',password:'',email:'',address:'',phone:'',idRole:''}} 
                 onSubmit={async valors => {
                    await New(valors);
                 }}
            />
        </>
    )
}