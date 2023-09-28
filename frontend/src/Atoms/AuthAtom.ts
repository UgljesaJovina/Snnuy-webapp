import { atom } from "recoil";
import Cookies, { CookieGetOptions } from "universal-cookie";

const cookies = new Cookies()

const authAtom = atom({
    key: "auth",
    default: cookies.get("auth")
})

export { authAtom };