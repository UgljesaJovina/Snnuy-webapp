import { atom } from "recoil";
import { defaultDeckOTD } from "../types";

const LatestDeckAtom = atom({
    key: "latestDeckAtom",
    default: defaultDeckOTD
});

export { LatestDeckAtom }