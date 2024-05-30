import React, { useEffect, useRef, useState } from "react";
import { useDeckActions } from "../actions";
import { TCardFilter, TDeck, TDeckFilters } from "../types";
import { CardRegions, CardTypes, SortByDate, SortByPopularity } from "../enums";
import { baseUrl } from "../utils/GlobalVariables";
import { Deck } from "../components/Deck";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { DeckCreateModal, DeckDisplay, Dropdown, DropdownButton, DropdownContent, DropdownItem, DropdownList, Modal } from "../components";

const Decks: React.FC = () => {
    const [decks, setDecks] = useState<TDeck[]>([]);
    const deckActions = useDeckActions();
    const [filters, setFilters] = useState<TCardFilter>({ skip: 0, take: 20 });
    const iconBase = baseUrl + "public/regionicons/";

    const [regions, setRegions] = useState<any[]>([]);
    const [deckTypes, setDeckTypes] = useState<any[]>([]);
    const [postedBefore, setReleasedBefore] = useState<string>("");
    const [postedAfter, setReleasedAfter] = useState<string>("");
    const [byDate, setByDate] = useState<SortByDate[]>([SortByDate.Newest]);
    const [byPopularity, setByPopularity] = useState<SortByPopularity[]>([SortByPopularity.None]);
    const [standard, setStandard] = useState<boolean[]>([true]);

    const [creationModal, setCreationModal] = useState(false);
    const [deckModal, setDeckModal] = useState(false);
    const [selectedDeck, setSelectedDeck] = useState("");
    const decksDiv = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const dDiv = decksDiv.current;
        if (!dDiv) return;

        const scrollFunc = (e: Event) => {
            const isAtBottom = dDiv.scrollTop + dDiv.clientHeight >= dDiv.scrollHeight;
            if (isAtBottom)
                deckActions.getAllFiltered({ ...filters, skip: decks.length, take: decks.length + 20 }).then(data => setDecks(curr => [...curr, ...data])).catch(e => alert(e));
        }

        dDiv.addEventListener("scroll", scrollFunc);

        return () => dDiv.removeEventListener("scroll", scrollFunc);
    }, [decksDiv.current, decks, filters]);

    useEffect(() => {
        const f: TDeckFilters = {
            skip: 0,
            take: 20,
            includeEternal: standard.length > 0 ? standard[0] : true,
            regions: regions.length === 0 ? CardRegions.All : regions.reduce((a, b) => a + b),
            deckTypes: deckTypes.length === 0 ? CardTypes.All : deckTypes.reduce((a, b) => a + b),
            byDate: byDate[0],
            byPopularity: byPopularity[0],
            postedBefore: !!postedBefore ? new Date(postedBefore) : undefined,
            postedAfter: !!postedAfter ? new Date(postedAfter) : undefined,
        }

        deckActions.getAllFiltered(f).then(data => setDecks(data)).catch(e => alert(e));
        setFilters(f);
    }, [regions, deckTypes, postedBefore, postedAfter, byDate, byPopularity, standard]);

    return (
        <div className="deck-page">
            <div style={{overflow: "hidden auto"}}>
                <div className="deck-container">
                    {decks.map(deck => <Deck key={deck.id} deck={deck} onClick={() => { setDeckModal(true); setSelectedDeck(deck.id) }} />)}
                </div>
            </div>
            <div className="filters">
                <div className="filters">
                    <div className="create">
                        <div className="button-container" onClick={() => setCreationModal(true)}>
                            <FontAwesomeIcon icon={faPlus} className="create-svg" />
                            <button className="create-button">Create a Card</button>
                        </div>
                        <hr style={{ width: "90%", marginTop: "10px" }} />
                    </div>
                    <div className="filter-option">
                        <h2>Regions:</h2>
                        <Dropdown setter={setRegions} multichoice style={{ width: "80%", marginLeft: "10%" }}>
                            <DropdownButton>Card regions</DropdownButton>
                            <DropdownContent>
                                <DropdownList>
                                    <DropdownItem name="Bandle City" value={1}><img alt="" src={iconBase + "bandle_city.png"} />Bandle City</DropdownItem>
                                    <DropdownItem name="Bilgewater" value={2}><img alt="" src={iconBase + "bilgewater.png"} />Bilgewater</DropdownItem>
                                    <DropdownItem name="Demacia" value={4}><img alt="" src={iconBase + "demacia.png"} />Demacia</DropdownItem>
                                    <DropdownItem name="Freljord" value={8}><img alt="" src={iconBase + "freljord.png"} />Feljord</DropdownItem>
                                    <DropdownItem name="Ionia" value={16}><img alt="" src={iconBase + "ionia.png"} />Ionia</DropdownItem>
                                    <DropdownItem name="Noxus" value={32}><img alt="" src={iconBase + "noxus.png"} />Noxus</DropdownItem>
                                    <DropdownItem name="Piltover & Zaun" value={64}><img alt="" src={iconBase + "pnz.png"} />Piltover & Zaun</DropdownItem>
                                    <DropdownItem name="Shadow Isles" value={128}><img alt="" src={iconBase + "shadow_isles.png"} />Shadow Isles</DropdownItem>
                                    <DropdownItem name="Shurima" value={256}><img alt="" src={iconBase + "shurima.png"} />Shurima</DropdownItem>
                                    <DropdownItem name="Targon" value={512}><img alt="" src={iconBase + "targon.png"} />Targon</DropdownItem>
                                    <DropdownItem name="Runterra" value={1024}><img alt="" src={iconBase + "runeterra.png"} />Runeterra</DropdownItem>
                                </DropdownList>
                            </DropdownContent>
                        </Dropdown>
                    </div>
                    <div className="filter-option">
                        <h2>Card Types:</h2>
                        <Dropdown setter={setDeckTypes} multichoice style={{ width: "80%", marginLeft: "10%" }}>
                            <DropdownButton>Deck type</DropdownButton>
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
                    <div className="filter-option">
                        <h2>Posted before:</h2>
                        <input type="date" onChange={e => setReleasedBefore(e.target.value)} className="date-filter" />
                    </div>
                    <div className="filter-option">
                        <h2>Posted after:</h2>
                        <input type="date" onChange={e => setReleasedAfter(e.target.value)} className="date-filter" />
                    </div>
                    <div className="filter-option">
                        <h2>Sort by date</h2>
                        <Dropdown setter={setByDate} style={{ width: "80%", marginLeft: "10%" }}>
                            <DropdownButton>Date Sort</DropdownButton>
                            <DropdownContent>
                                <DropdownList>
                                    <DropdownItem name="Newest" value={SortByDate.Newest} index>Newest</DropdownItem>
                                    <DropdownItem name="Oldest" value={SortByDate.Oldest}>Oldest</DropdownItem>
                                </DropdownList>
                            </DropdownContent>
                        </Dropdown>
                    </div>
                    <div className="filter-option">
                        <h2>Sort by popularity</h2>
                        <Dropdown setter={setByPopularity} style={{ width: "80%", marginLeft: "10%" }}>
                            <DropdownButton>Popularity Sort</DropdownButton>
                            <DropdownContent>
                                <DropdownList>
                                    <DropdownItem name="None" value={SortByPopularity.None} index>None</DropdownItem>
                                    <DropdownItem name="Most popular" value={SortByPopularity.MostPopular}>Most popular</DropdownItem>
                                    <DropdownItem name="Least popular" value={SortByPopularity.LeastPopular}>Least popular</DropdownItem>
                                </DropdownList>
                            </DropdownContent>
                        </Dropdown>
                    </div>
                    <div className="filter-option">
                        <h2>Format</h2>
                        <Dropdown setter={setStandard} style={{ width: "80%", marginLeft: "10%" }}>
                            <DropdownButton>Format</DropdownButton>
                            <DropdownContent>
                                <DropdownList>
                                    <DropdownItem name="Eternal" value={true} index>Eternal</DropdownItem>
                                    <DropdownItem name="Standard" value={false}>Standard</DropdownItem>
                                </DropdownList>
                            </DropdownContent>
                        </Dropdown>
                    </div>
                </div>
            </div>
            <Modal isOpen={creationModal} setOpen={setCreationModal} contentStyle={{width: "30%", height: "50%", minWidth: "300px", minHeight: "400px"}}>
                <DeckCreateModal setDecks={setDecks} setOpen={setCreationModal} />
            </Modal>
            <Modal isOpen={deckModal} setOpen={setDeckModal} contentStyle={{ backgroundColor: "transparent" }}>
                <DeckDisplay deckId={selectedDeck} />
            </Modal>
        </div>
    );
}

export { Decks };