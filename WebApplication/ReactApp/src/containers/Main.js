import React from 'react'
import '../assets/css/customStyles.css'


function Main({ children }) {
  
  return (
    <main className="h-full overflow-y-auto">
      <div className="ccontainer grid px-6 mx-auto">{children}</div>
    </main>
  )
}

export default Main
