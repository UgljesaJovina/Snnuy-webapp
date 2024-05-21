import { useRecoilState } from "recoil";
import { authAtom } from "../atoms";
import { baseUrl } from "./GlobalVariables";

function useFetchWrapper() {
    const [auth] = useRecoilState(authAtom);

    return {
        get: request("GET"),
        post: request("POST"),
        put: request("PUT"),
        delete: request("DELETE"),
        patch: request("PATCH")
    };

    function request(method: string) {
        return async (req: TRequestType) => {
            const reqOptions: { method: string, headers: any, body?: string } = {
                method,
                headers: setAuth(req.reqAuth)
            };
            if (req.body) {
                reqOptions.headers["Content-Type"] = req.contentType ? req.contentType : "application/json"
                reqOptions.body = JSON.stringify(req.body);
            }
            return fetch(baseUrl + req.url, reqOptions).then(handleResponse);
        }
    }

    function setAuth(reqAuth: boolean | undefined) {
        if (!reqAuth) return { };

        const token = auth;
        
        return !token ? { } : { Authorization: `Bearer ${token}` };
    }

    async function handleResponse(response: Response) {
        if (response.status === 204) return Promise.resolve();

        return response.json().then(data => {
            if (!response.ok) {
                const error = data.message || response.statusText;
                return Promise.reject(error);
            }

            return Promise.resolve(data);
        });
    }
}

type TContentType = "application/json" | "text/plain" | "multipart/form-data"
type TRequestType = { url: string, body?: object, reqAuth?: boolean, contentType?: TContentType }
export { useFetchWrapper }