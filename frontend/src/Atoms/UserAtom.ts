import { atom } from "recoil";
import { TUser, defaultUser } from "../Types";

const userAtom = atom<TUser>({
    key: "users",
    default: defaultUser
});

export { userAtom }