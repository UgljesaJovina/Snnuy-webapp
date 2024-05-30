import React, { useState } from "react";
import { Outlet } from "react-router";
import { NavLink, useLocation } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowLeft } from "@fortawesome/free-solid-svg-icons";
import { useRecoilValue } from "recoil";
import { userAtom } from "../atoms";
import Home from "../images/home.svg";
import Cards from "../images/cards.svg";
import DeckLib from "../images/deck-lib.svg";
import { faUser } from "@fortawesome/free-regular-svg-icons";

const Sidebar: React.FC = () => {
    const [collapsed, setCollapsed] = useState(false);
    const user = useRecoilValue(userAtom);
    const loc = useLocation();

    return (
        <>
            <div className={`sidebar ${collapsed ? "collapsed" : ""}`}>
                <NavLink to={"/home"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>
                    <img src={Home} />
                    <p className="hyperlink-text">Home</p>
                </NavLink>
                <NavLink to={"/custom-cards"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>
                    <img src={Cards} />
                    <p className="hyperlink-text">Custom Cards</p>
                </NavLink>
                <NavLink to={"/decks"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>
                    <img src={DeckLib} />
                    <p className="hyperlink-text">Deck Library</p>
                </NavLink>
                <NavLink to={user.username ? `/account` : `/login?from=${loc.pathname}`} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>
                    <FontAwesomeIcon icon={faUser} />
                    <p className="hyperlink-text">{user.username ? user.username : "Login"}</p>
                </NavLink>
                <button className="collapse-button" onClick={() => setCollapsed(cur => !cur)}>
                    <FontAwesomeIcon icon={faArrowLeft} style={{marginBottom: "0"}} className="collapse-icon" />
                    <p className="hyperlink-text">Collapse</p>
                </button>
            </div>
            <Outlet />
        </>
    );
}

export { Sidebar };