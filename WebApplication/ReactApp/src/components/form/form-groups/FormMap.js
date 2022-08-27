import React from 'react'
import { useFormikContext } from 'formik';
import Map from './Map'

export default function FormMap(props){

    const {values} = useFormikContext();

    function actualizarCampos(coordenadas){
        values[props.campoLat] = coordenadas.lat;
        values[props.campoLng] = coordenadas.lng;
    }

    return (
        <Map
            coordenadas={props.coordenadas}
            manejarClickMapa={actualizarCampos}
        />
    )
}

FormMap.defaultProps = {
    coordenadas: []
}