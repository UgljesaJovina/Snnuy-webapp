import { atom } from "recoil";
import { TUser, defaultUser } from "../types";

const userAtom = atom<TUser>({
    key: "users",
    default: defaultUser
});

export { userAtom }