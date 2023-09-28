import { atom } from "recoil";

const userAtom = atom({
    key: "users",
    default: null
});

export { userAtom }