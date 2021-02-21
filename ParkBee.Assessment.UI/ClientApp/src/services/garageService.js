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

    CheckDoorStatus = args => {
        let url = `${apiRoot}${apiPath.getGarageDetails}/${args.garageId}/doors/${args.doorId}/status`;
        let key = `bearer ${StateStore.retrieve(Topics.Details)['token']}`

        let body = { doorSerial: args.serial }

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