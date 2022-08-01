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


/**
 * ⚠ These are internal routes!
 * They will be rendered inside the app, using the default `containers/Layout`.
 * If you want to add a route to, let's say, a landing page, you should add
 * it to the `App`'s router, exactly like `Login`, `CreateAccount` and other pages
 * are routed.
 *
 * If you're looking for the links rendered in the SidebarContent, go to
 * `routes/sidebar.js`
 */
const routes = [
  {
    path: '/dashboard', // the url
    component: Dashboard, // view rendered
    isAdmin: false
  },
  {
    path: '/forms',
    component: Forms,
    isAdmin: true
  },
  {
    path: '/cards',
    component: Cards,
    isAdmin: true
  },
  {
    path: '/charts',
    component: Charts,
    isAdmin: true
  },
  {
    path: '/buttons',
    component: Buttons,
    isAdmin: true
  },
  {
    path: '/modals',
    component: Modals,
    isAdmin: true
  },
  {
    path: '/tables',
    component: Tables,
    isAdmin: true
  },
  {
    path: '/404',
    component: Page404,
    isAdmin: true
  },
  {
    path: '/blank',
    component: Blank,
    isAdmin: true
  },
  {
    path: '/users/newUser',
    component: NewUser,
    isAdmin: true
  },

  
]

export default routes
