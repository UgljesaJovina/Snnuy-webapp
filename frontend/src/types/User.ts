import { UserPermissions } from "../enums"

type TUser = {
    id: string,
    username: string,
    permissions: UserPermissions,
    ownedCards: string[],
    likedCards: string[],
    ownedDecks: string[],
    likedDecks: string[]
}

export const defaultUser: TUser = {
    id: "",
    username: "",
    permissions: UserPermissions.None,
    ownedCards: [],
    likedCards: [],
    ownedDecks: [],
    likedDecks: []
}

export type { TUser } 