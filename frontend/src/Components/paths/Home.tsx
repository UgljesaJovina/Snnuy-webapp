import React, { useEffect } from "react";
import { CustomCardOTD } from "..";
import { useCustomCardActions } from "../../Actions";
import { useRecoilState } from "recoil";
import { LatestCustomCardAtom } from "../../Atoms";

const Home: React.FC = () => {

    const [card, setCard] = useRecoilState(LatestCustomCardAtom);
    const ccActions = useCustomCardActions()

    useEffect(() => {
        ccActions.getLatestCardOTD().catch(err => console.log(err));
    }, []);

    return (
        <div className="page-container">
            <div className="featured-section">
                <CustomCardOTD card={card} />
                <div></div>
            </div>
            <iframe title="player" width="560" height="315" src="https://www.youtube.com/embed/yCR2llVsVss?si=3ryi3UFhTqFw3jql" frameBorder="0" allowFullScreen></iframe>
            <div className="">
            </div>
        </div>
    );
}

export { Home };