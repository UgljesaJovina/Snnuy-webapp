import { useRecoilState } from "recoil";
import { useFetchWrapper } from "../Utils/FetchWrapper"
import { TCustomCardOTD } from "../types"
import { LatestCustomCardAtom } from "../Atoms";

export { useCustomCardActions }

function useCustomCardActions() {
    const baseUrl = "customcard/";
    const fwrapper = useFetchWrapper();
    const [latestCC, setLatestCC] = useRecoilState(LatestCustomCardAtom);

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

    function getAll() {
        // getAll
    }

    function getAllFromUser(id: string) {
        // getAllFromUser/id
    }

    function getAllNonValidatedCards() {
        // getAllNonValid
        // req auth
    }

    function getLatestCardOTD() {
        return fwrapper.get(baseUrl + "GetLatestCardOTD").then(data => setLatestCC(data));
    }

    function getAllCardsOTD() {
        // getAllCardsOTD
    }

    function likeACard(id: string) {
        // likeACard/id
        //req auth
    }

    function createACard(props: any) { // napraviti tip za creation
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

    function validateACard(id: string, state: boolean) {
        // validateACard/id?state
        // req auth
    }
}