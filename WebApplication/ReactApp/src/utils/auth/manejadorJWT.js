import { authUrl } from "../../utils/http/endpoints";
import axios from "axios";

const llaveToken = "token";
const llaveExpiracion = "expiration"

export function guardarTokenLocalStorage(autenticacion){
    localStorage.setItem(llaveToken, autenticacion.token);
    localStorage.setItem(llaveExpiracion, autenticacion.expiration.toString());
}

export function obtenerClaims(){
    const token = localStorage.getItem(llaveToken);

    if (!token){
        return [];
    }

    const expiracion = localStorage.getItem(llaveExpiracion);
    const expiracionFecha = new Date(expiracion);
    
    if (expiracionFecha <= new Date()){
        logout();
        return [];
    }

    const dataToken = JSON.parse(atob(token.split(".")[1]));
    const respuesta = [];
    for (const propiedad in dataToken){
        respuesta.push({nombre: propiedad, valor: dataToken[propiedad]});
    }

    return respuesta;
}

export async function logout(){
    const token = {token: localStorage.getItem(llaveToken)}

    localStorage.removeItem(llaveToken);
    localStorage.removeItem(llaveExpiracion);

    try {
        const respuesta = 
        await axios.post(`${authUrl}/SetLogout`, token);
            //history.push("/app");
        }
    catch (error) {

    }
}

export function obtenerToken(){
    return localStorage.getItem(llaveToken);
}