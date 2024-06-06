import { Dispatch, SetStateAction, useRef, useState } from "react"
import { TDeck } from "../types"
import { Dropdown, DropdownButton, DropdownContent, DropdownItem, DropdownList } from "./Dropdown";
import { useDeckActions } from "../actions";
import { getDeckFromCode } from "lor-deckcodes-ts";

export const DeckCreateModal: React.FC<{ setDecks: Dispatch<SetStateAction<TDeck[]>>, setOpen: Dispatch<boolean> }> = ({ setDecks, setOpen }) => {
    const deckActions = useDeckActions();
    const deckCode = useRef<HTMLInputElement>(null);
    const deckName = useRef<HTMLInputElement>(null);
    const [deckType, setDeckType] = useState<number[]>([]);

    function handleSubmit() {
        if (deckCode.current === null || deckName.current === null || deckName.current.value.length > 50 || deckType.length === 0) {
            alert("You haven't filled out all the inputs");
            return;
        }

        try {
            getDeckFromCode(deckCode.current.value);
        } catch {
            alert("The code is not correct!");
        }

        deckActions.postADeck({ deckCode: deckCode.current.value, deckName: deckName.current.value, deckType: deckType[0] })
            .then(x => { 
                setDecks(curr => [x, ...curr]); 
                setOpen(false); 
                if (deckCode.current) deckCode.current.value = "";
                if (deckName.current) deckName.current.value = "";
            }).catch(x => alert(x));
    }

    return (
        <div className="create-deck-page">
            <div className="creation-field">
                <label htmlFor="deck-code">Deck code:</label>
                <input type="text" ref={deckCode} id="deck-code" name="deck-code" />
            </div>
            <div className="creation-field">
                <label htmlFor="deck-name">Deck name:</label>
                <input type="text" ref={deckName} id="deck-name" name="deck-name" />
            </div>
            <div className="creation-field">
                <label>Deck type:</label>
                <Dropdown setter={setDeckType}>
                    <DropdownButton>Deck Type</DropdownButton>
                    <DropdownContent>
                        <DropdownList>
                            <DropdownItem name="Aggro Burn" value={1}>Aggro Burn</DropdownItem>
                            <DropdownItem name="Aggro Swarm" value={2}>Aggro Swarm</DropdownItem>
                            <DropdownItem name="Midrange" value={4}>Midrange</DropdownItem>
                            <DropdownItem name="Control" value={8}>Control</DropdownItem>
                            <DropdownItem name="Combo" value={16}>Combo</DropdownItem>
                        </DropdownList>
                    </DropdownContent>
                </Dropdown>
            </div>
            <button onClick={handleSubmit} className="deck-submit">Post</button>
        </div>
    );
}