import { CardRegions, DeckType, SortByDate, SortByPopularity } from "../enums"

export type TDeckFilters = {
    skip: number,
    take: number,
    includeEternal?: boolean,
    regions?: CardRegions,
    deckTypes?: DeckType,
    postedBefore?: Date,
    postedAfter?: Date,
    byDate?: SortByDate,
    byPopularity?: SortByPopularity
}