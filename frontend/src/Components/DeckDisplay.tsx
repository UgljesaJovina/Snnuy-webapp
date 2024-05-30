import React, { useEffect, useState } from "react";
import { defaultDetailedDeck, TCard } from "../types";
import { useDeckActions } from "../actions";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSpinner } from "@fortawesome/free-solid-svg-icons";
import { CardRarity, CardRegions, CardTypes } from "../enums";
import { RegionCountDetailed } from "./RegionCountDetailed";
import { Card } from "./Card";
import { baseUrl } from "../utils/GlobalVariables";

export const DeckDisplay: React.FC<{ deckId: string }> = ({ deckId }) => {
    const [deck, setDeck] = useState(defaultDetailedDeck);
    const [loading, setLoading] = useState(true);
    const deckActions = useDeckActions();

    function getFeaturedCard() {
        if (deck.champions.length > 0) {
            return deck.champions.reduce((c1, c2) => c1.manaCost > c2.manaCost ? c1 : c2);
        } else {
            return deck.highestCostCard;
        }
    }

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

    function getSortedDeckRegions(regCount: [string, number][]) {
        return regCount.sort((a, b) => b[1] - a[1])
    }

    function getRarityCount(rarity: CardRarity) {
        const filtered = deck.deckContent.filter(x => x.rarity === rarity)

        return filtered.length === 0 ? 0 : filtered.map(x => x.count).reduce((a, b) => a + b)
    }

    function getTypeCount(type: CardTypes) {
        const filtered = deck.deckContent.filter(x => x.type === type)

        return filtered.length === 0 ? 0 : filtered.map(x => x.count).reduce((a, b) => a + b)
    }

    function getTypeCards(type: CardTypes) {
        return deck.deckContent.filter(x => x.type === type).sort((a, b) => a.manaCost - b.manaCost).map(x => <Card key={x.cardCode} card={x} />);
    }

    function getCount(pred: (card: TCard & { count: number }) => boolean) {
        const filtered = deck.deckContent.filter(pred);

        return filtered.length === 0 ? 0 : filtered.map(x => x.count).reduce((a, b) => a + b);
    }

    const spinnerStyle: React.CSSProperties = {
        position: "absolute",
        top: "50%",
        left: "50%",
        translate: "-50% -50%",
        color: "white",
        width: "50px",
        height: "50px"
    };

    const deckDetailStyle = {
        "--region-color": `var(${getRegionStyle(CardRegions[getHighestCardRegion(getFeaturedCard())])})`,
    } as React.CSSProperties;

    
    useEffect(() => {
        if (deckId) {
            setLoading(true);
            deckActions.getDeckInfo(deckId).then(data => { setDeck(data); setLoading(false); });
        }
    }, [deckId]);
    
    if (loading)
        return <FontAwesomeIcon icon={faSpinner} className="fa-spin" style={spinnerStyle} />;
    
    const numberOfCards = Object.entries(deck.regionCardCount).reduce((a, b) => ["", a[1] + b[1]])[1];

    const graphStyle = (count: number) => ({
        "--card-ratio": `${count / numberOfCards * 100}%`,
        "--region-color": `var(${getRegionStyle(CardRegions[getHighestCardRegion(getFeaturedCard())])})`
    } as React.CSSProperties);

    return (
        <div className="deck-display">
            <div className="deck-background" style={{ backgroundImage: `url(${getFeaturedCard().cardBackgroundLink})` }}></div>
            <div className="background-shade"></div>

            <div className="deck-details">
                <div className="deck-detail regions" style={deckDetailStyle}>
                    <div className="description">Regions</div>
                    {getSortedDeckRegions(Object.entries(deck.regionCardCount))
                        .map(([reg, count]) => <RegionCountDetailed key={reg} regName={CardRegions[+reg].toLowerCase()} regCount={count} />)}
                </div>
                <div className="deck-detail" style={deckDetailStyle}>
                    <div className="description">Rarities</div>
                    <div className="shards">
                        <img src={`${baseUrl}public/rarities/shards.svg`} alt="" />
                        <span>{deck.deckContent.map(x => x.rarity * x.count).reduce((a, b) => a + b)}</span>
                    </div>
                    <div className="rarities">
                        <div className="rarity">
                            <img src={`${baseUrl}public/rarities/champion.svg`} alt="" />
                            <div className="rarity-count">
                                <span>
                                    {getRarityCount(CardRarity.Champion)}
                                </span>
                            </div>
                        </div>
                        <div className="rarity">
                            <img src={`${baseUrl}public/rarities/epic.svg`} alt="" />
                            <div className="rarity-count">
                                <span>
                                    {getRarityCount(CardRarity.Epic)}
                                </span>
                            </div>
                        </div>
                        <div className="rarity">
                            <img src={`${baseUrl}public/rarities/rare.svg`} alt="" />
                            <div className="rarity-count">
                                <span>
                                    {getRarityCount(CardRarity.Rare)}
                                </span>
                            </div>
                        </div>
                        <div className="rarity">
                            <img src={`${baseUrl}public/rarities/common.svg`} alt="" />
                            <div className="rarity-count">
                                <span>
                                    {getRarityCount(CardRarity.Common)}
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="deck-detail" style={deckDetailStyle}>
                    <div className="description">Mana costs</div>
                    <div className="mana-costs">
                        <div className="cost-graph" style={graphStyle(getCount(c => c.manaCost === 0))}>
                            <div className="graph">{getCount(c => c.manaCost === 0)}</div>
                            <span style={{textAlign: "center"}}>0</span>
                        </div>
                        <div className="cost-graph" style={graphStyle(getCount(c => c.manaCost === 1))}>
                            <div className="graph">{getCount(c => c.manaCost === 1)}</div>
                            <span style={{ textAlign: "center" }}>1</span>
                        </div>
                        <div className="cost-graph" style={graphStyle(getCount(c => c.manaCost === 2))}>
                            <div className="graph">{getCount(c => c.manaCost === 2)}</div>
                            <span style={{ textAlign: "center" }}>2</span>
                        </div>
                        <div className="cost-graph" style={graphStyle(getCount(c => c.manaCost === 3))}>
                            <div className="graph">{getCount(c => c.manaCost === 3)}</div>
                            <span style={{ textAlign: "center" }}>3</span>
                        </div>
                        <div className="cost-graph" style={graphStyle(getCount(c => c.manaCost === 4))}>
                            <div className="graph">{getCount(c => c.manaCost === 4)}</div>
                            <span style={{ textAlign: "center" }}>4</span>
                        </div>
                        <div className="cost-graph" style={graphStyle(getCount(c => c.manaCost === 5))}>
                            <div className="graph">{getCount(c => c.manaCost === 5)}</div>
                            <span style={{ textAlign: "center" }}>5</span>
                        </div>
                        <div className="cost-graph" style={graphStyle(getCount(c => c.manaCost === 6))}>
                            <div className="graph">{getCount(c => c.manaCost === 6)}</div>
                            <span style={{ textAlign: "center" }}>6</span>
                        </div>
                        <div className="cost-graph" style={graphStyle(getCount(c => c.manaCost >= 7))}>
                            <div className="graph">{getCount(c => c.manaCost >= 7)}</div>
                            <span style={{ textAlign: "center" }}>7+</span>
                        </div>
                    </div>
                </div>
            </div>

            <div className="card-rows">
                <div className="card-row">
                    <div className="card-column">
                        <div className="column-info">
                            <h2>Champions</h2>
                            <div className="card-count">
                                <span style={{ fontSize: "1.25rem" }}>
                                    {getTypeCount(CardTypes.Champion)}
                                </span>
                            </div>
                        </div>
                        {getTypeCards(CardTypes.Champion)}
                    </div>
                    <div className="card-column">
                        <div className="column-info">
                            <h2>Landmarks</h2>
                            <div className="card-count">
                                <span style={{ fontSize: "1.25rem" }}>
                                    {getTypeCount(CardTypes.Landmark)}
                                </span>
                            </div>
                        </div>
                        {getTypeCards(CardTypes.Landmark)}
                    </div>
                    <div className="card-column">
                        <div className="column-info">
                            <h2>Equipment</h2>
                            <div className="card-count">
                                <span style={{ fontSize: "1.25rem" }}>
                                    {getTypeCount(CardTypes.Equipment)}
                                </span>
                            </div>
                        </div>
                        {getTypeCards(CardTypes.Equipment)}
                    </div>
                </div>
                <div className="card-row">
                    <div className="card-column">
                        <div className="column-info">
                            <h2>Equipment</h2>
                            <div className="card-count">
                                <span style={{ fontSize: "1.25rem" }}>
                                    {getTypeCount(CardTypes.Follower)}
                                </span>
                            </div>
                        </div>
                        {getTypeCards(CardTypes.Follower)}
                    </div>
                </div>
                <div className="card-row">
                    <div className="card-column">
                        <div className="column-info">
                            <h2>Equipment</h2>
                            <div className="card-count">
                                <span style={{ fontSize: "1.25rem" }}>
                                    {getTypeCount(CardTypes.Spell)}
                                </span>
                            </div>
                        </div>
                        {getTypeCards(CardTypes.Spell)}
                    </div>
                </div>
            </div>
        </div>
    );
}