import React from 'react'
import RoundIcon from '../../../components/RoundIcon'
import InfoCard from '../../../components/Cards/InfoCard'
import { AcceptedIcon, DeniedIcon } from '../../../icons'
import Map from '../form-groups/Map'
//import FormMap from '../form-groups/FormMap'

const SupportCard = (props) => {

    function transformarCoordenada() {
        if (props.data.shopCoordinates.latitude === undefined || props.data.shopCoordinates.latitude === null || props.props.longitude === undefined || props.props.longitude === null) {
            return [];
        }
        const respuesta = {
            lat: props.props.latitude,
            lng: props.props.longitude
        }
        return [respuesta];
    }
    return (
        <div><p className="mb-4 font-semibold">Barrio: {props.data.neighborhood} </p>
            <p className="mb-4 font-semibold">Potencial del barrio: {props.data.neighborhoodPotential}  </p>

            {props.data.recomendedDecision === "Se recomienda" ?
                <InfoCard title="Se recomienda aceptar la solicitud">
                    <RoundIcon
                        icon={AcceptedIcon}
                        iconColorClass="text-white-500 dark:text-white-100"
                        bgColorClass="bg-black-100 dark:bg-white-500"
                        className="mr-4"
                    />

                </InfoCard> :
                <InfoCard title="Se recomienda declinar la solicitud">
                    <RoundIcon
                        icon={DeniedIcon}
                        iconColorClass="text-white-500 dark:text-white-100"
                        bgColorClass="bg-white-100 dark:bg-white-500"
                        className="mr-4"
                    />

                </InfoCard>
            }
            <p className="mb-4 font-semibold">Descripción: {props.data.description}  </p>
            <p className="mb-4 font-semibold">Comercios de Agencia 8: {props.data.agencyShops}  </p>
            <p className="mb-4 font-semibold">Comercios de otras agencias: {props.data.externalAgencyShops}  </p>
            <Map soloLectura={true} colection={props.data.shopCoordinates} coordenadas={transformarCoordenada()} />
        </div>
    )
}

export default SupportCard