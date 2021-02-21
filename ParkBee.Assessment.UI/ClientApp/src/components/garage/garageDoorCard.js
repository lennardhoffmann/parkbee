import { Button } from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import { GarageService } from '../../services';
import './_style.garageDoorCard.scss';

export default props => {
    console.log(props.doorDetails.isOnline)
    const [state, setState] = useState({
        status: null,
        open: null,

    })

    const [message, setMessage] = useState(null);
    const [isDisabled, disableButton] = useState(false);

    const { status, open } = state;

    useEffect(_ => {
        if (props.doorDetails.isOnline != status)
            setState({
                status: props.doorDetails.isOnline,
                open: props.doorDetails.isOpen,
            })
    })

    const handleIPCheck = _ => {
        disableButton(true)
        GarageService.CheckDoorStatus({ doorId: props.doorDetails.id, serial: props.doorDetails.serialnumber, garageId: props.doorDetails.garageId }).then(res => {
            if (res.success) {
                setMessage("The door is online")

                if (!status) {
                    setState({
                        status: true,
                        open: open
                    })
                }
            }
            else {
                setMessage("The door is offline")

                if (status) {
                    setState({
                        status: false,
                        open: open
                    })
                }
            }
        }).then(_ => {
            setTimeout(function () {
                setMessage(null)
            }, 3000);
        }).then(_ => {
            disableButton(false)
        })
    }

    return <div className='door-container'>
        <label className='door-lbl'>ID: <strong>{props.doorDetails.serialnumber}</strong></label>
        <label className='door-lbl'>Open status: <strong>{open ? 'open' : "Closed"}</strong></label>
        <div className='door-status-container'>
            <label style={{ fontSize: '1.5vh', marginRight: '2vw', verticalAlign: 'center' }}>Status</label>
            <div className='door-status-div' style={{ backgroundColor: status ? 'green' : 'red' }}>{status ? 'Online' : 'Offline'}</div>
        </div>
        {message && < label className='door-lbl' style={{ alignSelf: 'center', fontStyle: 'italic' }}><strong>{message}</strong></label>}
        <div className='door-btn-container'>
            {status && <Button color='primary' size='small' variant='contained' style={{ fontSize: '1.2vh' }}>Open Door</Button>}
            <Button size='small' color='secondary' variant='contained' style={{ marginLeft: status && '4vh', fontSize: '1.2vh' }} disabled={isDisabled} onClick={_ => handleIPCheck()}>Check Door Status</Button>
        </div>
    </div>
}