import React, { useState, useEffect } from 'react'
import axios from "axios";
import EditUser from './EditUser';
import Loading from "../../utils/generals/Loading"
import { EditIcon, TrashIcon, SearchIcon } from '../../icons'
import { userUrl } from '../../utils/http/endpoints';
import PageTitle from '../../components/Typography/PageTitle';
import confirmation from '../../utils/generals/confirmation';
import { Grid } from '../../components/dataGrid/Grid';


export default function ListUsers() {


  const [refresh, setRefresh] = useState(false)

  const [openModal, setModalOpen] = useState(false)
  const [id, setId] = useState(0)


  async function logicDelete(id) {
    try {
      await axios.delete(`${userUrl}/${id}`)
      handleRefresh()
    }
    catch (error) {
      console.log(error.response.data);
    }
  }

  function handleEdit(id) {
    setModalOpen(true)
    setId(id);
  }
  function onClose() {
    setModalOpen(false)
    handleRefresh()
  }


  function handleRefresh() {
    if (refresh == true)
      setRefresh(false)
    else
      setRefresh(true)
  }

  const buttons = [
    {
      label: "Editar",
      title: "Editar usuario",      
      onClick: (id) => handleEdit(id),
      icon: <EditIcon className="w-5 h-5" aria-hidden="true" />
    },
    {
      label: "Eliminar",
      title: "Eliminar usuario", 
      onClick: (id) => confirmation(logicDelete(id)),
      icon: <TrashIcon className="w-5 h-5" aria-hidden="true" />
    }
  ]


  const labels = ["Nombre de Usuario", "Email", "Nombre", "Dirección", "Telófono", "Rol", "Fecha Alta", "Fecha Modificación"]
  const columns = ["userName", "email", "name", "address", "phone", "roleName", "addRow", "updRow"]

  return (
    <>
      <PageTitle></PageTitle>
      <Grid
        url={userUrl}
        columnsData={columns}
        labelsData={labels}
        searchLabel={"Busqueda por..."}
        buttons={buttons}
        refresh={refresh}
      >
      </Grid>

      <EditUser isOpen={openModal} onClose={onClose} id={id}></EditUser>
    </>
  )
}

