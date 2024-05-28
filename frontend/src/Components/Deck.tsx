import React, { CSSProperties, useEffect, useState } from "react";
import { TDeck } from "../types"
import { useRecoilValue } from "recoil";
import { userAtom } from "../atoms";
import { faHeart as solidHeart } from "@fortawesome/free-solid-svg-icons";
import { faHeart as hollowHeart } from "@fortawesome/free-regular-svg-icons";
import { useDeckActions } from "../actions";
import { CardRegions } from "../enums";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { RegionCount } from "./RegionCount";
import { baseUrl } from "../utils/GlobalVariables";

const Deck: React.FC<{ deck: TDeck, style?: CSSProperties }> = ({deck, style}) => {
    const user = useRecoilValue(userAtom);
    const deckActions = useDeckActions();
    const [liked, setLiked] = useState<boolean>(false);
    const [numberOfLikes, setNumberOfLikes] = useState<number>(0);
    const eternalIcon = baseUrl + "public/format/eternal.png";
    const standardIcon = baseUrl + "public/format/standard.png";
    const [regCount, setRegCount] = useState<[string, number][]>([]);

    useEffect(() => {
        setLiked(user.likedDecks.some(x => x === deck.id));
        setNumberOfLikes(deck.numberOfLikes);
        setRegCount(Object.entries(deck.regionCardCount).sort((a, b) => b[1] - a[1]));
    }, [deck]);

    function getFeaturedCard() {
        if (deck.champions.length > 0) {
            return deck.champions.reduce((c1, c2) => c1.manaCost > c2.manaCost ? c1 : c2);
        } else {
            return deck.highestCostCard;
        }
    }

    function getGradientString() {
        // sorted regions
        const sr = Object.entries(deck.regionCardCount)
            .map(([reg, num]) => ({ region: CardRegions[+reg], count: num }))
            .sort((a, b) => b.count - a.count)
        
        const cardCount = sr.map(x => x.count).reduce((r1, r2) => r1 + r2);
            
        const getStyle = (reg: string) => `--${reg.toLowerCase().replaceAll("_", "-")}`;
        
        return `linear-gradient(90deg, var(${getStyle(sr[0].region)}) ${sr[0].count / cardCount * 100}%, var(${getStyle(sr[1].region)}) ${sr[0].count / cardCount * 100}%)`
    }
    
    function like() {
        deckActions.likeADeck(deck.id)
            .then(data => { setLiked(data.liked); setNumberOfLikes(data.numberOfLikes); })
    }

    return (
        <div className="deck" style={style}>
            <div className="featured-card" style={{backgroundImage: `url(${getFeaturedCard().cardBackgroundLink})`}}></div>
            <div className="deck-region-gradient" style={{background: getGradientString()}}></div>
            <div className="deck-info">
                <p className="deck-name">{deck.deckName}</p>
                <div className="region-card-count">
                    {regCount.filter(x => x[1] != 0).map(x => <RegionCount key={x[0]} regName={CardRegions[+x[0]].toLowerCase()} regCount={x[1]} />)}
                </div>
                <div className="creator-region">
                    <p className="created-by">Created by:</p>
                    <Link to={`/users/${deck.owner.id}`} className="owner-name">{deck.owner.username}</Link>
                </div>
                <div className="like-region">
                    <div className="like" onClick={like}>
                        <div className="like-hover-bg"></div>
                        <FontAwesomeIcon icon={liked ? solidHeart : hollowHeart} className={liked ? "liked" : ""} />
                    </div>
                    <p className="likes">{numberOfLikes}</p>
                </div>
            </div>
            <div className="format-background">
                <div className="format-icon" style={{backgroundImage: `url(${deck.standard ? standardIcon : eternalIcon})`}}></div>
            </div>
        </div>
    );
}

export { Deck };