import { useSetRecoilState } from "recoil";
import { useFetchWrapper } from "../utils/FetchWrapper"
import { LatestCustomCardAtom, userAtom } from "../atoms";
import { TCardFilter } from "../types";


function useCustomCardActions() {
    const baseUrl = "customcard/";
    const fwrapper = useFetchWrapper();
    const setLatestCC = useSetRecoilState(LatestCustomCardAtom);
    const setUser = useSetRecoilState(userAtom);

    return {
        getAll,
        getAllFromUser,
        getAllFiltered,
        getAllNonValidatedCards,
        getLatestCardOTD,
        getAllCardsOTD,
        likeACard,
        validateACard
    }

    async function getAll() {
        return fwrapper.get({ url: baseUrl + "get-all" });
    }

    async function getAllFromUser(id: string) {
        return fwrapper.get({ url: baseUrl + `get-all/${id}` })
    }

    async function getAllFiltered(filters: TCardFilter) {
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

    async function getAllNonValidatedCards() {
        // getAllNonValid
        // req auth
    }

    async function getLatestCardOTD() {
        return fwrapper.get({ url: baseUrl + "get-latest-card-otd" }).then(data => { setLatestCC(data); return data; });
    }

    async function getAllCardsOTD() {
        // getAllCardsOTD
    }

    async function likeACard(id: string) {
        return fwrapper.patch({ url: baseUrl + `like-a-card/${id}`, reqAuth: true })
        .then(data => {
            if (data.liked) setUser(curr => ({ ...curr, likedCards: [...curr.likedCards, data.id] }));
            else setUser(curr => ({ ...curr, likedCards: [...curr.likedCards.filter(x => x !== data.id)] }));

            return data;
        });
    }

    async function validateACard(id: string, state: boolean) {
        // validateACard/id?state
        // req auth
    }
}

export { useCustomCardActions }