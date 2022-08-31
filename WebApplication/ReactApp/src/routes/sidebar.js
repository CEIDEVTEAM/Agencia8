
/**
 * ⚠ These are used just to render the Sidebar!
 * You can include any link here, local or external.
 *
 * If you're looking to actual Router routes, go to
 * `routes/index.js`
 */
const routes = [
  {
    path: '/app/dashboard', // the url
    icon: 'HomeIcon', // the component being exported from icons/index.js
    name: 'Dashboard',
    resourse: "dashboard" // name that appear in Sidebar
  },
  {
    path: '/app/forms',
    icon: 'FormsIcon',
    name: 'Forms',
    resourse: "forms"
  },
  {
    path: '/app/cards',
    icon: 'CardsIcon',
    name: 'Cards',
    resourse: "cards"
    
  },
  {
    path: '/app/charts',
    icon: 'ChartsIcon',
    name: 'Charts',
    resourse: "charts"
  },
  {
    path: '/app/buttons',
    icon: 'ButtonsIcon',
    name: 'Buttons',
    resourse: "buttons"
  },
  {
    path: '/app/modals',
    icon: 'ModalsIcon',
    name: 'Modals',
    resourse: "modals"
  },
  {
    path: '/app/dependent/ListDependent',
    icon: 'FortuneWheelIcon',
    name: 'Sub Agentes',
    resourse: "subagentes"
  },  
  {
    icon: 'FormsIcon',
    name: 'Aspirantes',
    resourse: "candidates",
    routes: [
      // submenu
      {
        path: '/app/candidates/NewCandidate',
        name: 'Registrar Aspirante',
        resourse: "NewCandidate",
        exact: true
      },
      {
        path: '/app/candidates/CandidatesManagment',
        name: 'Gestión de Aspirantes',
        resourse: "CandidatesManagment",        
      },
    ],
  },
  {
    icon: 'PeopleIcon',
    name: 'Usuarios',
    resourse: "users",
    routes: [
      // submenu
      {
        path: '/app/users/NewUser',
        name: 'Nuevo Usuario',
        resourse: "NewUser",
        exact: true
      },
      {
        path: '/app/users/ListUsers',
        name: 'Listado de Usuarios',
        resourse: "ListUsers",        
      },
    ],
  },
]

export default routes
