import { atom } from "recoil";
import { TUser, defaultUser } from "../types";

const userAtom = atom<TUser>({
    key: "user",
    default: defaultUser
});

export { userAtom }