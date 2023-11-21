import { atom } from "recoil";
import { defaultCustomCardOTD } from "../Types";

const LatestCustomCardAtom = atom({
    key: "latestCustomCardAtom",
    default: defaultCustomCardOTD
});

export { LatestCustomCardAtom }