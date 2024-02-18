import React, { useEffect } from "react";
import { CustomCardOTD } from "../components";
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
                    <h2>Featured Card</h2>
                    <hr />
                    <h4>A card made by Ugljesa Starcevic called Void Anomaly was chosen as the featured card for the day 10/3</h4>
                </div>
                <CustomCardOTD card={card} />
                <h1>*Deck*</h1>
                <div>
                    <h2>Featured Deck</h2>
                    <hr />
                    <h4>A card made by Ugljesa Starcevic called Void Anomaly was chosen as the featured deck for the day 10/3</h4>
                </div>
                <div>
                    <h2>Latest Snnuy video</h2>
                    <hr />
                </div>
                <iframe title="player" width="560" height="315" src="https://www.youtube.com/embed/L17jInDbT0c?si=0TnZ6MSH63e0sL2i" frameBorder="0" allowFullScreen></iframe>
            </div>
        </div>
    );
}

export { Home };