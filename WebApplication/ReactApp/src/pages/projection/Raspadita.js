import React, { useState, useEffect } from 'react'
import axios from "axios";
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

import { EditIcon, TrashIcon, SearchIcon } from '../../icons'
import { raspaditaUrl } from '../../utils/http/endpoints';
import PageTitle from '../../components/Typography/PageTitle';
import EditConcept from './EditConcept';
import NewConcept from './NewConcept';


export default function Raspadita() {

  const recordsPerPage = 30
  const [totalResults, setTotalResults] = useState(0);
  const [page, setPage] = useState(1)
  const [dataTable, setDataTable] = useState([])

  const [openModal, setModalOpen] = useState(false)
  const [openDeleteModal, setModalDeleteOpen] = useState(false)
  const [OpenNewModal, setModalNewOpen] = useState(false)
  const [id, setId] = useState(0)
  const [search, setSearch] = useState(null)
 

  useEffect(() => {
    loadData();
        // eslint-disable-next-line react-hooks/exhaustive-deps

  }, [page, setPage, openModal, openDeleteModal, search])

  function loadData() {
    axios.get(raspaditaUrl, {
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
    setModalDeleteOpen(true)
    setId(id);
  }


  function onPageChangeTable(p) {
    setPage(p)
  }

  function handleEdit(id) {
    setModalOpen(true)
    setId(id);
  }
  function handleNew(id) {
    setModalNewOpen(true)
    setId(id);
  }
  function onClose() {
    setModalOpen(false)
  }

  function onCloseDelete() {
    setModalDeleteOpen(false)
  }
  function onCloseNew() {
    setModalNewOpen(false)
  }



  const labels = ["Nombre", "Valor", "Pediodo","Fecha alta","Fecha Edición"]
  const columns = ["name", "value","description", "addRow","updRow"]

  return (
    <>
      <PageTitle>Raspadita</PageTitle>
      <div className="relative w-full max-w-xl mr-6 focus-within:text-purple-500">
        <div className="absolute inset-y-0 flex items-center pl-2">
          {/* <SearchIcon className="w-4 h-4" aria-hidden="true" /> */}
        </div>
        <Input
          className="pl-8 text-gray-700"
          placeholder="Búsqueda por nombre"
          aria-label="Búsqueda"
          onChange={(e) => {
            if (e.target.value === "") {
              setSearch(null)
            } else { setSearch(e.target.value.toLowerCase()) }
          }}
        />
        <br/>
        <div>
          <Button onClick={() =>handleNew()} >Nuevo Concepto</Button>
        </div>
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
            {dataTable? dataTable.map((data, i) => (
              <TableRow key={data.id}>
                <TableCell>
                  <div className="flex items-center space-x-4">
                    <Button title="Editar" onClick={() => handleEdit(data.id)} layout="link" size="icon" aria-label="Edit">
                      <EditIcon className="w-5 h-5" aria-hidden="true" />
                    </Button>
                    
                  </div>
                </TableCell>
                {columns.map((column, i) => <TableCell key={i}>{data[column]}</TableCell>)}

              </TableRow>
            )):<TableRow>.....Cargando</TableRow>}
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
      <EditConcept isOpen={openModal} onClose={onClose} id={id}></EditConcept>
      <NewConcept isOpen={OpenNewModal} onClose={onCloseNew} ></NewConcept>
      
    </>
  )
}