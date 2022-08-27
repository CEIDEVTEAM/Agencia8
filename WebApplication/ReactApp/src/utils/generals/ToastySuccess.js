import React, { useEffect } from 'react';
import { ToastContainer, toast } from 'react-toastify';

import "react-toastify/dist/ReactToastify.css";

const ToastySuccess = (props) => {
    if (props.success !== undefined || props.success != null)

        toast.success(props.success)


    return (
        <></>);
}

export default ToastySuccess
