
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
    path: '/app/tables',
    icon: 'TablesIcon',
    name: 'Tables',
    resourse: "tables"
  },
  {
    icon: 'PagesIcon',
    name: 'Pages',
    resourse: "pages",
    routes: [
      // submenu
      {
        path: '/login',
        name: 'Login',
        resourse: "login"
      },
      {
        path: '/create-account',
        name: 'Create account',
        resourse: "create-account"
      },
      {
        path: '/forgot-password',
        name: 'Forgot password',
        resourse: "forgot-password"
      },
      {
        path: '/app/404',
        name: '404',
        resourse: "404"
      },
      {
        path: '/app/blank',
        name: 'Blank',
        resourse: "blank"
      },
    ],
  },
]

export default routes
