import React, { Component } from 'react'
import { GarageCard, GarageDoorCard } from '.';
import { StateStore, Topics } from '../../state';
import './_style.garageContainer.scss'

export class GarageContainer extends Component {
    constructor(props) {
        super(props);
        this.state = {
            garages: null
        }

        StateStore.subscribe(Topics.Garages, this)
    }

    componentWillUnmount = _ => {
        StateStore.unsubscribe(Topics.Garage, this)
    }

    render = _ => {
        return <div className='details-container'>
            {this.state.garages ?
                <div className='inner-container'>
                    <label className='hdr'>{`Welcome back ${StateStore.retrieve(Topics.User)['name']}`}</label>
                    <div className='main-block'>
                        <GarageCard details={this.state.garages} />
                        <label style={{ marginTop: '5vw' }}>Your Garage Doors:</label>
                        <div className='card-block'>
                            {this.state.garages.doors.map((g, i) => {
                                return <GarageDoorCard key={`door-${i}-${g.Id}`} doorDetails={g} />
                            })}
                        </div>
                    </div>
                </div>
                :
                <></>
            }</div>
    }
}