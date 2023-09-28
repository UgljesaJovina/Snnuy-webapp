import React, { useEffect, useState } from "react";
import { CardComponent } from "./Card";
import { CustomCard } from "../types";

const Home: React.FC = () => {

    const [card, setCard] = useState<CustomCard>();

    useEffect(() => {
        
    }, []);

    return (
        <div className="page-container">
            <div className="featured-section">
                <CardComponent card={card} />
                <iframe width="560" height="315" src="https://www.youtube.com/embed/yCR2llVsVss?si=3ryi3UFhTqFw3jql" frameBorder="0" allowFullScreen></iframe>
            </div>
            <div className="">
            </div>
        </div>
    );
}

export { Home };