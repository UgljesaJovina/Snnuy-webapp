type Card = {
    count: number,
    cardName: string,
    manaCost: number,
    attackPower: number,
    healthValue: number,
    cardImageLink: string,
    standard: boolean,
    regions: string,
    type: string,
    rarity: number,
    deckRegions: string
}

export const defaultCard: Card = {
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

export type { Card };