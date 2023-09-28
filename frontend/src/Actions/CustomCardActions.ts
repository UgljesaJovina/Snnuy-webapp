function useCustomCardActions() {
    const baseUrl = "customcards/"

    return {

    }

    function getAll() {
        // getAll
    }

    function getAllFromUser(id: string) {
        // getAllFromUser/id
    }

    function GetAllNonValidatedCards() {
        // getAllNonValid
        // req auth
    }

    function getLatestCardOTD() {
        // getLatestCardOTD
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