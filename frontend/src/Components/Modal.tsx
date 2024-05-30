import React, { Dispatch, SetStateAction } from "react";
import { useKreyPress } from "../hooks";

const Modal: React.FC<{ 
    children: React.ReactNode, 
    isOpen: boolean, 
    setOpen: Dispatch<SetStateAction<boolean>>, 
    contentStyle?: React.CSSProperties 
}> = ({ children, isOpen, setOpen, contentStyle }) => {

    useKreyPress(["Escape"], () => setOpen(false));

    return (
        <div className={`modal ${isOpen ? "show" : ""}`}>
            <div className="shade" onClick={() => setOpen(false)}></div>
            <div className="content" style={contentStyle}>
                {children}
            </div>
        </div>
    );
}

export { Modal };