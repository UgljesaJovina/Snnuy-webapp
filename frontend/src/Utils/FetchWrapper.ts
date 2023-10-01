import { useRecoilState } from "recoil";
import { authAtom } from "../Atoms";
import { baseUrl } from "./GlobalVariables";

function useFetchWrapper() {
    const [auth] = useRecoilState(authAtom);

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

        const token = auth;
        
        return !token ? { } : { Autharization: `Bearer ${token}` };
    }

    function handleResponse(response: Response) {
        return response.json().then(data => {
            if (response.status !== 200) {
                const error = data.message || response.statusText;
                return Promise.reject(error);
            }

            return data;
        });
    }
}

type TContentType = "application/json"
export { useFetchWrapper }