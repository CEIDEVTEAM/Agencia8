import React, { useState } from 'react';
import { MapContainer, Marker, Popup, TileLayer, useMapEvent } from "react-leaflet"
import L from 'leaflet'
import icon from 'leaflet/dist/images/marker-icon.png'
import iconShadow from 'leaflet/dist/images/marker-shadow.png'
import 'leaflet/dist/leaflet.css';


let DefaultIcon = L.icon({
    iconUrl: icon,
    shadowUrl: iconShadow,
    iconAnchor: [16, 37]
});
L.Marker.prototype.options.icon = DefaultIcon;
const LeafIcon = L.Icon.extend({
    options: {}
});
const greenIcon = new LeafIcon({
    shadowUrl: iconShadow,
    iconAnchor: [16, 37],
    iconUrl:
        "https://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|2ecc71&chf=a,s,ee00FFFF"
});


export default function Map(props) {
    const [coords, setCoords] = useState(props.coordenadas)
    const [coordsCol, setCoordsCol] = useState(props.colection)
    return (
        <MapContainer
            center={[-34.910051, -54.953425]} zoom={12}
            style={{ height: "350px" }}
            fullscreenControl={true} 

        >
            <TileLayer attribution="Agencia 8"
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            {props.soloLectura ? null : <ClickMap setPunto={coords => {
                setCoords([coords]);
                props.manejarClickMapa(coords);
            }} />}

            {coords.map(coord => <Mark key={coord.lat + coord.lng}
                {...coord}
            />)}

            {props.colection ? props.colection.map(coord => <MarkOthers key={coord.latitude + coord.longitude}
                {...coord}
            />) : null}

        </MapContainer>
    )
}

Map.defaultProps = {
    height: '600px',
    soloLectura: false,
    manejarClickMapa: () => { }
}
function ClickMap(props) {
    useMapEvent('click', e => {
        props.setPunto({ lat: e.latlng.lat, lng: e.latlng.lng })
    })
    return null;
}
function Mark(props) {
    return (
        <Marker position={[props.lat, props.lng]}>
            {props.number ? <Popup>
                {props.number}
            </Popup> : null}
        </Marker>
    )
}
function MarkOthers(props) {
    return (
        <Marker icon={greenIcon} position={[props.latitude, props.longitude]}>
            {props.number ? <Popup>
                {props.number}
            </Popup> : null}
        </Marker>
    )
}
// function PopulateMarkers(coordsCol) {
//     return (
//         coordsCol.map(position =>
//             <Marker position={[position.lat, position.lng]}>
//                 {position.number ? <Popup>{position.number}</Popup> : null}
//             </Marker>)
//     )
// }

