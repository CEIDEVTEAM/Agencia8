import React, { useEffect, useState } from "react";
import axios from "axios";
import { useHistory } from "react-router-dom";
import { newPeriodUrl } from "../../utils/http/endpoints";
import PageTitle from '../../components/Typography/PageTitle';
import ToastyErrors from "../../utils/generals/ToastyErrors";
import { toast } from 'react-toastify';
import PeriodForm from "../../components/form/Models/PeriodForm";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'

function NewPeriod(props) {

    const history = useHistory();
    const [errors, setErrors] = useState([]);
    const [model, setModel] = useState({ description:'', referenceDate:''});
    const [params, setParamsList] = useState()

    async function New(values) {
        try {
            console.log(values)

            const response = await axios.post(newPeriodUrl, values);
            console.log(response)

            if (response.data.successful) {
                return true;
            } else {
                setErrors(response.data.errors)
                return false;
            }
        }
        catch (error) {
            setErrors(error.errors);
            return false;
        }
    }

    
    const handleClose = () => {
        props.onClose();
    }

    return (
       
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Comenzar Periodo</ModalHeader>
            <ModalBody >
            <ToastyErrors errors={errors} />
            <PeriodForm params={params} model={model}
                onSubmit={async (values, { resetForm }) => {
                    let response = await New(values);
                    if (response) {
                        toast.success("Guardado correctamente")
                        setErrors([])
                        resetForm()
                    }
                }}
            />
            </ModalBody>
            <ModalFooter>
                <Button className="w-full sm:w-autoblock" size="large" layout="outline" onClick={handleClose}>
                    Cerrar
                </Button>
            </ModalFooter>
        </Modal>
    )
}
export default NewPeriod