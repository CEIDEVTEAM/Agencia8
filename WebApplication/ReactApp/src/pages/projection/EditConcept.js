import React from "react";
import { projectionParamUrl } from "../../utils/http/endpoints";
import Edit from "../../utils/Edit/Edit";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'
import ConceptForm from "../../components/form/Models/ConceptForm";

function EditConcept(props) {

    const handleClose = () => {
        props.onClose();
    }

    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Editar Parametros de Projección</ModalHeader>
            <ModalBody >
                <Edit url={projectionParamUrl} id={props.id}>
                    {(entidad, editar) =>
                        <ConceptForm model={entidad} isEdit={true}
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
export default EditConcept