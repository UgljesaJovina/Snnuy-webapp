import { useSetRecoilState } from "recoil";
import { useFetchWrapper } from "../utils/FetchWrapper"
import { LatestCustomCardAtom } from "../atoms";
import { TCardCreation, TCardFilter } from "../types";


function useCustomCardActions() {
    const baseUrl = "customcard/";
    const fwrapper = useFetchWrapper();
    const setLatestCC = useSetRecoilState(LatestCustomCardAtom);

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
                if (key === 'releasedBefore' || key === 'releasedAfter') {
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
        return fwrapper.patch({ url: baseUrl + `like-a-card/${id}`, reqAuth: true });
    }

    // async function createACard(props: TCardCreation) { 
    //     /*
    //         public string CardName { get; set; }
    //         public string CardDescription { get; set; }
    //         public CardTypes CardType { get; set; } 
    //         public Stream DataStream;
    //         public UserAccount Owner;
    //         public IFormFile ImageFile { get; set; }
    //     */ 

    //     const formData = new FormData();

    //     return fwrapper.post({ url: baseUrl + "create-a-card", reqAuth: true, body: props,  })

    //     // createACard
    //     // body: props, multipart/form
    //     // req auth
    // }

    async function validateACard(id: string, state: boolean) {
        // validateACard/id?state
        // req auth
    }
}

export { useCustomCardActions }