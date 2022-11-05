import React, { useEffect, useState } from "react";
import axios from "axios";
import { useHistory } from "react-router-dom";
import { newConceptUrl } from "../../utils/http/endpoints";
import PageTitle from '../../components/Typography/PageTitle';
import ToastyErrors from "../../utils/generals/ToastyErrors";
import { toast } from 'react-toastify';
import ConceptForm from "../../components/form/Models/ConceptForm";
import { urlParamsOptions } from '../../utils/http/endpoints'
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'

function NewConcept(props) {

    const history = useHistory();
    const [errors, setErrors] = useState([]);
    const [model, setModel] = useState({ paramId: '', value: '' });
    const [params, setParamsList] = useState()

    async function New(values) {
        try {
            console.log(values)

            const response = await axios.post(newConceptUrl, values);
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

    useEffect(() => {   
        getParamList();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    
      }, [])

    function getParamList() {

        axios.get(urlParamsOptions)
            .then((response) => {
                setParamsList(response.data);
                console.log(response.data)

            })
    }
    const handleClose = () => {
        props.onClose();
    }

    return (
       
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Ingresar Concepto</ModalHeader>
            <ModalBody >
            <ToastyErrors errors={errors} />
            <ConceptForm params={params} model={model}
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
export default NewConcept