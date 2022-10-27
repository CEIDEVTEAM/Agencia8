import React, { useEffect, useState } from "react";
import PageTitle from '../../components/Typography/PageTitle'



export default function Analisis() {

    return (  
        <>
            <PageTitle>Análisis</PageTitle>      

            <iframe title="Agencia8" width="1140" height="541.25" 
            src="https://app.powerbi.com/reportEmbed?reportId=8f8365e0-c316-467c-adc1-5d2a1dc514ca&autoAuth=true&ctid=7b2f04b2-47ec-4ebc-8dcf-ce43a5248f33"
            frameBorder="0" allowFullScreen={true}></iframe>
         </>

    )
}