import { useRecoilState } from "recoil";
import { authAtom, userAtom } from "../Atoms";
import { useFetchWrapper } from "../Utils/FetchWrapper";
import { UserPermissions } from "../enums";
import { useRef } from "react";
import Cookies from "universal-cookie";

function useUserActions() {
    const baseUrl = "user/"
    const [user, setUser] = useRecoilState(userAtom);
    const [auth, setAuth] = useRecoilState(authAtom);
    const fwrapper = useFetchWrapper();
    const cookies = useRef(new Cookies());

    return {
        register,
        login,
        checkForPermission,
        getById
    }

    async function register(username: string, password: string) {
        return fwrapper.post(baseUrl + "register", { username, password }).then(data => {
            setAuth(data.token);
            cookies.current.set("auth", data.token);
            setUser(curr => ({ ...curr, id: data.id, username: data.username, permissions: data.permissions }));
            return data;
        });
    }

    async function login(username: string, password: string) {
        return fwrapper.post(baseUrl + "login", { username, password }).then(data => {
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
        
    }
}

export { useUserActions }