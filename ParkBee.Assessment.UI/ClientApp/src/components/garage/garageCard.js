import React, { useEffect, useState } from 'react'
import './_style.garageCard.scss';
import { Button } from '@material-ui/core';
import { GarageService } from '../../services';
import { StateStore, Topics } from '../../state';

export default props => {

    const [state, setState] = useState({
        name: null,
        id: null,
        zone: null,
        capacity: null,
        available: null,
        country: null
    })

    const [isDisabled, disableButton] = useState(false);

    const { name, id, zone, capacity, available, country } = state

    useEffect(_ => {
        console.log(props)
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

    const handleStatusCheck = _ => {
        disableButton(true);

        GarageService.GetGarageStatus(props.details.id).then(res => {
            if (res)
                StateStore.publish(Topics.Garages, res, true)
        })
            .then(_ => {
                setTimeout(function () {
                    disableButton(false)
                }, 1000);
            })

    }

    return <div className='card-container'>
        Your garage details:
         <label className='garage-lbl'>Name: <strong>{name}</strong></label>
        <label className='garage-lbl'>Garage Id: <strong>{id}</strong></label>
        <label className='garage-lbl'>Capacity: <strong>{capacity}</strong></label>
        <label className='garage-lbl'>Available Spaces: <strong>{available}</strong></label>
        <label className='garage-lbl'>Zone Nr: <strong>{zone}</strong></label>
        <label className='garage-lbl'>Country: <strong>{country}</strong></label>
        <Button size='small' color='secondary' variant='contained' style={{ fontSize: '1.2vh', width: '20%' }} disabled={isDisabled} onClick={_ => handleStatusCheck()}>Check Garage Status</Button>
    </div>
}