import React, { useState, useEffect } from 'react'
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
import { EditIcon, TrashIcon, SearchIcon } from '../../icons'
import confirmation from '../../utils/generals/confirmation';
import CustomPagination from '../../utils/generals/CustomPagination';

import { EditIcon, TrashIcon } from '../../icons'


export const Grid = (props) => {

  const recordsPerPage = 10
  const [totalResults, setTotalResults] = useState(0);
  const [page, setPage] = useState(1)
  const [dataTable, setDataTable] = useState([])



  useEffect(() => {
    getTotalRecords();
    loadData();
    // eslint-disable-next-line react-hooks/exhaustive-deps

  }, [page, setPage])

  function loadData() {
    axios.get(props.url, {
      params: { page, recordsPerPage }
    })
      .then((response) => {
        const totalRecords =
          parseInt(response.headers['totalRecords'], 10);
        setDataTable(response.data);
        setTotalResults(Math.ceil(totalRecords / recordsPerPage))
        console.log(response.data)
      })

  }

  function getTotalRecords() {
    axios.get(props.totalRecords)
      .then((response) => {
        setTotalResults(response.data)
        console.log(response.data)
      })
  }

  function onPageChangeTable(p) {
    setPage(p)
  }

  function handleEdit(id) {
    console.log(id)

  }

  return (
    <TableContainer className="mb-6">
      <Table>
        <TableHeader class="border px-8 py-4">
          <tr>
            <TableCell className="font-bold">Acciones</TableCell>
            {labels.map((label, i) => <TableCell class="border px-8 py-4 font-bold" key={i}>{label}</TableCell>)}
          </tr>
        </TableHeader>
        <TableBody>
          {props.data.map((data, i) => (
            <TableRow className="hover:bg-gray-100" key={data.id}>
              <TableCell className="border px-8 py-4">
                <div className="flex items-center space-x-4">
                  <Button onClick={() => handleEdit(props.data.id)} layout="link" size="icon" aria-label="Edit">
                    <EditIcon className="w-5 h-5" aria-hidden="true" />
                  </Button>
                  <Button onClick={() => confirmation(() => logicDelete(props.data.id))} layout="link" size="icon" aria-label="Delete">
                    <TrashIcon className="w-5 h-5" aria-hidden="true" />
                  </Button>
                </div>
              </TableCell>
              {columns.map((column, i) => <TableCell className="border px-8 py-4 " key={i}>{data[column]}</TableCell>)}

            </TableRow>
          ))}
        </TableBody>
      </Table>
      <TableFooter>
        <CustomPagination
          cantidadTotalDePaginas={totalDePaginas}
          paginaActual={page}
          onChange={newPage => setPage(newPage)}
        />
      </TableFooter>
    </TableContainer>


  )
}

