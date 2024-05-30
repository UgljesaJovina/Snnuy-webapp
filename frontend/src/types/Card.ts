import { CardRarity, CardRegions, CardTypes } from "../enums";

type TCard = {
    cardCode: string,
    cardName: string,
    manaCost: number,
    attackPower: number,
    healthValue: number,
    cardImageLink: string,
    cardBackgroundLink: string,
    standard: boolean,
    regions: CardRegions,
    type: CardTypes,
    rarity: CardRarity,
    deckRegions: string
}

export const defaultCard: TCard = {
    cardCode: "",
    cardName: "",
    manaCost: 0,
    attackPower: 0,
    healthValue: 0,
    cardImageLink: "",
    cardBackgroundLink: "",
    standard: false,
    regions: CardRegions.None,
    type: CardTypes.None,
    rarity: 0,
    deckRegions: ""
}

export type { TCard };