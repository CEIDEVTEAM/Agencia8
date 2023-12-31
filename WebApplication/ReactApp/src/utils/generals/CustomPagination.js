import React, { useEffect, useState } from "react";

export default function CustomPaginacion(props) {
    const [listadoLinks, setListadoLinks] = useState([]);
    useEffect(() => {
        const paginaAnteriorHabilitada = props.paginaActual !== 1;
        const paginaAnterior = props.paginaActual - 1;
        const links = [];

        links.push({
            texto: 'Anterior',
            habilitado: paginaAnteriorHabilitada,
            pagina: paginaAnterior,
            activo: false
        });

        for (let i = 1; i <= props.cantidadTotalDePaginas; i++) {
            if (i >= props.paginaActual - props.radio && i <= props.paginaActual + props.radio) {
                links.push({
                    texto: `${i}`,
                    activo: props.paginaActual === i,
                    habilitado: true, pagina: i
                })
            }
        }

        const paginaSiguienteHabilitada = props.paginaActual !== props.cantidadTotalDePaginas && props.cantidadTotalDePaginas > 0;
        const paginaSiguiente = props.paginaActual + 1;
        links.push({
            texto: 'Siguiente',
            pagina: paginaSiguiente,
            habilitado: paginaSiguienteHabilitada,
            activo: false
        });

        setListadoLinks(links);
    }, [props.paginaActual, props.cantidadTotalDePaginas, props.radio])

    function obtenerClase(link){
        if (link.activo){
            return "active pointer"
        }

        if (!link.habilitado){
            return "disabled";
        }

        return "pointer"
    }

    function seleccionarPagina(link){
        if (link.pagina === props.paginaActual){
            return;
        }

        if (!link.habilitado){
            return;
        }

        props.onChange(link.pagina);
    }

    return (
        <div className="flex mt-2 sm:mt-auto sm:justify-end"> 
        <nav>
            <ul className="inline-flex items-center">
                {listadoLinks.map(link => <li key={link.texto}
                 onClick={() => seleccionarPagina(link)}
                 className={
                    link.pagina === props.paginaActual
                      ? "relative z-10 inline-flex items-center bg-indigo-600 px-4 py-2 text-sm font-semibold text-white focus:z-20 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                      : "bg-white border-gray-300 text-gray-500 hover:bg-blue-200 relative inline-flex items-center px-4 py-2 border text-sm font-medium"
                  }
                >
                    <span className="page-link">{link.texto}</span>
                </li>)}
            </ul>
        </nav>
        </div>
    )
}



CustomPaginacion.defaultProps = {
    radio: 1
}