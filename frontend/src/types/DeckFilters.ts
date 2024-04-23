import { CardRegions, DeckType, SortByDate, SortByPopularity } from "../enums"

export type TDeckFilters = {
    skip: number,
    take: number,
    includeRotation?: boolean,
    regions?: CardRegions,
    types?: DeckType,
    postedBefore?: Date,
    postedAfter?: Date,
    byDate?: SortByDate,
    byPopularity?: SortByPopularity
}