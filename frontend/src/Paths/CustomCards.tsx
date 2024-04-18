import React, { useEffect, useState } from "react";
import { useCustomCardActions } from "../actions";
import { TCustomCard } from "../types";
import { CardCreateModal, CustomCard, Modal } from "../components";
import { useRecoilValue } from "recoil";
import { userAtom } from "../atoms";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { useNavigate } from "react-router";

const CustomCards: React.FC = () => {
    const user = useRecoilValue(userAtom);
    const customCardActions = useCustomCardActions();
    const [cards, setCards] = useState<TCustomCard[]>([]);
    const [skip, setSkip] = useState(0);
    const [take, setTake] = useState(0);
    const navigate = useNavigate();

    const [creationModal, setCreationModal] = useState(false);

    useEffect(() => {
        customCardActions.getAllFiltered({ skip: 0, take: 20 }).then(data => setCards(data)).catch(s => console.log(s));
    }, []);

    return (
        <div className="custom-card-page">
            <div style={{overflow: "hidden auto"}}>
                <div className="custom-card-container">
                    {cards.map(c => <CustomCard key={c.id} card={c} />)}
                </div>
            </div>
            <div className="filters">
                <div className="create">
                    <div className="button-container" onClick={() => setCreationModal(true)}>
                        <FontAwesomeIcon icon={faPlus} className="create-svg" />
                        <button className="create-button">Create a Card</button>
                    </div>
                    <hr style={{ width: "100%", marginTop: "20px" }} />
                </div>
                <div className="filter-option">
                    <h2>Regions:</h2>
                </div>
            </div>
            <Modal isOpen={creationModal} setOpen={setCreationModal}>
                <CardCreateModal />
            </Modal>
        </div>
    );
}

export { CustomCards };