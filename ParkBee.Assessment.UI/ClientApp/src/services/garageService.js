import { apiPath, apiRoot } from "../config";
import { StateStore, Topics } from "../state";

class _GarageService {
    GetGarageDetails = _ => {
        let url = `${apiRoot}${apiPath.getGarageDetails}/${StateStore.retrieve(Topics.User)['garageId']}`
        let key = `bearer ${StateStore.retrieve(Topics.Details)['token']}`

        return new Promise((resolve, reject) => {
            fetch(url, {
                method: "get",
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': key
                },
            })
                .then((response) => {
                    if (response.status !== 200)
                        throw { status: response.status, message: response.message };
                    return response.json();
                })
                .then((json) => {
                    resolve(json);
                })
                .catch((error) => {
                    reject(error);
                });
        })
    }

    GetGarageStatus = id => {
        let url = `${apiRoot}${apiPath.getGarageStatus}/${id}`
        let key = `bearer ${StateStore.retrieve(Topics.Details)['token']}`

        return new Promise((resolve, reject) => {
            fetch(url, {
                method: "get",
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': key
                },
            })
                .then((response) => {
                    if (response.status !== 200)
                        throw { status: response.status, message: response.message };
                    return response.json();
                })
                .then((json) => {
                    resolve(json);
                })
                .catch((error) => {
                    reject(error);
                });
        })
    }

    CheckDoorStatus = args => {
        var url = `${apiRoot}${apiPath.getGarageDetails}/${args.garageId}/doors/${args.doorId}/status`;
        var key = `bearer ${StateStore.retrieve(Topics.Details)['token']}`

        var body = { stringParam: args.serial }

        return new Promise((resolve, reject) => {
            fetch(url, {
                method: "post",
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': key
                },
                body: JSON.stringify(body),
            })
                .then((response) => {
                    if (response.status !== 200)
                        throw { status: response.status, message: response.message };
                    return response.json();
                })
                .then((json) => {
                    resolve(json);
                })
                .catch((error) => {
                    reject(error);
                });
        })
    }

    ToggleGaragedoor = args => {
        var url = `${apiRoot}${apiPath.getGarageDetails}/${args.garageId}/doors/${args.doorId}/open`;
        var key = `bearer ${StateStore.retrieve(Topics.Details)['token']}`

        var body = { stringParam: args.serial, boolParam: args.isOpen }

        return new Promise((resolve, reject) => {
            fetch(url, {
                method: "post",
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': key
                },
                body: JSON.stringify(body),
            })
                .then((response) => {
                    if (response.status !== 200)
                        throw { status: response.status, message: response.message };
                    return response.json();
                })
                .then((json) => {
                    resolve(json);
                })
                .catch((error) => {
                    reject(error);
                });
        })
    }
}

export const GarageService = new _GarageService();