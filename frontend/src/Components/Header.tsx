import React from "react";
import { Outlet } from "react-router";
import { Link } from "react-router-dom";
import SnnuyLogo from "../Images/SnnuyLogo.png"

const Header: React.FC = () => {
    return (
        <>
            <div className="header">   
                <img src={SnnuyLogo} height="100%" />
                <Link to={"/home"} className="hyperlink">Home</Link>
                <Link to={"/custom-cards"} className="hyperlink"> Custom Cards</Link>
                <Link to={"/decks"} className="hyperlink">Decks</Link>
            </div>
            <Outlet />
        </>
    );
}

export { Header };