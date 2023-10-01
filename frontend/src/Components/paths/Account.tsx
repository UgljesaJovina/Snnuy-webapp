import React from "react";
import { useRecoilState } from "recoil";
import { authAtom } from "../../Atoms";
import { Navigate } from "react-router";

const Account: React.FC = () => {

    const [auth] = useRecoilState(authAtom);

    if (!auth) return <Navigate to="/login" />

    return (
        <div>   
            
        </div>
    );
}

export { Account };