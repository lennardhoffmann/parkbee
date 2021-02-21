import React, { useEffect, useState } from 'react'
import './_style.garageCard.scss';

export default props => {

    const [state, setState] = useState({
        name: null,
        id: null,
        zone: null,
        capacity: null,
        available: null,
        country: null
    })

    const { name, id, zone, capacity, available, country } = state

    useEffect(_ => {
        if (!name || name.length < 1) {
            setState({
                name: props.details.name,
                id: props.details.garageId,
                zone: props.details.zoneNumber,
                capacity: props.details.capacity,
                available: props.details.availableSpaces,
                country: props.details.countryCode
            })
        }
    })
    return <div className='card-container'>
        Your garage details:
         <label className='garage-lbl'>Name: <strong>{name}</strong></label>
        <label className='garage-lbl'>Garage Id: <strong>{id}</strong></label>
        <label className='garage-lbl'>Capacity: <strong>{capacity}</strong></label>
        <label className='garage-lbl'>Available Spaces: <strong>{available}</strong></label>
        <label className='garage-lbl'>Zone Nr: <strong>{zone}</strong></label>
        <label className='garage-lbl'>Country: <strong>{country}</strong></label>
    </div>
}