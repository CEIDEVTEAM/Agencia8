import React, { useState } from 'react';

import Breadcrumb from './Breadcrumb';

function BreadCrumbContainer() {
  const [crumbs, setCrumbs] = useState(['Home', 'Category', 'Sub Category']);

  const selected = crumb => {
    console.log(crumb);
  }

  return (
    <>
      <Breadcrumb crumbs={ crumbs } selected={ selected }  />
    </>
  );
}

export default BreadCrumbContainer;