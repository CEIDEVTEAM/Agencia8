import React, { useEffect, useState } from "react";
import { urlCandidateStep } from "../../utils/http/endpoints";
import Edit from "../../utils/Edit/Edit";
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from '@windmill/react-ui'
import ProcedureForm from "../../components/form/Models/ProcedureForm";

function Procedure(props) {

    const handleClose = () => {
        props.onClose();
    }
    const [isFinalStep, setIsFinalStep] = useState(false)

    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Trámite</ModalHeader>
            <ModalBody>
                <Edit url={urlCandidateStep} id={props.id}>
                    {(entidad, editar) =>
                        <ProcedureForm model={{ stepType: '', description: ''} } 
                            isEdit={true}
                            options = {entidad.colStepTypes}
                            info = {entidad.candidate}
                            steps = {entidad.colProcedureStep}
                            onSubmit={async (valores,{resetForm}) => {
                                await editar(valores,resetForm())
                                if(valores.stepType === "ACEPTADO"|| valores.stepType==="DECLINADO")
                                    handleClose()  
                                                                                                                                                               
                            }}
                             />}
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
export default Procedure