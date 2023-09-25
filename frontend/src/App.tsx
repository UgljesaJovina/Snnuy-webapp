import React, { createRef, useEffect, useState } from 'react';
import Card from './types/Card';

function App() {

    const [deckCards, setDeckCards] = useState<Card[]>([]);

    useEffect(() => {
        fetch("https://bsp6kg95-5016.euw.devtunnels.ms/deck/getdeckinfo/007d8d0a-eb5e-4b9d-8c35-1d4f26e81b0b")
        .then(data => data.json())
        .then(data => setDeckCards(data));
    }, []);

    return (
        
    );
}

export default App;


// fetch("http://localhost:5016/WeatherForecast")
// .then(data => data.blob())
// .then(blob => {
//     const reader = new FileReader();
//     reader.readAsDataURL(blob);
//     reader.onloadend = () => {
//         const base64 = reader.result;
//         if (base64) setData(base64);
//     }
// }) iscitavanje slike koja mora biti encodirana kao base-64 string