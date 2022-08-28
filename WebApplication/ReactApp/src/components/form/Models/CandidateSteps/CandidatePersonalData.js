import React, { useEffect, useState } from "react";
import FormText from '../../form-groups/FormText'
import FormSelect from '../../form-groups/FormSelect'
import FormDate from "../../form-groups/FormDate";
import FormDatePicker from "../../form-groups/FormDatePicker";



export default function CandidatePersonalData(props) {
    const [options, setOptions] = useState([{ id: 1, name: "Masculino" }, { id: 2, name: "Femenino" }, { id: 3, name: "Otro" }])
    const [optionsCond, setOptionsCond] = useState([{ id: 1, name: "SubAgente" }, { id: 2, name: "Corredor" }])

    return (
        <div >
            <div className="grid md:grid-cols-2 md:gap-6">
                <FormText campo="name" label="Nombres" />
                <FormText campo="lastName" label="Apellidos" />
            </div>
            <FormText campo="personalAddress" label="Dirección Particular" />
            <div className="grid md:grid-cols-3 md:gap-6">
                <FormSelect options={optionsCond} campo="condition" label="Condición" />
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