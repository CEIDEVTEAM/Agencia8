import React,{useEffect, useState} from "react";
import { Card, CardBody } from '@windmill/react-ui'
import { urlCandidateDecision } from "../../../utils/http/endpoints";
import axios  from "axios";
import SupportCard from "./SupportCard";
import Loading from "../../../utils/generals/Loading";

export default function SupportForm(props) {

    const [data, setData] = useState()

    useEffect(() => {
        axios.get(`${urlCandidateDecision}/${props.id}`)
            .then((response) => {
                setData(response.data);
                
            })        
    }, [])
    

    return (
        <Card>
            <CardBody>
                {data? <SupportCard data = {data}/>:<Loading/> }             
                
            </CardBody>
        </Card>


    )
}