import React, { useEffect } from 'react';
import {ToastContainer,toast } from 'react-toastify';

import "react-toastify/dist/ReactToastify.css";

const ToastyErrors = (props) => {
    useEffect(() => {   
        if(props.errors.length > 0 )
            props.errors.forEach(error => {
                toast.error(error, {
                    position: "top-left",
                    autoClose: 5000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                    })
            });
    
      }, [])
    return (
        <></>)
        ;
}

export default ToastyErrors
