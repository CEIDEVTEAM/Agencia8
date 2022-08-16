import { database } from 'faker/lib/locales/en';
import React, { useEffect, useState } from 'react';
import { Grid } from '../components/dataGrid/Grid';
import {userUrl} from '../utils/http/endpoints'
import {userUrlTotalRecords} from '../utils/http/endpoints'
import {urerUrlEdit} from '../utils/http/endpoints'
import response from '../utils/demo/tableData'
import { EditIcon, TrashIcon } from '../icons'
import {
  Button  
} from '@windmill/react-ui'
// import axios, {axiosResponse} from 'axios';
// import { testUrl } from '../utils/http/endpoints';
// import AutForm from '../components/form/Models/AutForm';
// import {
//   Table,
//   TableHeader,
//   TableCell,
//   TableBody,
//   TableRow,
//   TableFooter,
//   TableContainer,
//   Badge,
//   Avatar,
//   Button,
//   Pagination,
// } from '@windmill/react-ui'

function Blank() {

    const labels =  ["id","Nombre de Usuario", "Contraseña", "email", "Dirección", "Telefono", "RolId","Fecha Agregado","Fecha Actualizado"]
    const columns = ["id","userName", "password", "email", "address", "phone", "idRole","addRow","updRow"]
    
 return(

    
    <Grid totalRecords = {userUrlTotalRecords}
        url = {userUrl}
        urlEdit = {urerUrlEdit}
        columns = {columns} 
        labels = {labels}>
          <div className="flex items-center space-x-4">
            <Button layout="link" size="icon" aria-label="Edit">
              <EditIcon className="w-5 h-5" aria-hidden="true" />
            </Button>
            <Button layout="link" size="icon" aria-label="Delete">
              <TrashIcon className="w-5 h-5" aria-hidden="true" />
            </Button>
          </div>
    </Grid>


 )
  
  
}

export default Blank
