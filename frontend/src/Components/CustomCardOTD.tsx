import React from "react";
import { TCustomCardOTD } from "../types";
import { baseUrl } from "../utils/GlobalVariables";
import { Link } from "react-router-dom";

const CustomCardOTD: React.FC<{ card: TCustomCardOTD }> = ({ card }) => {
    return (
        <div className="customCard">
            <img src={baseUrl + "public/customcards/" + card.card.id + ".png"} width="100%" alt="" />
            <div className="customCardData">
                <Link to={`/account/${card.card.owner.id}`} >{card.card.owner.username}</Link>
                <section>{/*card.card.numberOfLikes*/}</section>
            </div>
        </div>
    );
}

export { CustomCardOTD };