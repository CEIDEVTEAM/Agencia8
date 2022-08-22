import React from "react"
export default function ShowSuccess(props){
    const style= {color: 'red'}
    
    return(
        <>
            {props.success !== undefined || props.success != null ? 
            <ul className="border-green-400 rounded-b bg-green-100 px-4 py-3 text-green-700">
                {props.success}
            </ul> : null}
        </>
    )
}