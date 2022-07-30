import React, { useEffect, useState } from 'react';
import axios, {axiosResponse} from 'axios';
import { testUrl } from '../utils/http/endpoints';
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

function Blank() {
  
  const [test, setTest] = useState([]);
  
  useEffect(()=> {
    axios.get(testUrl).then((response)=>{
      setTest(response.data)
      
    })
  },[])

  console.log(test)
  return (
      
    <TableContainer className="mb-8">
    <Table>
      <TableHeader>
        <tr>
          <TableCell>Nombre</TableCell> 
          <TableCell>Email</TableCell>  
          <TableCell>Nombre</TableCell>           
        </tr>
      </TableHeader>
      <TableBody>
        {test.map((x)=>
      <TableRow key={x.id}>                
                <TableCell>
                  <span className="text-sm">{x.name}</span>
                </TableCell>
                <TableCell>
                  <span className="text-sm">{x.email}</span>
                </TableCell>
                <TableCell>
                  <span className="text-sm">{x.phone}</span>
                </TableCell>
       </TableRow>
    )}
      </TableBody>
        </Table>        
      </TableContainer>
    // <div>{test.map((x)=>
    //   < key={x}>{x} </h4> )}</div>

    )
  
}

export default Blank
