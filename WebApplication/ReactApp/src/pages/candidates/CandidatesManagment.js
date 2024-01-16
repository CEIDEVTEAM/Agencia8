import React, { useState, useEffect } from 'react'
import axios from "axios";
import {Link} from 'react-router-dom'
import {
  Table,
  TableHeader,
  TableCell,
  TableBody,
  TableRow,
  TableFooter,
  TableContainer,
  Button,  
  Input
} from '@windmill/react-ui'
import Pagination from '../../utils/generals/Pagination';

import { EditIcon, TrashIcon,SearchIcon, CardsIcon, LikeIcon, MenuIcon, ModalsIcon } from '../../icons'
import { candidateUrl, urlCandidateStep } from '../../utils/http/endpoints';
import PageTitle from '../../components/Typography/PageTitle';
import confirmation from '../../utils/generals/confirmation';
import EditCandidate from './EditCandidate';
import Procedure from './Procedure';
import DecisionSupport from './DecisionSupport';
import { Grid } from '../../components/dataGrid/Grid';


export default function CandidatesManagment () {

  
  const [openModal, setModalOpen] = useState(false)
  const [openProceduresModal, setOpenProceduresModal] = useState(false)
  const [openDecisionModal, setOpenDecisionModal] = useState(false)
  const [id, setId] = useState(0)
  const [search, setSearch] = useState(null)



  function handleEdit(id) {
    setModalOpen(true)
    setId(id);    
  }

  function handleProcedure(id){
    setOpenProceduresModal(true)
    setId(id)    
  }

  function handleSupport(id){
    setOpenDecisionModal(true)
    setId(id)    
  }

  function onClose(){
    setModalOpen(false)
  }

  function onCloseProcedure(){
    setOpenProceduresModal(false)
  }

  function onCloseSupport(){
    setOpenDecisionModal(false)
  }



  const labels = ["Número","Documento","Nombres","Apellidos",
  "Condición","Dirección Comercio","Barrio","Teléfonos","Teléfonos comercio",
  "Dirección Personal","Inscripción"]
  const columns = ["number","personalDocument","name","lastName",
  "condition","address","neighborhood","phone","cPhone","personalAddress","addRow"]

  return (
    <>
    <PageTitle>Gestión de Aspirantes</PageTitle>
 
      {/* <TableContainer className="mb-8">
        <Table>
          <TableHeader>
            <tr>
              <TableCell>Acciones</TableCell>
              {labels.map((label, i) => <TableCell key={i}>{label}</TableCell>)}
            </tr>
          </TableHeader>
          <TableBody>
            {dataTable.map((data, i) => (
              <TableRow key={data.id}>
                <TableCell>
                  <div className="flex items-center space-x-4">
                    <Button title = "Editar" onClick={() => handleEdit(data.id)} layout="link" size="icon" aria-label="Edit">
                      <EditIcon className="w-5 h-5" aria-hidden="true" />
                    </Button>
                    <Button title ="Actualizar Trámite" onClick={()=> handleProcedure(data.id)} layout="link" size="icon" aria-label="Delete">
                      <CardsIcon className="w-5 h-5" aria-hidden="true" />
                    </Button>
                    {data.condition ==="Corredor" ? null : <Button title ="Recomendación" onClick={()=> handleSupport(data.id)} layout="link" size="icon" aria-label="Delete">
                      <ModalsIcon className="w-5 h-5" aria-hidden="true" />
                    </Button>}
                  </div>
                </TableCell>
                {columns.map((column, i) => <TableCell key={i}>{data[column]}</TableCell>)}

              </TableRow>
            ))}
          </TableBody>
        </Table>
        <TableFooter>
          <Pagination
            totalResults={totalResults}
            resultsPerPage={recordsPerPage}
            onChange={onPageChangeTable}
            label="Table navigation"
          />
        </TableFooter>
      </TableContainer> */}
      <Grid url={candidateUrl} columnsData={columns} labelsData={labels} ></Grid>
      <EditCandidate isOpen={openModal} onClose={onClose} id={id}></EditCandidate> 
      <Procedure isOpen={openProceduresModal} onClose={onCloseProcedure} id={id}></Procedure>
      <DecisionSupport isOpen={openDecisionModal} onClose={onCloseSupport} id={id}></DecisionSupport>
    </>
  )
}

