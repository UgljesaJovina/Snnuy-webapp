import React from "react";
import { useRecoilValue } from "recoil";
import { userAtom } from "../atoms";
import { Navigate, useLocation } from "react-router";
import { useSearchParams } from "react-router-dom";
import Cookies from "universal-cookie";

const Account: React.FC = () => {

    const user = useRecoilValue(userAtom);
    const [params] = useSearchParams();

    if (!user.id) return <Navigate replace to={`/login?from=${params.get("from") ?? "account"}`} />
    else params.delete("from");

    return (
        <div>   
            <button onClick={e => {
                const cookies = new Cookies();
                cookies.remove("auth")
            }}>
                LogOut</button>
        </div>
    );
}

export { Account };