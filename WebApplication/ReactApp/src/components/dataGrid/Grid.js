import React, { useState, useEffect, Fragment, Children } from 'react'
import {
  Table,
  TableHeader,
  TableCell,
  TableBody,
  TableRow,
  TableFooter,
  TableContainer,
  Input,
  Button,
  Select,
  Label
} from '@windmill/react-ui'
import confirmation from '../../utils/generals/confirmation';
import CustomPagination from '../../utils/generals/CustomPagination';
import { EditIcon, TrashIcon, SearchIcon } from '../../icons'
import axios from 'axios';


export const Grid = ({ url, columnsData, labelsData, searchLabel, buttons, refresh }) => {

  const [defaultRecordsPerPage, setDefaultRecordsPerPage] = useState(10)
  const [recordsPerPage, setRecordsPerPage] = useState(defaultRecordsPerPage)
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
      isApiSubscribed = false;
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps


  }, [page, setPage, search, refresh, recordsPerPage])

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
        //console.log(response)
      })
  }

  function handleSearch(e) {
    setSearch(e.target.value.toLowerCase())
    setPage(1)
    console.log(recordsPerPage)
  }

  function harndleRecordsPerPage(e){
    setRecordsPerPage(e.target.value)
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
          placeholder={searchLabel}
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
              {buttons ? <TableCell className="font-bold">Acciones</TableCell> : null}
              {labels.map((label, i) => <TableCell className="border px-8 py-4 font-bold" key={i}>{label}</TableCell>)}
            </tr>
          </TableHeader>
          <TableBody>
            {data.map((data, i) => (
              <TableRow className="hover:bg-gray-100" key={i}>
                {buttons ?
                  <TableCell className="border px-8 py-4 relative">
                    <div className="flex items-center space-x-4">
                      {buttons.map((button, j) =>
                        <Button key={j}
                          onClick={() => {
                            button.onClick(data.id);
                          }}
                          layout="link" size="icon" aria-label={button.label} title={button.title}
                        >
                          {button.icon}
                        </Button>)}
                    </div>
                  </TableCell> : null}
                {columns.map((column, i) =>
                  <TableCell className="border px-8 py-4 " key={i}>{data[column]}</TableCell>)}

              </TableRow>
            ))}
          </TableBody>
        </Table>
        <TableFooter>
          <div className="flex justify-between mb-4">
            <div className='grid md:grid-cols-2 md:gap-6'>
              <Label  className="text-sm font-medium text-gray-600" >Registros por página:
                <Select value={recordsPerPage} 
                onChange={(e) => {
                  if (e.target.value === "") {
                    setRecordsPerPage(null)
                  } else { harndleRecordsPerPage(e) }
                }}>
                  <option value="5">5</option>
                  <option value="10">10</option>
                  <option value="15">15</option>
                  <option value="20">20</option>
                </Select>
              </Label>
            </div>
            <CustomPagination
              cantidadTotalDePaginas={totalDePaginas}
              paginaActual={page}
              onChange={newPage => setPage(newPage)}
            />
          </div>
        </TableFooter>
      </TableContainer>
    </>


  )
}

