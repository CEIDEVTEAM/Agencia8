import React from 'react'
import UserForm from '../../components/form/Models/UsersForm'
import PageTitle from '../../components/Typography/PageTitle'

function NewUser () {
    return (
        <>
            <div className="px-4 py-3 mb-8 bg-white rounded-lg shadow-md dark:bg-gray-800">
                <PageTitle>Crear Usuario</PageTitle>
                <UserForm modelo={{nombre: '', apellido: "", userName: ""}} 
                    onSubmit={async valores => {
                        await new Promise(r => setTimeout(r, 3000))
                        console.log(valores);
                    }}
                />
            </div>
        </>
    )
}
export default NewUser