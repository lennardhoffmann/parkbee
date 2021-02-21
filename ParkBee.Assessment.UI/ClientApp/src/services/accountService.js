import { apiPath, apiRoot, dummyUser, JWTDecoding } from "../config";
import { StateStore, Topics } from "../state";



class _AccountService {
    DecodeJWT = encoded => {
        let user = {
            garageId: encoded[JWTDecoding.garageId],
            emailAddress: encoded[JWTDecoding.email],
            name: encoded[JWTDecoding.name]
        }

        if (user) {
            StateStore.publish(Topics.User, user, true)
        }
    }

    GetJWTToken() {
        let url = `${apiRoot}${apiPath.getToken}`;

        return new Promise((resolve, reject) => {
            fetch(url, {
                method: "post",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(dummyUser),
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

export const AccountService = new _AccountService();