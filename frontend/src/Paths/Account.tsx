import React from "react";
import { useRecoilValue } from "recoil";
import { userAtom } from "../atoms";
import { Navigate } from "react-router";
import { useSearchParams } from "react-router-dom";

const Account: React.FC = () => {

    const user = useRecoilValue(userAtom);
    const [params] = useSearchParams();

    if (!user.username) return <Navigate replace to={`/login?from=${params.get("from")}`} />

    return (
        <div>   
            
        </div>
    );
}

export { Account };