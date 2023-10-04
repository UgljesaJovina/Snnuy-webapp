import React from "react";
import { useRecoilState, useRecoilValue } from "recoil";
import { authAtom } from "../../Atoms";
import { Navigate } from "react-router";

const Account: React.FC = () => {

    const auth = useRecoilValue(authAtom);

    console.log(auth);

    if (!auth) return <Navigate to="/login" />

    return (
        <div>   
            
        </div>
    );
}

export { Account };