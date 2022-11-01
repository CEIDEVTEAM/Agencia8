import React, { useEffect, useState } from "react";
import PageTitle from '../../components/Typography/PageTitle'



export default function Projection() {

    return (  
        <>
            <PageTitle>Projección Mensual</PageTitle>      

            <iframe title="ProyeccionMensual" width="1140" height="541.25" src="https://app.powerbi.com/reportEmbed?reportId=c401d862-c268-40db-b566-c2b2328c51bc&autoAuth=true&ctid=7b2f04b2-47ec-4ebc-8dcf-ce43a5248f33" frameborder="0" allowFullScreen="true"></iframe>
         </>

    )
}