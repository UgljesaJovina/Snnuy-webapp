import { useRecoilState } from "recoil";
import { authAtom } from "../Atoms";
import Cookies from "universal-cookie";
import { useState } from "react";

export { useFetchWrapper }

function useFetchWrapper() {
    const [auth, setAuth] = useRecoilState(authAtom);
    const baseUrl = "http://localhost:5062/"

    return {
        get: request("GET"),
        post: request("POST"),
        put: request("PUT"),
        delete: request("DELETE"),
    };

    function request(method: string) {
        return (url: string, body?: object, reqAuth?: boolean, contentType?: TContentType) => {
            const reqOptions: { method: string, headers: any, body?: string } = {
                method,
                headers: setHeaders(reqAuth)
            };
            if (body) {
                reqOptions.headers["Content-Type"] = contentType ? contentType : "application/json"
                reqOptions.body = JSON.stringify(body);
            }
            return fetch(baseUrl + url, reqOptions).then(handleResponse);
        }
    }

    function setHeaders(reqAuth: boolean | undefined) {
        if (!reqAuth) return { };

        const token = auth?.token;
        
        return !token ? { } : { Autharization: `Bearer ${token}` };
    }

    function handleResponse(response: Response) {
        return response.json().then(data => {
            if (!response.ok) {
                const error = data.message || response.statusText;
                return Promise.reject(error);
            }

            return data;
        });
    }
}

type TContentType = "application/json"
