import React, { useState, useEffect } from 'react'
import axios from "axios";
import { Link } from "react-router-dom"
import {
  Table,
  TableHeader,
  TableCell,
  TableBody,
  TableRow,
  TableFooter,
  TableContainer,
  Badge,
  Avatar,
  Button,
  Pagination,
} from '@windmill/react-ui'

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
        setDataTable(response.data);
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

  // useEffect(() => {
  //    setDataTable(props.response.slice((page - 1) * recordsPerPage, page * recordsPerPage))
  // }, [page])


  function onPageChangeTable(p) {
    setPage(p)
  }

  function handleEdit(id) {
    console.log(id)

  }

  return (
    <TableContainer className="mb-8">
      <Table>
        <TableHeader>
          <tr>
            <TableCell>Acciones</TableCell>
            {props.labels.map((label, i) => <TableCell key={i}>{label}</TableCell>)}
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
                  <Button layout="link" size="icon" aria-label="Delete">
                    <TrashIcon className="w-5 h-5" aria-hidden="true" />
                  </Button>
                </div>
              </TableCell>
              {props.columns.map((column, i) => <TableCell key={i}>{data[column]}</TableCell>)}

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

  )
}

