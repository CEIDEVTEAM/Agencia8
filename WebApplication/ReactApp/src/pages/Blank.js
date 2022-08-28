import React, { useEffect, useState } from "react";
import PageTitle from '../components/Typography/PageTitle'
import CandidateForm from "../components/form/Models/CandidateForm";
import { toast } from 'react-toastify';
import ToastyErrors from "../utils/generals/ToastyErrors";
import FormMap from "../components/form/form-groups/FormMap";



export default function Blank() {



    return (
        
        < FormMap  campoLat="latitude" campoLng="longitude"/>    
            
    )
}