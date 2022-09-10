import React from 'react'
import defaultTheme from '../themes/default'


export const CustomThemeContext = React.createContext({ theme: defaultTheme })



export const CustomThemeProvider = ({ children, value }) => {
  return <CustomThemeContext.Provider value={value}>{children}</CustomThemeContext.Provider>
}