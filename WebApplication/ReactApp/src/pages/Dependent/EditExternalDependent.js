import React, { useEffect, useState } from "react";
import { getExternalDependentUrl } from "../../utils/http/endpoints";
import Edit from "../../utils/Edit/Edit";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'
import ExternalDependentForm from "../../components/form/Models/ExternalDependentForm";

function EditExternalDependent(props) {

    const handleClose = () => {
        props.onClose();
    }

    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Editar SubAg Externo</ModalHeader>
            <ModalBody>
                <Edit url={getExternalDependentUrl} id={props.id}>
                    {(entidad, editar) =>
                        <ExternalDependentForm model={entidad} isEdit={true}
                            onSubmit={async valores => {
                                await editar(valores)
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
export default EditExternalDependent