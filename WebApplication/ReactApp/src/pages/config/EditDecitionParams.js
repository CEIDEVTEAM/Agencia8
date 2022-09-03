import React, { useEffect, useState } from "react";
import { decitionParamUrl } from "../../utils/http/endpoints";
import Edit from "../../utils/Edit/Edit";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'
import ParamsForm from "../../components/form/Models/ParamsForm";

function EditDecitionParams(props) {

    const handleClose = () => {
        props.onClose();
    }

    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Editar Parametros de Decisión</ModalHeader>
            <ModalBody >
                <Edit url={decitionParamUrl} id={props.id}>
                    {(entidad, editar) =>
                        <ParamsForm model={entidad} isEdit={true}
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
export default EditDecitionParams