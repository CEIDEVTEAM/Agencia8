import React from "react"
export default function ShowErrors(props){
    const style= {color: 'red'}
    return(
        <>
            {props.errors ? <ul style={style}>
                {props.errors.map((error) => <li key={error}>{error}</li>)}
            </ul> : null}
        </>
    )
}

