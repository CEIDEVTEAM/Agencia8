import React, { useEffect, useState } from "react";
import { urlDeleteDependent } from "../../utils/http/endpoints";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'
import ToastyErrors from "../../utils/generals/ToastyErrors";
import { toast } from 'react-toastify';
import axios from 'axios';
import DependentQuitForm from "../../components/form/Models/DependentQuitForm";

function DeleteDependent(props) {

    const id  = props.id;
    const handleClose = () => {
        props.onClose();
    }
    const [errors, setErrors] = useState([]);

    async function New(values, id) {
        try {
            console.log(values)

            const response = await axios.post(`${urlDeleteDependent}/${id}`, values);
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

    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Baja de Sub Agente o Corredor</ModalHeader>
            <ModalBody>
                <ToastyErrors errors={errors} />
                <DependentQuitForm model={{
                    description: '',
                }}

                    onSubmit={async (values, { resetForm }) => {
                        let response = await New(values, id);
                        if (response) {
                            toast.success("Dado de baja correctamente")
                            setErrors([])
                            resetForm()
                            handleClose()
                        }
                    }} />
            </ModalBody>
            <ModalFooter>
                <Button block size="large" layout="outline" onClick={handleClose}>
                    Cerrar
                </Button>
            </ModalFooter>
        </Modal>
    )
}
export default DeleteDependent