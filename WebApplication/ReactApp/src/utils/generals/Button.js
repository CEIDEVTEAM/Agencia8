import React, {useEffect, useState} from "react"
export default function Button(props){
    
    const [buttonClass, setbuttonClass]  = useState("");
    
    useEffect(() =>{
        switch (props.className) {
            case "blue":
                setbuttonClass("text-white bg-blue-500 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-large rounded text-md px-5 py-2 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800")  
                break;
            case "green":
                setbuttonClass("text-white bg-green-500 hover:bg-green-800 focus:ring-4 focus:ring-green-300 font-large rounded text-md px-5 py-2 mr-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 focus:outline-none dark:focus:ring-green-800")  
                break;
            case "red":
                setbuttonClass("text-white bg-red-500 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-large rounded text-md px-5 py-2 mr-2 mb-2 dark:bg-red-600 dark:hover:bg-red-700 focus:outline-none dark:focus:ring-red-800")  
                break;
            case "yellow":
                setbuttonClass("text-white bg-yellow-300 hover:bg-yellow-500 focus:ring-4 focus:ring-yellow-300 font-large rounded text-md px-5 py-2 mr-2 mb-2 dark:yellow-600 dark:hover:bg-yellow-700 focus:outline-none dark:focus:ring-yellow-800")  
                break; 
            default:
                setbuttonClass("text-white bg-yellow-100 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-large rounded text-md px-5 py-2 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800")  
            break;
        }
    },[])
    
    return (
        <button type={props.type}  
            disabled={props.disabled}
            onClick={props.onClick}
            className={buttonClass}>
            {props.children}
        </button>       
    )
}

Button.defaultProps = {
    type: "button",
    disabled: false    
}