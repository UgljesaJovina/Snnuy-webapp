import { Navigate, Route, Routes } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import { Account, CustomCards, Decks, Sidebar, Home, Login, Register } from './paths';
import "./styles/mainStyle.css";
import "./styles/customCardStyle.css";
import "./styles/homeStyle.css";
import "./styles/loginStyle.css";
import "./styles/filterStyle.css";
import "./styles/createCardStyle.css";
import "./styles/dropdownStyle.css";
import "./styles/modalStyle.css";
import "./styles/deckStyle.css";
import "./styles/deckDisplayStyle.css";
import "./styles/cardStyle.css";
import { faUser } from '@fortawesome/free-regular-svg-icons';
import { faTriangleExclamation, faSpinner, faArrowLeft, faPlus, faChevronDown, faCheck, faCopy } from '@fortawesome/free-solid-svg-icons';
import { library } from '@fortawesome/fontawesome-svg-core';
import { RecoilRoot, useRecoilValue } from 'recoil';
import { userAtom } from './atoms';
import { useEffect } from 'react';
import { useUserActions } from './actions';

library.add(faUser, faTriangleExclamation, faSpinner, faArrowLeft, faPlus, faChevronDown, faCheck, faCopy);

function App() {
    return (
        <RecoilRoot>
            <LoadUser></LoadUser>
            <BrowserRouter>
                <Routes>
                    <Route path='/' element={<Sidebar />}>
                        <Route index element={<Navigate to={"/home"} />} />
                        <Route path='/home' element={<Home />} />
                        <Route path='/custom-cards' element={<CustomCards />} />
                        <Route path='/decks' element={<Decks />} />
                        <Route path='/account' element={<Account />} />
                        <Route path='/users/:id?' element={<Home />} />
                        <Route path='*' element={<Navigate to={"/home"} />} />
                    </Route>
                    <Route path='/login' element={<Login />} />
                    <Route path='/register' element={<Register />} />
                    <Route path='*' element={<Navigate to={"/home"} />} />
                </Routes>
            </BrowserRouter>
        </RecoilRoot>
    );
}

const LoadUser: React.FC = () => {
    const user = useRecoilValue(userAtom);
    const userActions = useUserActions();

    useEffect(() => {
        if (!user.username)
            userActions.getMyInfo().catch(err => console.log(err));
    }, []);

    return <></>;
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