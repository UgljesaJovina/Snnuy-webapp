import React from "react";
import { TCard } from "../types"
import { CardRegions } from "../enums";

const Card: React.FC<{card: TCard & { count: number }}> = ({ card }) => {

    const cardStyles = {
        "--region-shade": `var(${getRegionStyle(CardRegions[getHighestCardRegion(card)])})`,
        "--card-image": `url(${card.cardImageLink})`, 
        backgroundImage: `url(${card.cardBackgroundLink})`
    } as React.CSSProperties;

    function getRegionStyle(reg: string) {
        return `--${reg.toLowerCase().replaceAll("_", "-")}`;
    }

    function getHighestCardRegion(card: TCard) {
        let max = CardRegions.None;
        for (let i = 1; i < CardRegions.Custom; i <<= 1) {
            if ((card.regions & i) !== 0 && max < i)
                max = i
        }
        return max;
    }

    return (
        <div className="card" style={cardStyles}>
            <div className="region-shade"></div>
            <div className="mana-cost"><span>{card.manaCost}</span></div>
            <span className="card-name">{card.cardName}</span>
            <div className="card-count"><span>{card.count}</span></div>
        </div>
    );
}

export { Card };