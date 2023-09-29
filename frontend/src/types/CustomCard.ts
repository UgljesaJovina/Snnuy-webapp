import { TUserShort, defaultShortUser } from "./UserShortObject";

type TCustomCard = {
    id: string,
    cardName: string,
    postingDate: Date,
    cardDescription: string,
    type: string,
    state: string,
    owner: TUserShort,
    numberOfLikes: number
}

export const defaultCustomCard: TCustomCard = {
    id: "",
    cardName: "",
    postingDate: new Date(),
    cardDescription: "",
    type: "",
    state: "",
    owner: defaultShortUser,
    numberOfLikes: 0
}

export type { TCustomCard };