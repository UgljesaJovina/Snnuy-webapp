import React, { useEffect } from "react";
import { Outlet } from "react-router";
import { NavLink, useLocation } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useRecoilValue } from "recoil";
import { userAtom } from "../atoms";
import { useUserActions } from "../actions";
import Home from "../images/home.svg";
import Cards from "../images/cards.svg";
import DeckLib from "../images/deck-lib.svg";

const Sidebar: React.FC = () => {
    const user = useRecoilValue(userAtom);
    const userActions = useUserActions();
    const loc = useLocation();

    useEffect(() => {
        if (!user.username)
            userActions.getMyInfo().catch(err => console.log(err));
    }, []);

    return (
        <>
            <div className="sidebar">
                <NavLink to={"/home"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>
                    <img src={Home} />
                    Home
                </NavLink>
                <NavLink to={"/custom-cards"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>
                    <img src={Cards} />
                    Custom Cards
                </NavLink>
                <NavLink to={"/decks"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>
                    <img src={DeckLib} />
                    Deck Library
                </NavLink>
                <NavLink to={`/account?from=${loc.pathname}`} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>
                    <FontAwesomeIcon icon={["far", "user"]} />
                    {user.username ? user.username : "Login"}
                </NavLink>
            </div>
            <Outlet />
        </>
    );
}

export { Sidebar };