import { useRecoilState, useSetRecoilState } from "recoil";
import { authAtom, userAtom } from "../atoms";
import { useFetchWrapper } from "../utils/FetchWrapper";
import { UserPermissions } from "../enums";
import { useRef } from "react";
import Cookies from "universal-cookie";

function useUserActions() {
    const baseUrl = "user/";
    const fwrapper = useFetchWrapper();
    const [user, setUser] = useRecoilState(userAtom);
    const setAuth = useSetRecoilState(authAtom);
    const cookies = useRef(new Cookies());

    return {
        register,
        login,
        checkForPermission,
        getById,
        getMyInfo
    }

    async function register(username: string, password: string) {
        return fwrapper.post({ url: baseUrl + "register", body: { username, password }}).then(data => {
            setAuth(data.token);
            cookies.current.set("auth", data.token);
            setUser(curr => ({ ...curr, id: data.id, username: data.username, permissions: data.permissions }));
            return data;
        });
    }

    async function login(username: string, password: string) {
        return fwrapper.post({ url: baseUrl + "login", body: { username, password } }).then(data => {
            setAuth(data.token);
            cookies.current.set("auth", data.token);
            setUser({ id: data.id, username: data.username, permissions: data.permissions, 
                likedCards: data.likedCards, likedDecks: data.likedDecks, ownedCards: data.ownedCards, ownedDecks: data.ownedDecks });
                return data;
        });
    }

    function checkForPermission(requiredPermission: UserPermissions) {
        return (user.permissions | requiredPermission) !== 0;
    }

    async function getById(id: string) {
        return fwrapper.get({ url: baseUrl + `get-by-id/${id}` });
    }

    async function getMyInfo() {
        return fwrapper.get({ url: baseUrl + "get-my-info", reqAuth: true }).then(data => setUser(data));
    }
}

export { useUserActions }