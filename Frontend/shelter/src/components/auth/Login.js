import { useState } from 'react';
import style from './Auth.module.css';
import { useDispatch } from 'react-redux';
import { fetchFavourites, loginAsync } from '../../store/userSlice';
import { Link, useNavigate } from 'react-router-dom';

export default function Login() {
    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const [errors, setErrors] = useState({ login: '', password: '' });
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const validateForm = () => {
        const newErrors = {};

        if (!login.trim()) {
            newErrors.login = 'Поле логина не может быть пустым';
        }
        if (!password.trim()) {
            newErrors.password = 'Поле пароля не может быть пустым';
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        if (!validateForm()) return;
    
        dispatch(
            loginAsync({ login: login.trim(), password: password.trim() })
        )
            .unwrap()
            .then((data) => {
                dispatch(fetchFavourites(data.userInfo.id));
                navigate('/account', { replace: true });
            })
            .catch((error) => {
                setErrors({ password: error });
            });
    };

    // const handleSubmit = async (e) => {
    //     e.preventDefault();

    //     if (!validateForm()) return;

    //     try {
    //         const response = await fetch(`${process.env.REACT_APP_API_URL}/auth`, {
    //             method: 'POST',
    //             headers: {
    //                 'Content-Type': 'application/json',
    //             },
    //             body: JSON.stringify({ login: login.trim(), password: password.trim() }),
    //         });

    //         if (!response.ok) {
    //             const errorData = await response.json();
    //             setErrors({ password: errorData.message || 'Ошибка сервера' });
    //         } else {
    //             const data = await response.json();
    //             console.log(data);

    //             dispatch(loginSuccess({
    //                 id: data.userInfo.id,
    //                 isAdmin: data.userInfo.isAdmin, 
    //                 userInfo: data.userInfo,  
    //             }));
    //             dispatch(fetchFavourites(data.userInfo.id));
    //             navigate('/account');

    //         }
    //     } catch (error) {
    //         setErrors({  password: error.message || 'Ошибка сервера' });
    //     }
    // };

    return (
        <div className={style.container}>
            <div className={style.containerImg} />
            <div className={style.containerForm}>
                <form className={style.form} onSubmit={handleSubmit}>
                    <div>
                        <h1 className={style.h1}>Вход</h1>
                        <p className={style.welcome}>Добро пожаловать снова! Пожалуйста, войдите в свой аккаунт.</p>
                    </div>
                    <div className={style.containerInput}>
                        <label htmlFor="login">Логин (почта / номер телефона)</label>
                        <input
                            type="text"
                            placeholder="Введите логин"
                            id="login"
                            className={style.input}
                            aria-label="Логин"
                            value={login}
                            onChange={(e) => setLogin(e.target.value)}
                        />
                        <p className={style.error} id="loginError" aria-live="assertive">{errors.login}</p>
                    </div>
                    <div className={style.containerInput}>
                        <label htmlFor="password">Пароль</label>
                        <input
                            type="password"
                            placeholder="Введите пароль"
                            id="password"
                            className={style.input}
                            aria-label="Пароль"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        <p className={style.error} id="passwordError" aria-live="assertive">{errors.password}</p>
                    </div>
                    <div className={style.containerRemember}>
                        <div className={style.containerCheckbox}>
                            <input
                                type="checkbox"
                                id="myCheckbox"
                                name="myCheckbox"
                                className={style.checkbox}
                            />
                            <label htmlFor="myCheckbox" className={style.labelCheckbox}>
                                Запомнить меня
                            </label>
                        </div>
                        <a href="#" className={style.forgotPassword}>Забыли пароль?</a>
                    </div>
                    <div>
                        <button type="submit" className={style.button}>Войти</button>
                        <p className={style.noAccount}>
                            Нет аккаунта? <Link to="/register" className={style.noAccountLink}>Зарегистрируйтесь</Link>
                        </p>
                    </div>
                </form>
            </div>
        </div>
    );
}
