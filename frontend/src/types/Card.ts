import { CardRarity } from "../enums";

type TCard = {
    count: number,
    cardName: string,
    manaCost: number,
    attackPower: number,
    healthValue: number,
    cardImageLink: string,
    standard: boolean,
    regions: string,
    type: string,
    rarity: CardRarity,
    deckRegions: string
}

export const defaultCard: TCard = {
    count: 0,
    cardName: "",
    manaCost: 0,
    attackPower: 0,
    healthValue: 0,
    cardImageLink: "",
    standard: false,
    regions: "",
    type: "",
    rarity: 0,
    deckRegions: ""
}

export type { TCard };