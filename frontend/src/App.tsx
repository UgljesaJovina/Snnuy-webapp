import React, { createRef, useEffect, useState } from 'react';
import { Navigate, Route, Routes } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import { CustomCards, Decks, Header, Home } from './components/Components';
import "./styles/main.css"

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path='/' element={<Header />}>
                    <Route index element={<Navigate to={"/home"} />} />
                    <Route path='/home' element={<Home />} />
                    <Route path='/custom-cards' element={<CustomCards />} />
                    <Route path='/decks' element={<Decks />} />
                </Route>
            </Routes>
        </BrowserRouter>
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