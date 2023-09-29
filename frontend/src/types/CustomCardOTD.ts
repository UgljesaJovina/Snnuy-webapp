import { TCustomCard, defaultCustomCard } from "./CustomCard"
import { TUserShort } from "./UserShortObject"

type TCustomCardOTD = {
    id: string,
    card: TCustomCard,
    settingDate: Date,
    setAutomatically: boolean,
    settingUser?: TUserShort
}

export const defaultCustomCardOTD: TCustomCardOTD = {
    id: "",
    card: defaultCustomCard,
    settingDate: new Date(),
    setAutomatically: true
}

export type { TCustomCardOTD }