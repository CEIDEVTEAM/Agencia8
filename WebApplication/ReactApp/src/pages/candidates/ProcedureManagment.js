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
  Modal, ModalHeader, ModalBody, ModalFooter
} from '@windmill/react-ui'
import EditUser from './EditUser';
import { EditIcon, TrashIcon, SearchIcon } from '../../icons'
import { userUrl } from '../../utils/http/endpoints';
import confirmation from '../../utils/generals/confirmation';
import Edit from "../../utils/Edit/Edit";


function ProcedureManagment(props) {

    const handleClose = () => {
        props.onClose();
    }

    return (
        <Modal isOpen={props.isOpen} onClose={handleClose} >
            <ModalHeader>Tramitación</ModalHeader>
            <ModalBody>
                <Edit url={userUrl} id={props.id}>
                    {(entidad, editar) =>
                        <UsersForm model={entidad} isEdit={true}
                            onSubmit={async valores => {
                                await editar(valores)
                            }} />}
                </Edit>

                <TableContainer className="mb-6">
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
                                            <Button onClick={() => confirmation(() => logicDelete(data.id))} layout="link" size="icon" aria-label="Delete">
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
            </ModalBody>
            <ModalFooter>
                <Button block size="large" layout="outline" onClick={handleClose}>
                    Cerrar
                </Button>
            </ModalFooter>
        </Modal>
    )
}
export default EditUser