import { useCallback, useEffect, useLayoutEffect, useRef } from "react";

function useKreyPress(keys: string[], callback: (e: KeyboardEvent) => void, node?: HTMLElement) {
    const callbackRef = useRef(callback);

    useLayoutEffect(() => {
        callbackRef.current = callback;
    });

    const handleKeyPress = useCallback((e: any) => {
        if (keys.some(key => e.key === key))
            callbackRef.current(e);
    }, [keys]);
    
    useEffect(() => {
        const targetNode = node ?? document;
        targetNode && targetNode.addEventListener("keydown", handleKeyPress);

        return () => targetNode.removeEventListener("keydown", handleKeyPress);
    }, [handleKeyPress, node]);
}

export { useKreyPress }