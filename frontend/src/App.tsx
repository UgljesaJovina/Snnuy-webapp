import React, { createRef, useEffect, useState } from 'react';

function App() {

    const [imgData, setData] = useState<string | ArrayBuffer | null>("");
    const nameInput = createRef<HTMLInputElement>();
    const passInput = createRef<HTMLInputElement>();
    const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2OTM3MzU5MTksImV4cCI6MTY5NDM0MDcxOSwiaWF0IjoxNjkzNzM1OTE5fQ.LDd8_S92IRMM1LpDS0_8Jp6NUaA8N4nM3OwgvoBDEQM";


    const handleSubmit = (e:  React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        fetch("http://localhost:4000/users/", { method: "GET", headers: { "Content-Type": "application/json", Authorization: `Bearer ${token}` }})
        .then(data => console.log(data.status));
    }

    return (
        <form onSubmit={handleSubmit}>
            <input type="text" name="username" ref={nameInput} />
            <input type="text" name="password" ref={passInput} />
            <input type="submit" />
        </form>
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