import React, { useEffect, useState } from 'react'
import Button from '@material-ui/core/Button';
import { AccountService, GarageService } from '../services';
import { GarageContainer } from '../components/garage';
import { StateStore, Topics } from '../state';
import './_style.layout.scss';

export default _ => {
    const handleClick = _ => {
        GarageService.GetGarageDetails().then(res => {
            if (res) {
                StateStore.publish(Topics.Garages, res, true)
            }
        })
    }

    return <div className='layout-container' >
        <Button variant='contained' color="primary" style={{ marginBottom: '1vh' }} onClick={_ => handleClick()} >Get Details</Button>
        <GarageContainer />

    </div >
}