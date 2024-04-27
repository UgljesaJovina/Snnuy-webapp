import React, { useEffect } from "react";
import { CustomCard } from "../components";
import { useCustomCardActions } from "../actions";
import { useRecoilValue } from "recoil";
import { LatestCustomCardAtom } from "../atoms";

const Home: React.FC = () => {
    const card = useRecoilValue(LatestCustomCardAtom);
    const ccActions = useCustomCardActions()

    useEffect(() => {
        ccActions.getLatestCardOTD().catch(err => console.log(err));
    }, []);

    return (
        <div className="home-page-container">
            <div className="featured-section">
                <div>
                    <div>
                        <h2>Featured Card</h2>
                        <hr />
                        <h4>A card made by <u>{card.card.owner.username}</u> called <u>{card.card.cardName}</u> was chosen as the featured card for the day {new Date(card.settingDate).toLocaleDateString()}</h4>
                    </div>
                    <CustomCard card={card.card} style={{width: "50%", marginBottom: "10%", marginLeft: "25%"}} />
                </div>
                <div style={{gridTemplateColumns: "1fr 1fr"}}>
                    <h1>*Deck*</h1>
                    <div>
                        <h2>Featured Deck</h2>
                        <hr />
                        <h4>A card made by Ugljesa Starcevic called Void Anomaly was chosen as the featured deck for the day 10/3</h4>
                    </div>
                </div>
                <div>
                    <div>
                        <h2>Latest Snnuy video</h2>
                        <hr />
                    </div>
                    <iframe title="player" width="100%" style={{aspectRatio: "16 / 9"}} src="https://www.youtube.com/embed/L17jInDbT0c?si=0TnZ6MSH63e0sL2i" frameBorder="0" allowFullScreen></iframe>
                </div>
            </div>
        </div>
    );
}

export { Home };