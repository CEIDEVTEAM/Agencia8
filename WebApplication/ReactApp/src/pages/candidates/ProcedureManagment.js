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
    Modal, ModalHeader, ModalBody, ModalFooter,
    Card, CardBody
} from '@windmill/react-ui'
import { EditIcon, TrashIcon, SearchIcon } from '../../icons'
import { urlCandidateStep } from '../../utils/http/endpoints';
import { urlCandidatePostStep } from '../../utils/http/endpoints';
import ProcedureForm from '../../components/form/Models/ProcedureForm';
import { toast } from 'react-toastify';
import ToastyErrors from "../../utils/generals/ToastyErrors";


export default function ProcedureManagment(props) {

    const handleClose = () => {
        props.onCloseProcedure();
    }

    const recordsPerPage = 30
    const [totalResults, setTotalResults] = useState(0);
    const [page, setPage] = useState(1)
    const [dataTable, setDataTable] = useState([])


  
    function loadData() {
        axios.get(`${urlCandidateStep}/${props.id}`, {
            params: { page, recordsPerPage }
        })
            .then((response) => {
                const totalRecords =
                    parseInt(response.headers['totalrecords'], 10);
                setDataTable(response.data);
                setTotalResults(totalRecords)
                console.log(response)
            })
    }

    function onPageChangeTable(p) {
        setPage(p)
    }

    const [errors, setErrors] = useState([]);
    async function New(values) {
        try {
            console.log(values)

            const response = await axios.post(`${urlCandidatePostStep}/${props.id}`, values);
            console.log(response)

            if (response.data.successful) {
                return true;
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

    const labels = ["Estado","Descripción","Fecha"]
    const columns = ["colProcedureStep.stepType","colProcedureStep.description","colProcedureStep.addRow"]


    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Tramitación</ModalHeader>
            <hr/>
            <ModalBody>
                <Card colored className="text-white bg-purple-600">
                    <CardBody>
                        <p className="mb-4 font-semibold">Aspirante: {dataTable.candidate.name}</p>
                        <p className="mb-4 font-semibold">Barrio: {dataTable.candidate.neighborhood}</p>
                        <p className="mb-4 font-semibold">Calidad: {dataTable.candidate.condition}</p>
                    </CardBody>
                </Card>
                {/* <TableContainer className="mb-6">
                    <Table>
                        <TableHeader>
                            <tr>
                                {labels.map((label, i) => <TableCell key={i}>{label}</TableCell>)}
                            </tr>
                        </TableHeader>
                        <TableBody>
                            {dataTable.map((data, i) => (
                                <TableRow key={data.id}>
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
                <hr/> */}
                <ToastyErrors errors={errors} />
                <ProcedureForm model={{
                    stepType: '',
                    description: ''
                }}

                    onSubmit={async (values, { resetForm }) => {
                        let response = await New(values);
                        if (response) {
                            toast.success("Guardado correctamente")
                            setErrors([])
                            resetForm()
                        }
                    }} />
            </ModalBody>
            <ModalFooter>
                <Button block size="large" layout="outline" onClick={handleClose}>
                    Cerrar
                </Button>
            </ModalFooter>
        </Modal>
    )
}
