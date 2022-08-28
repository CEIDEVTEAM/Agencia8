import React, { useEffect } from 'react';
import { ToastContainer, toast } from 'react-toastify';

import "react-toastify/dist/ReactToastify.css";

const ToastyWarn = (props) => {
    if (props.warn !== undefined || props.warn != null)

        toast.warn(props.warn)


    return (
        <></>);
}

export default ToastyWarn
