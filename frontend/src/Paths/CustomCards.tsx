import React, { useEffect, useRef, useState } from "react";
import { useCustomCardActions } from "../actions";
import { TCardFilter, TCustomCard } from "../types";
import { CardCreateModal, CustomCard, Dropdown, DropdownButton, DropdownContent, DropdownItem, DropdownList, Modal } from "../components";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { baseUrl } from "../utils/GlobalVariables";
import { CardRegions, CardTypes, SortByDate, SortByPopularity } from "../enums";

const CustomCards: React.FC = () => {
    const customCardActions = useCustomCardActions();
    const [cards, setCards] = useState<TCustomCard[]>([]);
    const [filters, setFilters] = useState<TCardFilter>({ skip: 0, take: 20 });
    const iconBase = baseUrl + "public/regionicons/";

    const [regions, setRegions] = useState<any[]>([]);
    const [types, setTypes] = useState<any[]>([]);
    const [postedBefore, setReleasedBefore] = useState<string>("");
    const [postedAfter, setReleasedAfter] = useState<string>("");
    const [byDate, setByDate] = useState<SortByDate[]>([SortByDate.Newest]);
    const [byPopularity, setByPopularity] = useState<SortByPopularity[]>([SortByPopularity.None]);

    const [creationModal, setCreationModal] = useState(false);
    const cardsDiv = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const cDiv = cardsDiv.current;
        if (!cDiv) return;

        const scrollFunc = (e: Event) => {
            const isAtBottom = cDiv.scrollTop + cDiv.clientHeight >= cDiv.scrollHeight;
            if (isAtBottom)
                customCardActions.getAllFiltered({ ...filters, skip: cards.length, take: cards.length + 20 }).then(data => setCards(curr => [...curr, ...data])).catch(e => console.log(e))
        }

        cDiv.addEventListener("scroll", scrollFunc);

        return () => cDiv.removeEventListener("scroll", scrollFunc);
    }, [cardsDiv.current, cards, filters]);

    useEffect(() => {
        const f: TCardFilter = {
            skip: 0,
            take: 20,
            regions: regions.length === 0 ? CardRegions.All : regions.reduce((a, b) => a + b),
            cardTypes: types.length === 0 ? CardTypes.All : types.reduce((a, b) => a + b),
            byDate: byDate[0],
            byPopularity: byPopularity[0],
            postedBefore: !!postedBefore ? new Date(postedBefore) : undefined,
            postedAfter: !!postedAfter ? new Date(postedAfter) : undefined
        }

        customCardActions.getAllFiltered(f).then(data => setCards(data)).catch(e => console.log(e));
        setFilters(f);
    }, [regions, types, postedBefore, postedAfter, byDate, byPopularity]);

    return (
        <div className="custom-card-page">
            <div style={{ overflow: "hidden auto" }} ref={cardsDiv}>
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
                    <Dropdown setter={setTypes} multichoice style={{ width: "80%", marginLeft: "10%" }}>
                        <DropdownButton>Card type</DropdownButton>
                        <DropdownContent>
                            <DropdownList>
                                <DropdownItem name="Follower" value={1}>Follower</DropdownItem>
                                <DropdownItem name="Champion" value={2}>Champion</DropdownItem>
                                <DropdownItem name="Spell" value={4}>Spell</DropdownItem>
                                <DropdownItem name="Landmark" value={8}>Landmark</DropdownItem>
                                <DropdownItem name="Equipment" value={16}>Equipment</DropdownItem>
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
            </div>
            <Modal isOpen={creationModal} setOpen={setCreationModal}>
                <CardCreateModal setCards={setCards} setOpen={setCreationModal} />
            </Modal>
        </div>
    );
}

export { CustomCards };