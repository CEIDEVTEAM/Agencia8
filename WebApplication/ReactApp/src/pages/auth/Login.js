import React from 'react'
import AutForm from '../../components/form/Models/AutForm'
import PageTitle from '../../components/Typography/PageTitle'

function Login () {
    
    return (        
        
            <AutForm modelo={{user:'', password:''}} 
                    onSubmit={async valores => {
                        await new Promise(r => setTimeout(r, 3000))
                        console.log(valores);
            }}/>
                  
    )
}
export default Login