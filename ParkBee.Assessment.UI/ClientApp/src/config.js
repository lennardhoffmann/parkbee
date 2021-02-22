const apiRoot = 'http://localhost:51312/';

const apiPath = {
    getToken: 'token',
    getGarageDetails: 'api/garages',
    getGarageStatus: 'api/garages/status'
}

const dummyUser = {
    "username": "test",
    "password": "test"
};

const JWTDecoding = {
    name: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name',
    email: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress',
    garageId: 'http://schemas.parkbee.com/ws/identity/claims/garageid'
}

export { apiRoot, dummyUser, apiPath, JWTDecoding }