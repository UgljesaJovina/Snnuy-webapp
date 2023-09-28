import React, { useState } from 'react';
import { Navigate, Route, Routes } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import { Account, CustomCards, Decks, Header, Home } from './Components';
import "./styles/main.css"
import { faUser } from '@fortawesome/free-regular-svg-icons';
import { library } from '@fortawesome/fontawesome-svg-core';
import { TNavigations } from './types';

library.add(faUser);

function App() {
    const [currentTab, setCurrentTab] = useState<TNavigations>("home");

    return (
        <BrowserRouter>
            <Routes>
                <Route path='/' element={<Header currentlyActive={currentTab} />}>
                    <Route index element={<Navigate to={"/home"} />} />
                    <Route path='/home' element={<Home />} />
                    <Route path='/custom-cards' element={<CustomCards />} />
                    <Route path='/decks' element={<Decks />} />
                    <Route path='/account' element={<Account />} />
                    <Route path='*' element={<Navigate to={"/home"} />} />
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