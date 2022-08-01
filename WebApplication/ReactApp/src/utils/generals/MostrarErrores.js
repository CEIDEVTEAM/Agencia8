import React from "react"
export default function MostrarErrores(props){
    const style= {color: 'red'}
    return(
        <>
            {props.errores ? <ul style={style}>
                {props.errores.map((error) => <li key={error}>{error}</li>)}
            </ul> : null}
        </>
    )
}

