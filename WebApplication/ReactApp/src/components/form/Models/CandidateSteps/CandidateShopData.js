import React, { useEffect, useState } from "react";
import FormText from '../../form-groups/FormText'
import FormSelect from '../../form-groups/FormSelect'
import FormDate from "../../form-groups/FormDate";



export default function TestForm1(props) {     

    const [options, setOptions] = useState([{ id: 1, name: "Barrio 1" }, { id: 2, name: "Barrio2" }, { id: 3, name: "Barrio 3" }])

    return (
        <div >
            <FormText campo="address" label="Dirección del comercio" />
            <div className="grid md:grid-cols-2 md:gap-6">
                <FormText campo="cName" label="Nombre del Comercio" />
                <FormSelect options={options} campo="neighborhood" label="Barrio" />
                <FormText campo="shopType" label="Tipo de Comercio"/>
                <FormText campo="cPhone" label="Telefono de comercio" />
            </div>          
            
        </div>
    )
}