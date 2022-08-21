import React from 'react';
import axios from "axios";
import { useState } from "react";
import { useHistory } from "react-router-dom";
import { urlNewUser } from "../utils/http/endpoints";
import ShowErrors from "../utils/generals/ShowErrors";
import NewUser from "../components/form/Models/UsersForm";


export default function Blank() {
     const history = useHistory();
     const [errors, setErrors] = useState([]);

    async function New(valors){       
        try{
            
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
            <ShowErrors errors={errors} />
            <NewUser model={{name:'',userName:'',password:'',email:'',address:'',phone:'',idRole:''}} 
                 onSubmit={async valors => {
                    await New(valors);
                 }}
            />
        </>
    )
}