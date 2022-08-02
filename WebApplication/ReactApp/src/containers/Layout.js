import React, { useState, useContext, Suspense, useEffect, lazy } from 'react'
import { Switch, Route, Redirect, useLocation, useHistory} from 'react-router-dom'
import routes from '../routes'
import { obtenerClaims , obtenerToken } from '../utils/auth/manejadorJWT';
import AuthContext from '../context/AuthContext'

import Sidebar from '../components/Sidebar'
import Header from '../components/Header'
import Main from '../containers/Main'
import ThemedSuspense from '../components/ThemedSuspense'
import { SidebarContext } from '../context/SidebarContext'

const Page404 = lazy(() => import('../pages/404'))

function Layout() {
  const { isSidebarOpen, closeSidebar } = useContext(SidebarContext)
  let location = useLocation()
  const history = useHistory();
  useEffect(() => {
    closeSidebar()
  }, [location])

  const [claims, setClaims] = useState([]);

  useEffect(() => {
    let token = obtenerToken()
    if(token === undefined || token === null){
      history.push("/")
    }else{
      setClaims(obtenerClaims());
    }
  }, [])

  function actualizar(claims) {
    setClaims(claims);
  } 

  function existResourse(resourse) {
        
    return claims.findIndex(claim => claim.valor === resourse) > -1;
  }

  return (
    <div
      className={`flex h-screen bg-gray-50 dark:bg-gray-900 ${isSidebarOpen && 'overflow-hidden'}`}
    >
      <AuthContext.Provider value={{ claims, actualizar }}>
      <Sidebar />

      <div className="flex flex-col flex-1 w-full">
        <Header />
        <Main>
          <Suspense fallback={<ThemedSuspense />}>
            <Switch>
              {routes.map(route =>
                <Route key={route.path} path={`/app${route.path}`}
                  exact={true}>
                  {!existResourse(route.resourse) ? <>
                    No tiene permiso para acceder a este componente
                    </> : <route.component/>}
                </Route>)}
            </Switch>
          </Suspense>
        </Main>
      </div>
      </AuthContext.Provider>
    </div>
  )
}

export default Layout
