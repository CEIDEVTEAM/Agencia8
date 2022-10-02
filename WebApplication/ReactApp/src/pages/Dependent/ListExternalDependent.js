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

import { EditIcon, SearchIcon } from '../../icons'
import { externalDependentUrl,proccessExternalsUrl } from '../../utils/http/endpoints';
import PageTitle from '../../components/Typography/PageTitle';
import EditExternalDependent from './EditExternalDependent';

export default function ListExternalDependent() {

  const recordsPerPage = 30
  const [totalResults, setTotalResults] = useState(0);
  const [page, setPage] = useState(1)
  const [dataTable, setDataTable] = useState([])

  const [openModal, setModalOpen] = useState(false)
  const [openDeleteModal, setModalDeleteOpen] = useState(false)
  const [id, setId] = useState(0)
  const [search, setSearch] = useState(null)

  const [errors, setErrors] = useState([])
  const [reProccessSuccess, setReProccessSuccess] = useState(false);


  useEffect(() => {
    loadData();
    // eslint-disable-next-line react-hooks/exhaustive-deps

  }, [page, setPage, openModal, openDeleteModal, search, reProccessSuccess])

  function loadData() {
    axios.get(externalDependentUrl, {
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
  function onClose() {
    setModalOpen(false)
  }

  function onCloseDelete() {
    setModalDeleteOpen(false)
  }

  async function reProccess() {
    try {

      console.log('se disparó')
      const response = await axios.post(proccessExternalsUrl);
      console.log(response)

      if (response.data.successful) {
        setReProccessSuccess(true);
        setPage(1);
        setSearch(null)
      } else {
        setErrors(response.data.errors)
        return false;
      }
    }
    catch (error) {
      setErrors(error.errors);
      return false;
    }


  }

  const labels = ["Numero", "Nombres y Apellidos", "Dirección Comercio"]
  const columns = ["number", "name", "address"]

  return (
    <>
      <PageTitle>Sub Agentes Extenos</PageTitle>
      <div className="relative w-full max-w-xl mr-6 focus-within:text-purple-500">
        <Input
          className="pl-8 text-gray-700"
          placeholder="Buscar por: Nombre, dirección o número"
          aria-label="Búsqueda"
          onChange={(e) => {
            if (e.target.value === "") {
              setSearch(null)              
            } else { setSearch(e.target.value.toLowerCase()); onPageChangeTable(1) }
          }}
        />
        <br/>
        <div>
          <Button onClick={() =>reProccess()} >Reprocesar</Button>
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
            {dataTable ? dataTable.map((data, i) => (
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
            )) : <TableRow>.....Cargando</TableRow>}
          </TableBody>
        </Table>
        <TableFooter>
          <Pagination
            totalResults={totalResults}
            resultsPerPage={recordsPerPage}
            onChange={onPageChangeTable}
            currentPage={page}
            label="Table navigation"
          />
        </TableFooter>
      </TableContainer>
      <EditExternalDependent isOpen={openModal} onClose={onClose} id={id}></EditExternalDependent>
    </>
  )
}

//export default ListUsers