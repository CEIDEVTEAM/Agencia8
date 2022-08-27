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

export default function Map(props) {
    const [coords, setCoords] = useState(props.coords)
    return (
        <MapContainer
            center={[18.467455, -69.931242]} zoom={14}
            style={{ height: props.height }}
        >
            <TileLayer attribution="React Películas"
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            {props.soloLectura ? null : <ClickMap setPunto={coords => {
                setCoords([coords]);
                props.manejarClickMapa(coords);
            }} />}

            {coords.map(coord => <Mark key={coord.lat + coord.lng}
                {...coord}
            />)}
        </MapContainer>
    )
}

Map.defaultProps = {
    height: '500px',
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
            {props.nombre ? <Popup>
                {props.nombre}
            </Popup> : null}
        </Marker>
    )
}

