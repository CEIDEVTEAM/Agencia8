import { lazy } from 'react'

// use lazy for better code splitting, a.k.a. load faster
const Dashboard = lazy(() => import('../pages/Dashboard'))
const Forms = lazy(() => import('../pages/Forms'))
const Cards = lazy(() => import('../pages/Cards'))
const Charts = lazy(() => import('../pages/Charts'))
const Buttons = lazy(() => import('../pages/Buttons'))
const Modals = lazy(() => import('../pages/Modals'))
const Tables = lazy(() => import('../pages/Tables'))
const Page404 = lazy(() => import('../pages/404'))
const Blank = lazy(() => import('../pages/Blank'))

//USUARIOS
const NewUser = lazy(() => import('../pages/users/NewUser'))
const ListUsers = lazy(() => import('../pages/users/ListUsers'))
const EditUser = lazy(() => import('../pages/users/EditUser'))

//CANDIDATOS
const CandidatesManagment = lazy(() => import('../pages/candidates/CandidatesManagment'))
const NewCandidate = lazy(() => import('../pages/candidates/NewCandidate'))

//SUBAGENTES
const ListDependent = lazy(() => import('../pages/Dependent/ListDependent'))
const ListExDependent = lazy(() => import('../pages/Dependent/ListExDependent'))
const ListExternalDependent = lazy(() => import('../pages/Dependent/ListExternalDependent'))
//PARAMETROS DECISION
const ListDecitionParams = lazy(() => import('../pages/config/ListDecitionParams'))

//Analisis
const Analisis = lazy(() => import('../pages/analisis/Analisis'))

//PROYECCIÓN
const ListParams = lazy(() => import('../pages/projection/ListParams'))
const NewParam = lazy(() => import('../pages/projection/NewParam'))
const Projection = lazy(() => import('../pages/projection/Projection'))
const ListConcepts = lazy(() => import('../pages/projection/ListConcepts'))


const routes = [
  {
    path: '/dashboard', 
    component: Dashboard, 
    isAdmin: false,
    resourse: "dashboard"
  },
  {
    path: '/forms',
    component: Forms,
    isAdmin: true,
    resourse: "forms"
  },
  {
    path: '/cards',
    component: Cards,
    isAdmin: true,
    resourse: "cards"
  },
  {
    path: '/charts',
    component: Charts,
    isAdmin: true,
    resourse: "charts"
    
  },
  {
    path: '/buttons',
    component: Buttons,
    isAdmin: true,
    resourse: "buttons"
  },
  {
    path: '/modals',
    component: Modals,
    isAdmin: true,
    resourse: "modals"
  },
  {
    path: '/tables',
    component: Tables,
    isAdmin: true,
    resourse: "tables"
  },
  {
    path: '/404',
    component: Page404,
    isAdmin: true,
    resourse: "404"
  },
  {
    path: '/blank',
    component: Blank,
    isAdmin: true,
    resourse: "blank"
  },
  {
    path: '/users/newUser',
    component: NewUser,
    isAdmin: true,
    resourse: "NewUser"
  },
  {
    path: '/users/ListUsers',
    component: ListUsers,
    isAdmin: true,
    resourse: "ListUsers"
  },
  {
    path: '/users/EditUser',
    component: EditUser,
    isAdmin: true,
    resourse: "EditUser"
  },
  {
    path: '/candidates/CandidatesManagment',
    component: CandidatesManagment,
    isAdmin: true,
    resourse: "CandidatesManagment"
  },
  {
    path: '/candidates/NewCandidate',
    component: NewCandidate,
    isAdmin: true,
    resourse: "NewCandidate"
  },
  {
    path: '/dependent/ListDependent',
    component: ListDependent,
    isAdmin: true,
    resourse: "ListDependent"
  },
  {
    path: '/config/ListDecitionParams',
    component: ListDecitionParams,
    isAdmin: true,
    resourse: "ListDependent"
  },
  {
    path: '/dependent/ListExDependent',
    component: ListExDependent,
    isAdmin: true,
    resourse: "ListExDependent"
  },
  {
    path: '/candidates/ListExternalDependent',
    component: ListExternalDependent,
    isAdmin: true,
    resourse: "ExternalDependent"
  },
  {
    path: '/analisis/Analisis',
    component: Analisis,
    isAdmin: true,
    resourse: "Analisis"
  },
  {
    path: '/analisis/Analisis',
    component: Analisis,
    isAdmin: true,
    resourse: "Analisis"
  },
  {
    path: '/projection/ListParams',
    component: ListParams,
    isAdmin: true,
    resourse: "ListParams"
  },
  {
    path: '/projection/NewParam',
    component: NewParam,
    isAdmin: true,
    resourse: "NewParam"
  },
  {
    path: '/projection/Projection',
    component: Projection,
    isAdmin: true,
    resourse: "ProjectionMensual"
  },
  {
    path: '/projection/ListConcepts',
    component: ListConcepts,
    isAdmin: true,
    resourse: "ListConcepts"
  },


  



  
]

export default routes
