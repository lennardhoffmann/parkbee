
import { Button } from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import { GarageService } from '../../services';
import './_style.garageDoorCard.scss';

export default props => {
    const [state, setState] = useState({
        serial: null,
        status: null,
        open: null,
        id: null
    })

    const [message, setMessage] = useState(null);
    const [isDisabled, disableButton] = useState(false);

    const { serial, id, status, open } = state;

    useEffect(_ => {
        if (!serial || serial.length < 1)
            setState({
                serial: props.doorDetails.serialnumber,
                status: props.doorDetails.isOnline,
                open: props.doorDetails.isOpen,
                id: props.doorDetails.id
            })
    })

    const handleIPCheck = _ => {
        disableButton(true)
        GarageService.CheckDoorStatus({ doorId: id, serial: serial, garageId: props.doorDetails.garageId }).then(res => {
            if (res.success) {
                setMessage("The door is online")

                if (!status) {
                    setState({
                        serial: serial,
                        status: true,
                        open: open,
                        id: id
                    })
                }
            }
            else {
                setMessage("The door is offline")

                if (status) {
                    setState({
                        serial: serial,
                        status: false,
                        open: open,
                        id: id
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
        <label className='door-lbl'>Serial: <strong>{serial}</strong></label>
        <label className='door-lbl'>Door ID: <strong>{id}</strong></label>
        <label className='door-lbl'>Open status: <strong>{open ? 'open' : "Closed"}</strong></label>
        <div className='door-status-container'>
            <label style={{ fontSize: '1.5vh', marginRight: '2vw', verticalAlign: 'center' }}>Status</label>
            <div className='door-status-div' style={{ backgroundColor: status ? 'green' : 'red' }}>{status ? 'Online' : 'Offline'}</div>
        </div>
        {message && < label className='door-lbl' style={{ alignSelf: 'center', fontStyle: 'italic' }}><strong>{message}</strong></label>}
        <div className='door-btn-container'>
            {status && <Button color='primary' size='small' variant='contained' style={{ fontSize: '1.2vh' }}>Open Door</Button>}
            <Button size='small' color='secondary' variant='contained' style={{ marginLeft: status && '4vh', fontSize: '1.2vh' }} disabled={isDisabled} onClick={_ => handleIPCheck()}>Check door IP</Button>
        </div>
    </div>
}