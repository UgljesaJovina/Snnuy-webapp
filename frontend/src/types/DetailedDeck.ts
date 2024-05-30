import { CardRegions, DeckType } from "../enums"
import { defaultCard, TCard } from "./Card"
import { defaultShortUser, TUserShort } from "./UserShortObject"

export type TDetailedDeck = {
    id: string,
    deckCode: string,
    deckName: string,
    postingDate: Date,
    standard: boolean,
    owner: TUserShort,
    deckType: DeckType,
    deckRegions: CardRegions,
    numberOfLikes: number,
    champions: TCard[],
    highestCostCard: TCard,
    regionCardCount: { [key in CardRegions]: number },
    deckContent: (TCard & { count: number })[],
    deckCost: number
}

export const defaultDetailedDeck: TDetailedDeck = {
    id: "",
    deckCode: "",
    deckName: "",
    postingDate: new Date(),
    standard: false,
    owner: defaultShortUser,
    deckType: DeckType.None,
    deckRegions: CardRegions.None,
    numberOfLikes: 0,
    champions: [],
    highestCostCard: defaultCard,
    regionCardCount: {
        0: 0,
        1: 0,
        2: 0,
        4: 0,
        8: 0,
        16: 0,
        32: 0,
        64: 0,
        128: 0,
        256: 0,
        512: 0,
        1024: 0,
        2048: 0,
        4095: 0,
    },
    deckContent: [],
    deckCost: 0
}