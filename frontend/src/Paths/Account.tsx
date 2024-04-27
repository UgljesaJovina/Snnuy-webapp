import React from "react";
import { useRecoilState, useSetRecoilState } from "recoil";
import { authAtom, userAtom } from "../atoms";
import { Navigate, useNavigate } from "react-router";
import { useSearchParams } from "react-router-dom";
import Cookies from "universal-cookie";
import { defaultUser } from "../types";

const Account: React.FC = () => {

    const [user, setUser] = useRecoilState(userAtom);
    const setAuth = useSetRecoilState(authAtom);
    const [params] = useSearchParams();
    const navigate = useNavigate();

    if (!user.id) return <Navigate replace to={`/login?from=account`} />

    return (
        <div>
            <button onClick={e => {
                const cookies = new Cookies();
                cookies.remove("auth");
                setAuth("");
                setUser(defaultUser);
                navigate("/login?from=account");
            }}>
                LogOut</button>
        </div>
    );
}

export { Account };