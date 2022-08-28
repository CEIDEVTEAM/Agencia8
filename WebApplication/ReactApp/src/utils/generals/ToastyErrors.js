import React, { useEffect } from 'react';
import {ToastContainer,toast } from 'react-toastify';

import "react-toastify/dist/ReactToastify.css";

const ToastyErrors = (props) => {       
        if(props.errors.length > 0 )
            props.errors.forEach(error => {
                toast.error(error)
            });   
    
    return (
        <></>);
}

export default ToastyErrors
