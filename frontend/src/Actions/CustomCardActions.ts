import { useSetRecoilState } from "recoil";
import { useFetchWrapper } from "../utils/FetchWrapper"
import { LatestCustomCardAtom } from "../atoms";


function useCustomCardActions() {
    const baseUrl = "customcard/";
    const fwrapper = useFetchWrapper();
    const setLatestCC = useSetRecoilState(LatestCustomCardAtom);

    return {
        getAll,
        getAllFromUser,
        getAllNonValidatedCards,
        getLatestCardOTD,
        getAllCardsOTD,
        likeACard,
        createACard,
        validateACard
    }

    async function getAll() {
        return fwrapper.get({ url: baseUrl + "get-all" })
    }

    async function getAllFromUser(id: string) {
        return fwrapper.get({ url: baseUrl + `get-all/${id}` })
    }

    async function getAllNonValidatedCards() {
        // getAllNonValid
        // req auth
    }

    async function getLatestCardOTD() {
        return fwrapper.get({ url: baseUrl + "get-latest-card-otd" }).then(data => setLatestCC(data));
    }

    async function getAllCardsOTD() {
        // getAllCardsOTD
    }

    async function likeACard(id: string) {
        return fwrapper.patch({ url: baseUrl + `like-a-card/${id}` });
    }

    async function createACard(props: any) { // napraviti tip za creation
        /*
            public string CardName { get; set; }
            public string CardDescription { get; set; }
            public CardTypes CardType { get; set; } 
            public Stream DataStream;
            public UserAccount Owner;
            public IFormFile ImageFile { get; set; }
        */ 

        // createACard
        // body: props, multipart/form
        // req auth
    }

    async function validateACard(id: string, state: boolean) {
        // validateACard/id?state
        // req auth
    }
}

export { useCustomCardActions }