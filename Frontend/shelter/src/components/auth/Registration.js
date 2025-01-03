import { useState } from 'react';
import style from './Auth.module.css';
import { useDispatch } from 'react-redux';
import { register } from '../../store/userSlice';
import { Link, useNavigate } from 'react-router-dom';

export default function Registration() {
    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const [passwordRepeat, setPasswordRepeat] = useState('');
    const [errors, setErrors] = useState({ login: '', password: '', passwordRepeat: '' });
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const validateForm = () => {
        const newErrors = {};

        if (!login.trim()) {
            newErrors.login = 'Поле логина не может быть пустым';
        }
        if (!password.trim()) {
            newErrors.password = 'Поле пароля не может быть пустым';
        } else if (password.trim().length < 8) {
            newErrors.password = 'Пароль должен содержать не менее 8 символов';
        }
        if (password !== passwordRepeat) {
            newErrors.passwordRepeat = 'Пароли не совпадают';
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        if (!validateForm()) return;
    
        dispatch(
            register({ login: login.trim(), password: password.trim() })
        )
            .unwrap()
            .then(() => {
                navigate('/fillingData', { replace: true });
            })
            .catch((error) => {
                setErrors({ passwordRepeat: error });
            });
    };
    
    return (
        <div className={style.container}>
            <div className={style.containerImg} />
            <div className={style.containerForm}>
                <form className={style.form} onSubmit={handleSubmit}>
                    <div>
                        <h1 className={style.h1}>Регистрация</h1>
                        <p className={style.welcome}>Добро пожаловать! Пожалуйста, введите свои данные для создания аккаунта.</p>
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
                    <div className={style.containerInput}>
                        <label htmlFor="passwordRepeat">Повторите пароль</label>
                        <input
                            type="password"
                            placeholder="Введите пароль"
                            id="passwordRepeat"
                            className={style.input}
                            aria-label="Повторите пароль"
                            value={passwordRepeat}
                            onChange={(e) => setPasswordRepeat(e.target.value)}
                        />
                        <p className={style.error} id="passwordRepeatError" aria-live="assertive">{errors.passwordRepeat}</p>
                    </div>
                    <div>
                        <button type="submit" className={style.button}>Зарегистрироваться</button>
                        <p className={style.noAccount}>Уже есть аккаунт? <Link to="/login" className={style.noAccountLink}>Войдите</Link></p>
                    </div>
                </form>
            </div>
        </div>
    );
}
