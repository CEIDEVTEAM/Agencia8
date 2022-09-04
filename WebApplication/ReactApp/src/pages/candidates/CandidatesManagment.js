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
  Pagination,
  Input
} from '@windmill/react-ui'

import { EditIcon, TrashIcon,SearchIcon, CardsIcon } from '../../icons'
import { candidateUrl, urlCandidateStep } from '../../utils/http/endpoints';
import PageTitle from '../../components/Typography/PageTitle';
import confirmation from '../../utils/generals/confirmation';
import EditCandidate from './EditCandidate';
import ProcedureManagment from './ProcedureManagment'
import Procedure from './Procedure';


export default function CandidatesManagment () {

  const recordsPerPage = 30
  const [totalResults, setTotalResults] = useState(0);
  const [page, setPage] = useState(1)
  const [dataTable, setDataTable] = useState([])  

  const [openModal, setModalOpen] = useState(false)
  const [openProceduresModal, setOpenProceduresModal] = useState(false)
  const [id, setId] = useState(0)
  const [search, setSearch] = useState(null)


  useEffect(() => {   
    loadData();
    // eslint-disable-next-line react-hooks/exhaustive-deps

  }, [page, setPage, openModal, openProceduresModal,search])

  function loadData() {
    axios.get(candidateUrl, {
      params: { page, recordsPerPage, search }
    })
    .then((response) => {
      const totalRecords =
        parseInt(response.headers['totalrecords'], 10);
      setDataTable(response.data);
      setTotalResults(totalRecords)
      console.log(response)       
      })
  }

  async function logicDelete(id) {
    try {
        await axios.delete(`${candidateUrl}/${id}`)
        loadData();
    }
    catch (error) {
        console.log(error.response.data);
    }
  }  

  function onPageChangeTable(p) {
    setPage(p)
  }

  function handleEdit(id) {
    setModalOpen(true)
    setId(id);    
  }
  function handleProcedure(id){
    setOpenProceduresModal(true)
    setId(id)    
  }
  function onClose(){
    setModalOpen(false)
  }

  function onCloseProcedure(){
    setOpenProceduresModal(false)
  }



  const labels = ["Documento","Nombres","Apellidos",
  "Condición","Dirección","Teléfonos","Teléfonos comercio",
  "Dirección Personal","Inscripción"]
  const columns = ["personalDocument","name","lastName",
  "condition","address","phone","cPhone","personalAddress","addRow"]

  return (
    <>
    <PageTitle>Gestión de Aspirantes</PageTitle>
      <div className="relative w-full max-w-xl mr-6 focus-within:text-purple-500">
        <div className="absolute inset-y-0 flex items-center pl-2">
          <SearchIcon className="w-4 h-4" aria-hidden="true" />
        </div>
        <Input
          className="pl-8 text-gray-700"
          placeholder="Búsqueda por documento o apellido"
          aria-label="Búsqueda"
          onChange={(e) => {
            if (e.target.value === "") {
              setSearch(null)
            } else { setSearch(e.target.value.toLowerCase()) }
          }}
        />
      </div>
      <br />
      <TableContainer className="mb-8">
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
      </TableContainer>
      <EditCandidate isOpen={openModal} onClose={onClose} id={id}></EditCandidate> 
      <Procedure isOpen={openProceduresModal} onClose={onCloseProcedure} id={id}></Procedure>
    </>
  )
}

