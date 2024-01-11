import React from 'react';
import { useLocation } from 'react-router-dom';
import PageTitle from './Typography/PageTitle';

const PageIndicator = ({ routes }) => {
  const location = useLocation();

  const getPageName = () => {
    const currentPath = location.pathname;
    const splicedPath = currentPath.split("/");
    const resourse = splicedPath[splicedPath.length - 1];
    const matchedRoute = routes.find(route => resourse.includes(route.resourse));
    return matchedRoute ? matchedRoute.name : 'Inicio';
  };

  return (
    <div>
      <PageTitle>{getPageName()}</PageTitle> 
      {/* <p>Estás en la página: </p> */}
    </div>
  );
};

export default PageIndicator