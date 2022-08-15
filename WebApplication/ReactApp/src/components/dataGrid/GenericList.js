import React from "react";
import Loading from "../../utils/generals/Loading";

export default function GenericList(props){
    if (!props.dataList){
        if (props.cargandoUI){
            return props.cargandoUI;
        }
        return <Loading/>
    } else if (props.dataList.length === 0){
        if (props.emptyListUI){
            return props.emptyListUI;
        }
        return <>No hay elementos para mostrar</>
    } else{
        return props.children;
    }
}