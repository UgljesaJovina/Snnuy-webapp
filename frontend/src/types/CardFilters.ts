import { CardRegions, CardTypes, SortByDate, SortByPopularity } from "../enums"

type TCardFilter = {
    skip: number,
    take: number,
    regions?: CardRegions,
    type?: CardTypes,
    releasedBefore?: Date,
    releasedAfter?: Date,
    byDate?: SortByDate,
    byPopularity?: SortByPopularity
}

export type { TCardFilter };