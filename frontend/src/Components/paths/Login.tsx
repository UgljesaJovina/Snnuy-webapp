import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useState } from "react";
import { useKreyPress } from "../../hooks";
import { Link, useNavigate } from "react-router-dom";
import { useUserActions } from "../../Actions";

const Login: React.FC = () => {
    const userActions = useUserActions();

    const [username, setUsername] = useState("");
    const [usernameError, setUsernameError] = useState("");
    const [password, setPassword] = useState("");
    const [passwordError, setPasswordError] = useState("");
    const [waitingResponse, setWaitingResponse] = useState(false);

    const navigate = useNavigate();

    const handleSubmit = async (e?: React.FormEvent<HTMLFormElement>) => {
        e?.preventDefault();
        if (username.length < 2) setUsernameError("The username must be at least 2 characters long");
        else if (username.length > 16) setUsernameError("The username can't be longer than 16 characters");
        else setUsernameError("");

        if (password.length < 8) setPasswordError("The password must be at least 8 characters long");
        else if (password.length > 40) setPasswordError("The password can't be longer than 40 characters");
        else setPasswordError("");

        if (!usernameError && !passwordError) {
            setWaitingResponse(true);
            userActions.login(username, password).then(data => navigate("/home")).catch(err => console.log(err)).finally(() => setWaitingResponse(false));
        } 
    }

    const handleKeyPress = (e: KeyboardEvent) => {
        handleSubmit();
    }

    useKreyPress(["Enter"], handleKeyPress);

    return (
        <div className="login-page">
            <div className="login-form-container">
                <h3>Login</h3>
                <hr />
                <form onSubmit={handleSubmit} className="login-form">
                    <div className="data-input">
                        <label htmlFor="username">Username</label>
                        <input type="text" name="username" id="username" required onChange={e => setUsername(e.target.value)} />
                        <div className={`input-error ${usernameError ? "active" : ""}`}>
                            <FontAwesomeIcon icon={["fas", "triangle-exclamation"]} />
                            <p>{usernameError}</p>
                        </div>
                    </div>
                    <div className="data-input">
                        <label htmlFor="password">Password</label>
                        <input type="password" name="password" id="password" required onChange={e => setPassword(e.target.value)} />
                        <div className={`input-error ${passwordError ? "active" : ""}`}>
                            <FontAwesomeIcon icon={["fas", "triangle-exclamation"]} />
                            <p>{passwordError}</p>
                        </div>
                    </div>
                    <button type="submit">{waitingResponse ? <FontAwesomeIcon icon={["fas", "spinner"]} className="spin" /> : "Login"}</button>
                </form>
                <div className="redirect">
                    <section>Don't have an account?</section>
                    <Link to={"/register"}>Register</Link>
                </div>
            </div>
        </div>
    );
}

export { Login };