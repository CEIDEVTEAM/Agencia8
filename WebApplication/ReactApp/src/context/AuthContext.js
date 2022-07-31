import React from 'react';

const claims = {
    nombre:"",
    valor:""
}
const AuthContext = React.createContext(
    {claims: [], actualizar: () => {}});

export default AuthContext;