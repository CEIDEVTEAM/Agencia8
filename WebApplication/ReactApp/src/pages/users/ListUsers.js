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
  Pagination,
  Input
} from '@windmill/react-ui'
import EditUser from './EditUser';

import { EditIcon, TrashIcon, SearchIcon } from '../../icons'
import { userUrl } from '../../utils/http/endpoints';
import PageTitle from '../../components/Typography/PageTitle';
import confirmation from '../../utils/generals/confirmation';


export default function ListUsers () {

  const recordsPerPage = 30
  const [totalResults, setTotalResults] = useState(0);
  const [page, setPage] = useState(1)
  const [dataTable, setDataTable] = useState([])

  const [openModal, setModalOpen] = useState(false)
  const [id, setId] = useState(0)

  const [search, setSearch] = useState(null)
  
  useEffect(() => {   
    loadData();
    console.log(search)
    // eslint-disable-next-line react-hooks/exhaustive-deps

  }, [page, setPage, openModal, search])

  function loadData() {
    axios.get(userUrl, {
      params: {page, recordsPerPage, search }
    })
      .then((response) => {
        const totalRecords =
          parseInt(response.headers['totalrecords'], 10);
        setDataTable(response.data);
        setTotalResults(Math.ceil(totalRecords))
        console.log(response)
        console.log(totalRecords)
        console.log(totalRecords)        
      })
  }

  async function logicDelete(id) {
    try {
        await axios.delete(`${userUrl}/${id}`)
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
  function onClose(){
    setModalOpen(false)
  }


  const labels = ["Nombre de Usuario","Email","Nombre","Direccion","Telefono","Role","Fecha Alta","Fecha Modificación","Flag"]
  const columns = ["userName","email","name","address","phone","idRole","addRow","updRow","activeFlag"]

  return (
    <>
    <PageTitle>Listado de Usuarios</PageTitle>
    <div className="relative w-full max-w-xl mr-6 focus-within:text-purple-500">
            <div className="absolute inset-y-0 flex items-center pl-2">
              <SearchIcon className="w-4 h-4" aria-hidden="true" />
            </div> 
            <Input
              className="pl-8 text-gray-700"
              placeholder="Search for projects"
              aria-label="Search"
              onChange={(e) => setSearch(e.target.value.toLowerCase())}
            /> 
          </div>
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
                  <Button onClick={() => handleEdit(data.id)} layout="link" size="icon" aria-label="Edit">
                    <EditIcon className="w-5 h-5" aria-hidden="true" />
                  </Button>
                  <Button onClick={()=>confirmation(()=> logicDelete(data.id))} layout="link" size="icon" aria-label="Delete">
                    <TrashIcon className="w-5 h-5" aria-hidden="true" />
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
    <EditUser isOpen={openModal} onClose={onClose} id={id}></EditUser>
    </>
  )
}

//export default ListUsers