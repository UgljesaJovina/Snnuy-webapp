import React from "react";
import { TCustomCard } from "../types"
import { baseUrl } from "../utils/GlobalVariables";

const CustomCard: React.FC<{ card: TCustomCard }> = ({ card }) => {
    console.log(`${baseUrl}/public/CustomCards/${card.id}.png`)
    return (
        <div className="custom-card">
            <img className="custom-card-image" src={`${baseUrl}public/CustomCards/${card.id}.png`} alt="" />
            <div className="custom-card-info">
                
            </div>
        </div>
    );
}

export { CustomCard };