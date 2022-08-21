import React from "react"
export default function ShowErrors(props){
    const style= {color: 'red'}
    return(
        <>
            {props.errors.length > 0 ? 
            <ul className="border-red-400 rounded-b bg-red-100 px-4 py-3 text-red-700">
                {props.errors.map((error) => <li key={error}>{error}</li>)}
            </ul> : null}
        </>
    )
}

