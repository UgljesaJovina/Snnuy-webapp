import { atom } from "recoil";
import { defaultCustomCardOTD } from "../types";

const LatestCustomCardAtom = atom({
    key: "latestCustomCardAtom",
    default: defaultCustomCardOTD
});

export { LatestCustomCardAtom }