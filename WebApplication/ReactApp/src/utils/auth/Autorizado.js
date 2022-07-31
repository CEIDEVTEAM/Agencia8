import React, { ReactElement, useContext, useEffect, useState } from "react";
import AuthContext from "../../context/AuthContext";

export default function Autorizado(props) {
    const [estaAutorizado, setEstaAutorizado] = useState(false);
    const { claims } = useContext(AuthContext);

    useEffect(() => {
        if (props.role) {
            const indice = claims.findIndex(claim =>
                claim.nombre === 'role' && claim.valor === props.role)
            setEstaAutorizado(indice > -1);
        } else {
            setEstaAutorizado(claims.length > 0);
        }
    }, [claims, props.role])

    return (
        <>
            {estaAutorizado ? props.autorizado : props.noAutorizado}
        </>
    )
}
