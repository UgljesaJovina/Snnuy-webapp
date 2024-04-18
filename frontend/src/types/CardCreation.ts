import { CardRegions, CardTypes } from "../enums"

export type TCardCreation = {
    cardName: string,
    cardDescription: string,
    regions: CardRegions,
    cardType: CardTypes,
    imageFile: string
}