import React from "react";
import { useRecoilState } from "recoil";
import { userAtom } from "../atoms";
import { Navigate, useNavigate } from "react-router";
import { useSearchParams } from "react-router-dom";
import Cookies from "universal-cookie";
import { defaultUser } from "../types";

const Account: React.FC = () => {

    const [user, setUser] = useRecoilState(userAtom);
    const [params] = useSearchParams();
    const navigate = useNavigate();

    if (!user.id) return <Navigate replace to={`/login?from=${params.get("from") ?? "account"}`} />
    else params.delete("from");

    return (
        <div>   
            <button onClick={e => {
                const cookies = new Cookies();
                cookies.remove("auth");
                setUser(defaultUser);
                navigate("/login?from=account");
            }}>
                LogOut</button>
        </div>
    );
}

export { Account };