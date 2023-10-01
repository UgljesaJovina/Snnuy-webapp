import { Navigate, Route, Routes } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import { Account, CustomCards, Decks, Header, Home, Login, Register } from './Components';
import "./styles/mainStyle.css";
import "./styles/customCardStyle.css";
import "./styles/homeStyle.css";
import "./styles/loginStyle.css";
import { faUser } from '@fortawesome/free-regular-svg-icons';
import { faTriangleExclamation, faSpinner } from '@fortawesome/free-solid-svg-icons';
import { library } from '@fortawesome/fontawesome-svg-core';
import { RecoilRoot } from 'recoil';

library.add(faUser, faTriangleExclamation, faSpinner);

function App() {
    return (
        <RecoilRoot>
            <BrowserRouter>
                <Routes>
                    <Route path='/' element={<Header />}>
                        <Route index element={<Navigate to={"/home"} />} />
                        <Route path='/home' element={<Home />} />
                        <Route path='/custom-cards' element={<CustomCards />} />
                        <Route path='/decks' element={<Decks />} />
                        <Route path='/account/:id?' element={<Account />} />
                        <Route path='*' element={<Navigate to={"/home"} />} />
                    </Route>
                    <Route path='/login' element={<Login />} />
                    <Route path='/register' element={<Register />} />
                </Routes>
            </BrowserRouter>
        </RecoilRoot>
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