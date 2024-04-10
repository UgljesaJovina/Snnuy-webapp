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
        return (req: TRequestType) => {
            const reqOptions: { method: string, headers: any, body?: string } = {
                method,
                headers: setHeaders(req.reqAuth)
            };
            if (req.body) {
                reqOptions.headers["Content-Type"] = req.contentType ? req.contentType : "application/json"
                reqOptions.body = JSON.stringify(req.body);
            }
            return fetch(baseUrl + req.url, reqOptions).then(handleResponse);
        }
    }

    function setHeaders(reqAuth: boolean | undefined) {
        if (!reqAuth) return { };

        const token = auth;
        
        return !token ? { } : { Authorization: `Bearer ${token}` };
    }

    function handleResponse(response: Response) {
        return response.text().then(data => {
            let json;
            try {
                json = JSON.parse(data);
            } catch {
                return "";
            }

            if (response.status !== 200) {
                const error = json.message || response.statusText;
                return Promise.reject(error);
            }

            return json;
        });
    }
}

type TContentType = "application/json" | "text/plain" | "multipart/form-data"
type TRequestType = { url: string, body?: object, reqAuth?: boolean, contentType?: TContentType }
export { useFetchWrapper }