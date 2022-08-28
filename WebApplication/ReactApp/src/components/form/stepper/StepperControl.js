import React from 'react'
import { Button } from '@windmill/react-ui'
export default function StepperControl({ handleClick, currentStep, steps, disabled }) {
  return (
    <div className="grid md:grid-cols-2 md:gap-6">
      <Button
        onClick={() => handleClick()}
        className={`cursor-pointer rounded-xl border-2 border-slate-300 bg-white py-2 px-4 font-semibold uppercase text-slate-400 transition duration-200 ease-in-out hover:bg-slate-700 hover:text-white  ${
          currentStep === 1 ? " cursor-not-allowed opacity-50 " : ""
        }`}
      >
        Atrás
      </Button>

      <Button
        onClick={() => handleClick("next")} disabled = {disabled} type = {currentStep === steps.length ? "submit" : "button"}
        className="cursor-pointer rounded-lg bg-green-500 py-2 px-4 font-semibold uppercase text-white transition duration-200 ease-in-out hover:bg-slate-700 hover:text-white"
      >
        {currentStep === steps.length ? "Confirmar" : "Siguiente"}
      </Button> 
      
      

    </div>
  );
}
