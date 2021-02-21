import React, { useEffect, useState } from 'react';
import { LayoutComponent } from './layout'
import { AccountService } from './services';
import { StateStore, Topics } from './state';


export default _ => {
    const [state, setState] = useState(null);

    useEffect(_ => {
        if (!StateStore.retrieve(Topics.Details))
            AccountService.GetJWTToken().then(res => {
                if (res != null) {
                    StateStore.publish(Topics.Details, res, true)
                    AccountService.DecodeJWT(JSON.parse(atob(res.token.split('.')[1])))
                }
            }).then(_ => {
                setState('state')
            })
    })


    return (
        state ? <LayoutComponent /> : <></>
    );

}
