import React from "react";
import { TCustomCard } from "../types"
import { baseUrl } from "../utils/GlobalVariables";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faHeart as solidHeart } from "@fortawesome/free-solid-svg-icons";
import { faHeart as hollowHeart } from "@fortawesome/free-regular-svg-icons";
import { useRecoilValue } from "recoil";
import { userAtom } from "../atoms";

const CustomCard: React.FC<{ card: TCustomCard }> = ({ card }) => {
    const user = useRecoilValue(userAtom);

    function like() {
        
    }

    return (
        <div className="custom-card">
            <img className="custom-card-image" src={`${baseUrl}public/CustomCards/${card.id}.png`} alt="" />
            <div className="custom-card-info">
                <div className="creator-region">
                    <p className="created-by">Created by: </p>
                    <Link to={`/users/${card.owner.id}`} className="owner-name">{card.owner.username}</Link>
                </div>
                <div className="like-region">
                    <FontAwesomeIcon icon={user.likedCards.find(x => x === card.id) ? solidHeart : hollowHeart} />
                    <p className="likes">{card.numberOfLikes}</p>
                </div>
            </div>
        </div>
    );
}

export { CustomCard };