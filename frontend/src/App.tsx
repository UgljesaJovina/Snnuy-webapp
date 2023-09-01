import React, { useEffect, useState } from 'react';

function App() {

    const [imgData, setData] = useState<string | ArrayBuffer | null>("");

    useEffect(() => {
        fetch("http://localhost:5016/WeatherForecast")
        .then(data => data.blob())
        .then(blob => {
            const reader = new FileReader();
            reader.readAsDataURL(blob);
            reader.onloadend = () => {
                const base64 = reader.result;
                if (base64) setData(base64);
            }
        })
    })

    return (
        <img src={`${imgData}`} width={64} />
    );
}

export default App;
