import React, { useEffect } from "react";
import { Outlet } from "react-router";
import { NavLink } from "react-router-dom";
import SnnuyLogo from "../Images/SnnuyLogo.png"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useRecoilValue } from "recoil";
import { userAtom } from "../Atoms";
import { useUserActions } from "../Actions";

const Header: React.FC = () => {
    const user = useRecoilValue(userAtom);
    const userActions = useUserActions();

    useEffect(() => {
        if (!user.username)
            userActions.getMyInfo().catch(err => alert(err));
    }, []);
    
    return (
        <>
            <div className="header">   
                <img src={SnnuyLogo} height="100%" className="header-logo" alt="card of the day" />
                <NavLink to={"/home"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>Home</NavLink>
                <NavLink to={"/custom-cards"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}> Custom Cards</NavLink>
                <NavLink to={"/decks"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>Decks</NavLink>
                <NavLink to={"/account"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>
                    { user.username ? user.username : <FontAwesomeIcon icon={["far", "user"]} /> }
                </NavLink>
            </div>
            <Outlet />
        </>
    );
}

export { Header };