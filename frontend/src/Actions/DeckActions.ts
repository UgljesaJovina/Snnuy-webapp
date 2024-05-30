import { useFetchWrapper } from "../utils/FetchWrapper"
import { TDeckFilters } from "../types";
import { TDeckCreation } from "../types/DeckCreation";
import { useSetRecoilState } from "recoil";
import { LatestDeckAtom, userAtom } from "../atoms";

export const useDeckActions = () => {
    const baseUrl = "deck/";
    const fwrapper = useFetchWrapper();
    const setUser = useSetRecoilState(userAtom);
    const setLatestDeck = useSetRecoilState(LatestDeckAtom);

    return {
        getAll,
        getAllFiltered,
        postADeck,
        likeADeck,
        getLatestDeckOTD,
        getDeckInfo
    }

    function getAll() {
        return fwrapper.get({ url: baseUrl + "get-all" });
    }

    async function getAllFiltered(filters: TDeckFilters) {
        const activeFilters: string[] = [];

        for (const [key, value] of Object.entries(filters)) {
            if (value !== undefined) {
                if (key === 'postedBefore' || key === 'postedAfter') {
                    activeFilters.push(`${key}=${(value as Date).toISOString()}`);

                } else {
                    activeFilters.push(`${key}=${value}`);
                }
            }
        }

        return fwrapper.get({ url: baseUrl + `get-all-filtered?${activeFilters.join("&")}` });
    }

    async function getDeckInfo(deckId: string) {
        return fwrapper.get({ url: baseUrl + `get-deck-info/${deckId}` });
    }

    async function postADeck(props: TDeckCreation) {
        return fwrapper.post({ url: baseUrl + "create-a-deck", body: props, reqAuth: true });
    }

    async function likeADeck(id: string) {
        return fwrapper.patch({ url: baseUrl + `like-a-deck/${id}`, reqAuth: true })
        .then(data => {
            if (data.liked) setUser(curr => ({ ...curr, likedDecks: [...curr.likedDecks, data.id] }))
            else setUser(curr => ({ ...curr, likedDecks: [...curr.likedDecks.filter(x => x !== data.id)] }))
            
            return data;
        });
    }

    async function getLatestDeckOTD() {
        return fwrapper.get({ url: baseUrl + "get-latest-deck-otd" }).then(data => { setLatestDeck(data); return data; });
    }
}