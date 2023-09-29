import React from "react";
import { Outlet } from "react-router";
import { NavLink } from "react-router-dom";
import SnnuyLogo from "../../Images/SnnuyLogo.png"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { TNavigations } from "../../types";

const Header: React.FC<{ currentlyActive: TNavigations }> = ({ currentlyActive }) => {
    return (
        <>
            <div className="header">   
                <img src={SnnuyLogo} height="100%" className="header-logo" />
                <NavLink to={"/home"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>Home</NavLink>
                <NavLink to={"/custom-cards"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}> Custom Cards</NavLink>
                <NavLink to={"/decks"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>Decks</NavLink>
                <NavLink to={"/account"} className={({ isActive }) => `hyperlink ${isActive ? "current" : ""}`}>
                    <FontAwesomeIcon icon={["far", "user"]} />
                </NavLink>
            </div>
            <Outlet />
        </>
    );
}

export { Header };