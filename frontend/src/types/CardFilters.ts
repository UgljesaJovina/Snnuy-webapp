import { CardRegions, CardTypes, SortByDate, SortByPopularity } from "../enums"

export type TCardFilter = {
    skip: number,
    take: number,
    regions?: CardRegions,
    cardTypes?: CardTypes,
    postedBefore?: Date,
    postedAfter?: Date,
    byDate?: SortByDate,
    byPopularity?: SortByPopularity
}
