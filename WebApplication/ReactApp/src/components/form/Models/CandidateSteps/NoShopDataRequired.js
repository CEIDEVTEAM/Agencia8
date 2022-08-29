import React from 'react'
import { ForbiddenIcon } from '../../../../icons'

const NoShopDataRequired = () => {
  return (
    <div className="flex flex-col items-center">
      <ForbiddenIcon className="w-12 h-12 mt-8 text-purple-200" aria-hidden="true" />
      <h1 className="text-6xl font-semibold text-gray-700 dark:text-gray-200">REGISTRO DE CORREDOR</h1>
      <p className="text-gray-700 dark:text-gray-300">
        No se registran datos del comercio para el caso de los corredores, presione "ingresar".     
        
      </p>
    </div>
  )
}

export default NoShopDataRequired