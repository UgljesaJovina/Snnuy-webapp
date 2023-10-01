import { atom } from "recoil";
import Cookies from "universal-cookie";

const cookies = new Cookies();

const authAtom = atom<string>({
    key: "auth",
    default: cookies.get("auth") ?? ""
})

export { authAtom };