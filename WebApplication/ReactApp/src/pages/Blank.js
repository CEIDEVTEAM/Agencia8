import React from 'react'
import UserForm from '../components/form/Models/UsersForm'

import PageTitle from '../components/Typography/PageTitle'
import Button from '../utils/generals/Button'

function Blank() {
  return (
    // <>
    //   <PageTitle>Blank</PageTitle>
    //   <div className="inline-flex">
    //     <Button className="green">Aceptar</Button>
    //     <Button className="green">Pepe</Button>
    //   </div>
    // </>
    // <>
    

    <UserForm modelo={{nombre: ''}} 
         onSubmit={async valores => {
            await new Promise(r => setTimeout(r, 3000))
            console.log(valores);
         }}
    />

  )
}

export default Blank
