import React, { useEffect, useState } from "react";
import FormText from '../../form-groups/FormText'
import FormSelect from '../../form-groups/FormSelect'
import FormDate from "../../form-groups/FormDate";



export default function DependentPersonalData(props) {
    const [options, setOptions] = useState([{ id: "M", name: "Masculino" }, { id: "F", name: "Femenino" }])
    const [optionsCond, setOptionsCond] = useState([{ id: "SubAgente", name: "SubAgente" }, { id: "Corredor", name: "Corredor" }])

    return (
        <div >
            <div className="grid md:grid-cols-2 md:gap-6">
                <FormText campo="name" label="Nombres" />
                <FormText campo="lastName" label="Apellidos" />
            </div>
            <FormText campo="personalAddress" label="Dirección Particular" />
            <div className="grid md:grid-cols-3 md:gap-6">
                <FormSelect disabled = {props.isEdit} options={optionsCond} campo="condition" label="Condición" />
                <FormText campo="personalDocument" label="Cédula" />
                <FormDate campo="birthDate" label="Fecha de Nacimiento" />
            </div>
            <div className="grid md:grid-cols-3 md:gap-6">
                <FormText campo="phone" label="Telefono" />
                <FormText campo="maritalStatus" label="Estado Civil" />
                <FormSelect options={options} campo="gender" label="Género" />
            </div>
        </div>
    )
}