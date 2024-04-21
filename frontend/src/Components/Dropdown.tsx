import React, { Dispatch, ReactNode, SetStateAction, useContext, useEffect, useRef, useState } from 'react'
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
    const { open, setOpen } = useContext(DropdownContext);
    const div = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const close = () => setOpen(false);

        const handleClickOutside = (e: MouseEvent) => {
            const boundingBox = div.current?.getBoundingClientRect();

            if (boundingBox && boundingBox.height > 10 
                && (e.clientX < boundingBox.left || e.clientY < boundingBox.top || e.clientX > boundingBox.right || e.clientY > boundingBox.bottom))
                    close();
        }

        if (open) document.addEventListener('click', handleClickOutside);

        return () => document.removeEventListener('click', handleClickOutside);

    }, [open])

    return (
        <div className={`dropdown-content ${open ? "open" : ""}`} style={style} ref={div} >
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

const DropdownItem: React.FC<{ children: ReactNode, style?: React.CSSProperties, name: string, value: any, index?: boolean }> = ({ children, style, name, value, index }) => {
    const { selected, setSelected, setValue, multichoice } = useContext(DropdownContext);
    const clicked = selected.some(x => x === name);

    useEffect(() => {
        if (index) {
            setSelected([name]);
            setValue([value]);
        }
    }, [])

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