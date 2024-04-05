import React from "react";
import { useRecoilValue } from "recoil";
import { userAtom } from "../atoms";
import { Navigate, useLocation } from "react-router";
import { useSearchParams } from "react-router-dom";

const Account: React.FC = () => {

    const user = useRecoilValue(userAtom);
    const [params] = useSearchParams();

    if (!user.id) return <Navigate replace to={`/login?from=${params.get("from")}`} />

    return (
        <div>   
            
        </div>
    );
}

export { Account };