import React, { useEffect, useState } from "react";
import { raspaditaUrl } from "../../utils/http/endpoints";
import Edit from "../../utils/Edit/Edit";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'
import RaspaditaForm from "../../components/form/Models/RaspaditaForm";

function EditRaspadita(props) {

    const handleClose = () => {
        props.onClose();
    }

    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Editar Raspadita</ModalHeader>
            <ModalBody >
                <Edit url={raspaditaUrl} id={props.id}>
                    {(entidad, editar) =>
                        <RaspaditaForm model={entidad} isEdit={true}
                            onSubmit={async valores => {
                                await editar(valores)
                            }} />}
                </Edit>
            </ModalBody>
            <ModalFooter>
                <Button className="w-full sm:w-autoblock" size="large" layout="outline" onClick={handleClose}>
                    Cerrar
                </Button>
            </ModalFooter>
        </Modal>
    )
}
export default EditRaspadita