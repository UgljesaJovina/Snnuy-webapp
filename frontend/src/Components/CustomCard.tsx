import React, { CSSProperties, useEffect, useState } from "react";
import { TCustomCard } from "../types"
import { baseUrl } from "../utils/GlobalVariables";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeart as solidHeart } from "@fortawesome/free-solid-svg-icons";
import { faHeart as hollowHeart } from "@fortawesome/free-regular-svg-icons";
import { useRecoilValue } from "recoil";
import { userAtom } from "../atoms";
import { useCustomCardActions } from "../actions";

const CustomCard: React.FC<{ card: TCustomCard, style?: CSSProperties }> = ({ card, style }) => {
    const user = useRecoilValue(userAtom);
    const customCardActions = useCustomCardActions();
    const [liked, setLiked] = useState(user.likedCards.some(x => x === card.id));
    const [numberOfLikes, setNumberOfLikes] = useState(card.numberOfLikes);

    useEffect(() => {
        setNumberOfLikes(card.numberOfLikes);
        setLiked(user.likedCards.some(x => x === card.id));
    }, [card.numberOfLikes, user])
    
    function like() {
        const currentLike = liked;
        const currentLikes = card.numberOfLikes;
        customCardActions.likeACard(card.id)
            .then(data => { setLiked(data.liked); setNumberOfLikes(data.numberOfLikes); })
            .catch(err => { setLiked(currentLike); setNumberOfLikes(currentLikes) });
    }
    
    return (
        <div className="custom-card" style={style}>
            <img className="custom-card-image" src={`${baseUrl}public/CustomCards/${card.cardImagePath}`} alt="" />
            <div className="custom-card-info">
                <p className="card-name">{card.cardName}</p>
                <div className="creator-region">
                    <p className="created-by">Created by:</p>
                    <Link to={`/users/${card.owner.id}`} className="owner-name">{card.owner.username}</Link>
                </div>
                <div className="like-region">
                    <div className="like" onClick={like}>
                        <div className="like-hover-bg"></div>
                        <FontAwesomeIcon icon={liked ? solidHeart : hollowHeart}
                            className={liked ? "liked" : ""} />
                    </div>
                    <p className="likes">{numberOfLikes}</p>
                </div>
            </div>
        </div>
    );
}

export { CustomCard };