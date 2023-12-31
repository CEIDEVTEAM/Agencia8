import React, { useEffect, useState } from "react";
import { userUrl } from "../../utils/http/endpoints";
import UsersForm from "../../components/form/Models/UsersForm";
import Edit from "../../utils/Edit/Edit";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'

function EditUser(props) {

    const handleClose = () => {
        props.onClose();
    }

    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Editar Usuario</ModalHeader>
            <ModalBody>
                <Edit url={userUrl} id={props.id}>
                    {(entidad, editar) =>
                        <UsersForm model={entidad} isEdit={true}
                            onSubmit={async valores => {
                                await editar(valores)
                                handleClose()
                            }} />}
                </Edit>
            </ModalBody>
            
        </Modal>
    )
}
export default EditUser