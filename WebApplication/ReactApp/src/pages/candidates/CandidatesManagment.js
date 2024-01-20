import React, { useState, useEffect } from 'react'
import { EditIcon, TrashIcon, SearchIcon, CardsIcon, LikeIcon, MenuIcon, ModalsIcon } from '../../icons'
import { candidateUrl, urlCandidateStep } from '../../utils/http/endpoints';
import PageTitle from '../../components/Typography/PageTitle';
import EditCandidate from './EditCandidate';
import Procedure from './Procedure';
import DecisionSupport from './DecisionSupport';
import { Grid } from '../../components/dataGrid/Grid';

export default function CandidatesManagment() {

  const [openModal, setModalOpen] = useState(false)
  const [openProceduresModal, setOpenProceduresModal] = useState(false)
  const [openDecisionModal, setOpenDecisionModal] = useState(false)
  const [id, setId] = useState(0)
  const [refresh, setRefresh] = useState(false)


  function handleEdit(id) {
    setModalOpen(true)
    setId(id);
  }

  function handleProcedure(id) {
    setOpenProceduresModal(true)
    setId(id)
  }

  function handleSupport(id) {
    setOpenDecisionModal(true)
    setId(id)
  }

  function onClose() {
    setModalOpen(false)
    handleRefresh()
  }

  function onCloseProcedure() {
    setOpenProceduresModal(false)
    handleRefresh()
  }

  function onCloseSupport() {
    setOpenDecisionModal(false)
    handleRefresh()
  }

  function handleRefresh(){
    if(refresh == true)
      setRefresh(false)
    else
      setRefresh(true)
  }



  const labels = ["Número", "Documento", "Nombres", "Apellidos",
    "Condición", "Dirección Comercio", "Barrio", "Teléfonos", "Teléfonos comercio",
    "Dirección Personal", "Inscripción"]
  const columns = ["number", "personalDocument", "name", "lastName",
    "condition", "address", "neighborhood", "phone", "cPhone", "personalAddress", "addRow"]

  const buttons = [
    {
      label: "Editar",
      onClick: (id) => handleEdit(id),
      icon: <EditIcon className="w-5 h-5" aria-hidden="true" />
    },
    {
      label: "Procedimiento",
      onClick: (id) => handleProcedure(id),
      icon: <MenuIcon className="w-5 h-5" aria-hidden="true" />
    }
  ]  

  return (
    <>
      <PageTitle>Gestión de Aspirantes</PageTitle>
      <Grid
        url={candidateUrl}
        columnsData={columns}
        labelsData={labels}
        searchLabel={"Busqueda por..."}
        buttons={buttons}
        refresh={refresh}
        >
      </Grid>
      <EditCandidate isOpen={openModal} onClose={onClose} id={id}></EditCandidate>
      <Procedure isOpen={openProceduresModal} onClose={onCloseProcedure} id={id}></Procedure>
      <DecisionSupport isOpen={openDecisionModal} onClose={onCloseSupport} id={id}></DecisionSupport>
    </>
  )
}

