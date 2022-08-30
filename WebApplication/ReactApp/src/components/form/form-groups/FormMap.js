import React from 'react'
import { useFormikContext,ErrorMessage } from 'formik';
import ShowFieldError from "./ShowFieldError";
import Map from './Map'

export default function FormMap(props){

    const {values} = useFormikContext();

    function actualizarCampos(coordenadas){
        values[props.campoLat] = coordenadas.lat;
        values[props.campoLng] = coordenadas.lng;
    }

    return (
        <>
        <Map
            colection = {props.colection}
            coordenadas = {props.coordenadas}
            manejarClickMapa={actualizarCampos}
        />
        <ErrorMessage name={props.campoLat}>{mensaje =>                
            <ShowFieldError mensaje={mensaje} />
       }</ErrorMessage>
       </>
    )
}

FormMap.defaultProps = {
    coordenadas: []
}