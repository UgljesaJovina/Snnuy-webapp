import React, { useEffect } from "react";
import { CustomCard } from "../components";
import { useCustomCardActions, useDeckActions } from "../actions";
import { useRecoilValue } from "recoil";
import { LatestCustomCardAtom, LatestDeckAtom } from "../atoms";
import { Deck } from "../components/Deck";

const Home: React.FC = () => {
    const card = useRecoilValue(LatestCustomCardAtom);
    const deck = useRecoilValue(LatestDeckAtom);
    const ccActions = useCustomCardActions()
    const deckActions = useDeckActions();

    useEffect(() => {
        if (card.id === "") 
            ccActions.getLatestCardOTD().catch(err => console.log(err));

        if (deck.id === "")
            deckActions.getLatestDeckOTD().catch(err => console.log(err));
    }, []);

    return (
        <div className="home-page-container">
            <div className="featured-section">
                <div>
                    <div>
                        <h2>Featured Card</h2>
                        <hr />
                        <p>A card made by <u>{card.card.owner.username}</u> called <u>{card.card.cardName}</u> was chosen as the featured card for the day {new Date(card.settingDate).toLocaleDateString()}</p>
                    </div>
                    <CustomCard card={card.card} style={{width: "50%", marginBottom: "10%", marginLeft: "25%"}} />
                </div>
                <div style={{gridTemplateColumns: "1fr 1fr"}}>
                    <Deck deck={deck.deck} />
                    <div>
                        <h2>Featured Deck</h2>
                        <hr />
                        <p>A card made by <u>{deck.deck.owner.username}</u> called <u>{deck.deck.deckName}</u> was chosen as the featured deck for the day {new Date(card.settingDate).toLocaleDateString()}</p>
                    </div>
                </div>
                <div>
                    <div>
                        <h2>Latest LoR video</h2>
                        <hr />
                    </div>
                    <iframe title="player" width="100%" style={{ aspectRatio: "16 / 9" }} src="https://www.youtube-nocookie.com/embed?listType=playlist&list=UUMZ5vTV7dLz_yoWw4nOzDwg" allowFullScreen></iframe>
                </div>
            </div>
        </div>
    );
}

export { Home };