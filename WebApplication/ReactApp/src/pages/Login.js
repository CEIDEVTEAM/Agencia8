import React ,{ useContext, useState }from 'react'
import { Link } from 'react-router-dom'

//import ImageLight from '../assets/img/login-office.jpeg'
import ImageLight from '../assets/img/bolillero.jpg'
import ImageDark from '../assets/img/maxresdefault.jpg'
//import ImageDark from '../assets/img/login-office-dark.jpeg'
import { GithubIcon, TwitterIcon } from '../icons'
import { Label, Input, Button } from '@windmill/react-ui'
import AutForm from '../components/form/Models/AutForm'

import axios from "axios";
import { useHistory } from "react-router-dom";
import { authUrl } from "../utils/http/endpoints";
import AuthContext from "../context/AuthContext";

import { guardarTokenLocalStorage, obtenerClaims } from "../utils/auth/manejadorJWT";;

function Login() {
  const {actualizar} = useContext(AuthContext);
  const [errores, setErrores] = useState([]);
  const history = useHistory();

  async function login(credenciales) {
    try {
        const respuesta = await
            axios.post(`${authUrl}/validateCredentials`, credenciales);
        
            guardarTokenLocalStorage(respuesta.data);
            actualizar(obtenerClaims());
            history.push("/app");
        console.log(respuesta);
    }
    catch (error) {
        setErrores(error.response.data);
    }
}
  
  return (
    <div className="flex items-center min-h-screen p-6 bg-gray-50 dark:bg-gray-900">
      <div className="flex-1 h-full max-w-4xl mx-auto overflow-hidden bg-white rounded-lg shadow-xl dark:bg-gray-800">
        <div className="flex flex-col overflow-y-auto md:flex-row">
          <div className="h-32 md:h-auto md:w-1/2">
            <img
              aria-hidden="true"
              className="object-cover w-full h-full dark:hidden"
              src={ImageLight}
              alt="Office"
            />
            <img
              aria-hidden="true"
              className="hidden object-cover w-full h-full dark:block"
              src={ImageDark}
              alt="Office"
            />
          </div>                 
          <AutForm errores={errores} modelo={{user:'', password:''}} 
                    onSubmit={async valores => await login(valores)}
          />
        </div>
      </div>
    </div>
  )
}

export default Login
