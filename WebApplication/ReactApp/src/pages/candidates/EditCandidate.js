import React, { useEffect, useState } from "react";
import { candidateUrl } from "../../utils/http/endpoints";
import Edit from "../../utils/Edit/Edit";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'
import CandidateForm from "../../components/form/Models/CandidateForm";

function EditCandidate(props) {

    const handleClose = () => {
        props.onClose();
    }

    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Editar Candidato</ModalHeader>
            <ModalBody>
                <Edit url={candidateUrl} id={props.id}>
                    {(entidad, editar) =>
                        <CandidateForm model={entidad} isEdit={true}
                            onSubmit={async valores => {
                                await editar(valores)
                                handleClose()
                            }} />}
                </Edit>
            </ModalBody>
            <ModalFooter>
                <Button block size="large" layout="outline" onClick={handleClose}>
                    Cerrar
                </Button>
            </ModalFooter>
        </Modal>
    )
}
export default EditCandidate