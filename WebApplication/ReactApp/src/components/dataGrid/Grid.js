import React, { useState, useEffect, Children } from 'react'
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
import confirmation from '../../utils/generals/confirmation';
import CustomPagination from '../../utils/generals/CustomPagination';
import { EditIcon, TrashIcon, SearchIcon } from '../../icons'
import axios from 'axios';


export const Grid = ({ url, columnsData, labelsData }) => {

  const recordsPerPage = 30
  const [totalResults, setTotalResults] = useState(0);
  const [page, setPage] = useState(1)
  const [data, setData] = useState([])
  const [totalDePaginas, setTotaDePaginas] = useState(0);
  const [search, setSearch] = useState(null)
  const [labels, setLabels] = useState([])
  const [columns, setColums] = useState([])


  useEffect(() => {
    let isApiSubscribed = true;
    if (isApiSubscribed) {
      loadData();
      setLabels(labelsData);
      setColums(columnsData);
    }
    return () => {
      // cancel the subscription
      isApiSubscribed = false;
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps


  }, [page, setPage, search])

  async function loadData() {
    await axios.get(url, {
      params: { page, recordsPerPage, search }
    })
      .then((response) => {
        const totalRecords =
          parseInt(response.headers['totalrecords'], 10);
        setData(response.data);
        setTotalResults(totalRecords)
        setTotaDePaginas(Math.ceil(totalRecords / recordsPerPage))
        console.log(response)
      })
  }

  function handleSearch(e) {
    setSearch(e.target.value.toLowerCase())
    setPage(1)
  }

  return (
    <>
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
            } else { handleSearch(e) }
          }}
        />
      </div>
      <br />
      <TableContainer className="mb-6">
        <Table>
          <TableHeader className="border px-8 py-4">
            <tr>
              <TableCell className="font-bold">Acciones</TableCell>
              {labels.map((label, i) => <TableCell className="border px-8 py-4 font-bold" key={i}>{label}</TableCell>)}
            </tr>
          </TableHeader>
          <TableBody>
            {data.map((data, i) => (
              <TableRow className="hover:bg-gray-100" key={data.id}>
                

                {/* <TableCell className="border px-8 py-4">
                <div className="flex items-center space-x-4">
                  <Button onClick={() => handleEdit(props.data.id)} layout="link" size="icon" aria-label="Edit">
                    <EditIcon className="w-5 h-5" aria-hidden="true" />
                  </Button>
                  <Button onClick={() => confirmation(() => logicDelete(props.data.id))} layout="link" size="icon" aria-label="Delete">
                    <TrashIcon className="w-5 h-5" aria-hidden="true" />
                  </Button>
                </div>
              </TableCell> */}
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
    </>


  )
}

