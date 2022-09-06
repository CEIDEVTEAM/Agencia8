import React, { useEffect, useState } from "react";

//import Edit from "../../utils/Edit/Edit";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'
//import ProcedureForm from "../../components/form/Models/ProcedureForm";
import SupportForm from "../../components/form/Models/SupportForm";
import {axios} from 'axios';

function DecisionSupport(props) {

    const handleClose = () => {
        props.onClose();
    }    


    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Recomendación Automática</ModalHeader>
            <ModalBody>
                <SupportForm id = {props.id} />
            </ModalBody>
            <ModalFooter>
                <Button block size="large" layout="outline" onClick={handleClose}>
                    Cerrar
                </Button>
            </ModalFooter>
        </Modal>
    )
}
export default DecisionSupport