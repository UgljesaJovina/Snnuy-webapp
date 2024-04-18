import React, { Dispatch, SetStateAction } from "react";

const DropdownContext = React.createContext<TDropdownContext>({
    open: false,
    setOpen: () => {},
    selected: [],
    setSelected: () => {},
    value: [],
    setValue: () => {},
    multichoice: false
});

type TDropdownContext = {
    open: boolean,
    setOpen: Dispatch<SetStateAction<boolean>>,
    selected: string[],
    setSelected: Dispatch<SetStateAction<string[]>>,
    value: any[],
    setValue: Dispatch<SetStateAction<any[]>>,
    multichoice: boolean
}

export { DropdownContext }