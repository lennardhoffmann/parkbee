import { Button } from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import { GarageService } from '../../services';
import './_style.garageDoorCard.scss';

export default props => {
    const [state, setState] = useState({
        status: null,
        open: null,
    })

    const [message, setMessage] = useState(null);
    const [isDisabled, disableButton] = useState(false);
    const [openDisabled, disableOpenButton] = useState(false);

    const { status, isOpen } = state;

    useEffect(_ => {
        console.log('PROPS', props.doorDetails.isOnline)
        console.log('STATE', status)
    })

    const handleIPCheck = _ => {
        disableButton(true)
        disableOpenButton(true)

        GarageService.CheckDoorStatus({ doorId: props.doorDetails.id, serial: props.doorDetails.serialnumber, garageId: props.doorDetails.garageId }).then(res => {
            if (res.success) {
                setMessage("The door is online")

                if (!status) {
                    setState({
                        status: true,
                        isOpen: isOpen
                    })
                }
            }
            else {
                setMessage("The door is offline")

                if (status) {
                    setState({
                        status: false,
                        isOpen: isOpen
                    })
                }
            }
        }).then(_ => {
            clearMessage()
        })
    }

    const toggleDoor = _ => {
        disableButton(true);
        disableOpenButton(true);

        GarageService.ToggleGaragedoor({ doorId: props.doorDetails.id, serial: props.doorDetails.serialnumber, garageId: props.doorDetails.garageId, isOpen: isOpen })
            .then(res => {
                console.log(res)

                if (res.success) {
                    setMessage(`Door successfully ${isOpen ? 'closed' : 'opened'}`)

                    setState({
                        status: true,
                        isOpen: isOpen ? false : true
                    })

                }
                else {
                    setMessage(`The door could not be ${isOpen ? 'closed' : 'opened'}`)

                    setState({
                        status: false,
                        isOpen: isOpen
                    })

                }
            })
            .then(_ => {
                clearMessage();
            })
    }

    const clearMessage = _ => {
        setTimeout(function () {
            setMessage(null)
        }, 1500);

        disableButton(false);
        disableOpenButton(false);
    }

    const checkStatus = _ => {
        if (status != null)
            return status;
        else return props.doorDetails.isOnline;
    }

    const checkIfOpen = _ => {
        if (isOpen != null)
            return isOpen;
        else return props.doorDetails.isOpen;
    }

    return <div className='door-container'>
        <label className='door-lbl'>ID: <strong>{props.doorDetails.serialnumber}</strong></label>
        <label className='door-lbl'>Open status: <strong>{isOpen || props.doorDetails.isOpen ? 'Open' : "Closed"}</strong></label>
        <div className='door-status-container'>
            <label style={{ fontSize: '1.5vh', marginRight: '2vw', verticalAlign: 'center' }}>Status</label>
            <div className='door-status-div' style={{ backgroundColor: checkStatus() ? 'green' : 'red' }}>{checkStatus() ? 'Online' : 'Offline'}</div>
        </div>
        {message && < label className='door-lbl' style={{ alignSelf: 'center', fontStyle: 'italic' }}><strong>{message}</strong></label>}
        <div className='door-btn-container'>
            {status && <Button color='primary' size='small' variant='contained' style={{ fontSize: '1.2vh' }} onClick={_ => toggleDoor()} disabled={openDisabled}>{checkIfOpen() ? 'Close Door' : 'Open Door'}</Button>}
            <Button size='small' color='secondary' variant='contained' style={{ marginLeft: status && '4vh', fontSize: '1.2vh' }} disabled={isDisabled} onClick={_ => handleIPCheck()}>Check Door Status</Button>
        </div>
    </div>
}