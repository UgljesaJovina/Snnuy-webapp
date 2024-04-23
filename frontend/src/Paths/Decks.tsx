import { Deck } from "lor-deckcodes-ts";
import React, { useEffect, useState } from "react";
import { useDeckActions } from "../actions";

const Decks: React.FC = () => {
    const [decks, setDecks] = useState<Deck[]>([]);
    const deckActions = useDeckActions();

    useEffect(() => {

    })


    return (
        <div className="custom-card-page">
            <div style={{overflow: "hidden auto"}}>
                <div className="custom-card-container">

                </div>
            </div>
            <div className="filters">

            </div>
        </div>
    );
}

export { Decks };