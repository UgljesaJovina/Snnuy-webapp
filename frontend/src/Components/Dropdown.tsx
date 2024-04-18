import React, { Dispatch, ReactNode, SetStateAction, useContext, useEffect, useState } from 'react'
import { DropdownContext } from '../contexts';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck, faChevronDown } from '@fortawesome/free-solid-svg-icons';

const Dropdown: React.FC<{ children: ReactNode, setter: Dispatch<SetStateAction<any[]>>, style?: React.CSSProperties, multichoice?: boolean }> = ({ children, setter, style, multichoice=false }) => {
    const [open, setOpen] = useState(false);
    const [selected, setSelected] = useState<string[]>([]);
    const [value, setValue] = useState<any[]>([]);

    useEffect(() => {
        setter(value);
    }, [value]);

    return (
        <DropdownContext.Provider value={{ open, setOpen, selected, setSelected, value, setValue, multichoice }}>
            <div className='dropdown' style={style}>
                {children}
            </div>
        </DropdownContext.Provider>
    );
}

const DropdownButton: React.FC<{ children: ReactNode, style?: React.CSSProperties }> = ({ children, style }) => {
    const { open, setOpen, selected, value } = useContext(DropdownContext);
    const [title, setTitle] = useState<string | ReactNode>("");

    useEffect(() => {
        if (value.length === 0) setTitle(children);
        else if (value.length === 1) setTitle(selected);
        else setTitle(`${value.length} selected`);
    }, [selected, value]);

    function toggleOpen() {
        setOpen(curr => !curr);
    }

    return (
        <div onClick={toggleOpen} style={style} className='dropdown-button'>
            <section>{title}</section>
            <FontAwesomeIcon icon={faChevronDown} className={`dropdown-button-arrow ${open ? "open" : ""}`} />
        </div>  
    );
}

const DropdownContent: React.FC<{ children: ReactNode, style?: React.CSSProperties }> = ({ children, style }) => {
    const { open } = useContext(DropdownContext);

    return (
        <div className={`dropdown-content ${open ? "open" : ""}`} style={style}>
            { children }
        </div>
    );
}

const DropdownList: React.FC<{ children: ReactNode, style?: React.CSSProperties }> = ({ children, style }) => {
    const { setOpen, multichoice } = useContext(DropdownContext);

    return (
        <div className='dropdown-list' style={style} onClick={() => { if (!multichoice) setOpen(false) }}>
            { children }
        </div>
    )
}

const DropdownItem: React.FC<{ children: ReactNode, style?: React.CSSProperties, name: string, value: any }> = ({ children, style, name, value }) => {
    const { selected, setSelected, setValue, multichoice } = useContext(DropdownContext);
    const clicked = selected.some(x => x === name);

    function handleClick() {
        if (clicked === false) {
            if (multichoice) {
                setSelected(curr => [...curr, name]);
                setValue(curr => [...curr, value]);
            } else {
                setSelected([name]);
                setValue([value]);
            }
        }
        else {
            setSelected(curr => curr.filter(x => x !== name));
            setValue(curr => curr.filter(x => x !== value));
        }
    }

    return (
        <div className='dropdown-item' style={style} onClick={handleClick}>
            {children}
            { clicked && <FontAwesomeIcon className='dropdown-check' icon={faCheck} /> }
        </div>
    )
}

export { Dropdown, DropdownButton, DropdownContent, DropdownContext, DropdownList, DropdownItem };