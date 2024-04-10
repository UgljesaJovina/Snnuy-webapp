import React, { useEffect, useState } from "react";
import { useCustomCardActions } from "../actions";
import { TCustomCard } from "../types";
import { CustomCard } from "../components";
import { useRecoilValue } from "recoil";
import { userAtom } from "../atoms";

const CustomCards: React.FC = () => {
    const user = useRecoilValue(userAtom);
    const customCardActions = useCustomCardActions();
    const [cards, setCards] = useState<TCustomCard[]>([]);

    useEffect(() => {
        customCardActions.getAll().then(data => setCards(data)).catch(s => console.log(s));
    }, []);

    return (
        <div className="custom-card-page">
            <div style={{overflow: "hidden auto"}}>
                <div className="custom-card-container">
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                </div>
            </div>
            <div className="filters">

            </div>
        </div>
    );
}

export { CustomCards };