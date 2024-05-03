import { useFetchWrapper } from "../utils/FetchWrapper"
import { TDeckFilters } from "../types";
import { TDeckCreation } from "../types/DeckCreation";

export const useDeckActions = () => {
    const baseUrl = "deck/";
    const fwrapper = useFetchWrapper();
    // const setLatestDeck = useSetRecoilState(LatestDeckAtom)

    return {
        getAll,
        getAllFiltered,
        postADeck,
        likeADeck
    }

    function getAll() {
        return fwrapper.get({ url: baseUrl + "get-all" });
    }

    function getAllFiltered(filters: TDeckFilters) {
        const activeFilters: string[] = [];

        for (const [key, value] of Object.entries(filters)) {
            if (value !== undefined) {
                if (key === 'releasedBefore' || key === 'releasedAfter') {
                    activeFilters.push(`${key}=${(value as Date).toISOString()}`);
                } else {
                    activeFilters.push(`${key}=${value}`);
                }
            }
        }

        return fwrapper.get({ url: baseUrl + `get-all-filtered?${activeFilters.join("&")}` });
    }

    function postADeck(props: TDeckCreation) {
        return fwrapper.post({ url: baseUrl + "create-a-deck", body: props, reqAuth: true });
    }

    function likeADeck(id: string) {
        return fwrapper.put({ url: baseUrl + `like-a-deck/${id}`, reqAuth: true });
    }
}