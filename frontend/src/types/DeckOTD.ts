import { defaultDeck, TDeck } from "./Deck"
import { TUserShort } from "./UserShortObject"

export type TDeckOTD = {
    id: string,
    deck: TDeck,
    settingDate: Date,
    setAutomatically: boolean,
    settingUser?: TUserShort
}

export const defaultDeckOTD: TDeckOTD = {
    id: "",
    deck: defaultDeck,
    settingDate: new Date(),
    setAutomatically: true
}
