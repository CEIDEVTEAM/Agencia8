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
    Input,
    Select
} from '@windmill/react-ui'

import { EditIcon, TrashIcon, SearchIcon } from '../../icons'
import { exCandidateDependent } from '../../utils/http/endpoints';
import PageTitle from '../../components/Typography/PageTitle';
import confirmation from '../../utils/generals/confirmation';



export default function ListExDependent() {

    const recordsPerPage = 30
    const [totalResults, setTotalResults] = useState(0);
    const [page, setPage] = useState(1)
    const [dataTable, setDataTable] = useState([])

    const [openModal, setModalOpen] = useState(false)
    const [openDeleteModal, setModalDeleteOpen] = useState(false)
    const [id, setId] = useState(0)
    const [search, setSearch] = useState(null)
    const [filter, setFilter] = useState(null)


    useEffect(() => {
        loadData();
        // eslint-disable-next-line react-hooks/exhaustive-deps

    }, [page, setPage, openModal, search, filter])

    function loadData() {
        axios.get(exCandidateDependent, {
            params: { page, recordsPerPage, search, filter }
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



    const labels = ["Tipo", "Numero", "Documento", "Nombres", "Apellidos",
        "Condición", "Dirección", "Teléfonos", "Teléfonos comercio",
        "Dirección Personal", "Inscripción"]
    const columns = ["type", "number", "personalDocument", "name", "lastName",
        "condition", "address", "phone", "phoneShopData", "personalAddress", "addRow"]

    return (
        <>
            <PageTitle>Consulta Historica de Sub Agentes, Corredores y Aspirantes</PageTitle>
            <div className="relative w-full max-w-xl mr-6 focus-within:text-purple-500">
                <div className="absolute inset-y-0 flex items-center pl-2">
                    <SearchIcon className="w-4 h-4" aria-hidden="true" />
                </div>
                <div className="grid md:grid-cols-2 md:gap-6">
                    <Input
                        className="pl-8 text-gray-700"
                        placeholder="Búsqueda por CI o apellido"
                        aria-label="Búsqueda"
                        onChange={(e) => {
                            if (e.target.value === "") {
                                setSearch(null)
                            } else { setSearch(e.target.value.toLowerCase()) }
                        }}
                    />
                    <Select onChange={(e) => {
                        if (e.target.value === "") {
                            setFilter(null)
                        } else { setFilter(e.target.value.toLowerCase()) }
                    }} className="mt-1">
                        <option value="">  ----Tipo----</option>
                        <option value="dependent">Ex SubAgente o Corredor</option>
                        <option value="candidate">Ex Aspirante</option>
                    </Select>
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
                        {dataTable.map((data, i) => (
                            <TableRow key={data.id}>
                                <TableCell>
                                    <div className="flex items-center space-x-4">
                                        <Button onClick={() => handleEdit(data.id)} layout="link" size="icon" aria-label="Edit">
                                            <EditIcon className="w-5 h-5" aria-hidden="true" />
                                        </Button>
                                        <Button onClick={() => logicDelete(data.id)} layout="link" size="icon" aria-label="Delete">
                                            <TrashIcon className="w-5 h-5" aria-hidden="true" />
                                        </Button>
                                        {/* <Button onClick={()=>confirmation(()=> logicDelete(data.id))} layout="link" size="icon" aria-label="Delete">
                    <TrashIcon className="w-5 h-5" aria-hidden="true" />
                  </Button> */}
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
        </>
    )
}

//export default ListUsers