import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

let prevX = 0, prevY = 0;
let x = 0, y = 0;
let trackingSpeed = 0.1;
const circleElem = document.getElementById("tracking-circle")!;

document.body.addEventListener("mousemove", e => {
    prevX = e.x;
    prevY = e.y;
});

function updateBackground() {
    x = lerp(x, prevX, trackingSpeed);
    y = lerp(y, prevY, trackingSpeed);

    circleElem.style.setProperty("--bg-grad-x", `${x}px`);
    circleElem.style.setProperty("--bg-grad-y", `${y}px`);

    requestAnimationFrame(updateBackground);
}

function lerp(a: number, b: number, t: number) {
    return a + (b - a) * t;
}

updateBackground();

root.render(
    <React.StrictMode>
        <App />
    </React.StrictMode>
);