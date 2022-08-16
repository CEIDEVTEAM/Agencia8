import { obtenerToken } from "./manejadorJWT";
import axios from "axios";

export function interceptorConfig(){
    axios.interceptors.request.use(
        function (config){
            const token = obtenerToken();
            if (token){
                config.headers.Authorization = `bearer ${token}`;
            }

            return config;
        },
        function (error){
            return Promise.reject(error);
        }
    )
}