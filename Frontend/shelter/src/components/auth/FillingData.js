import { useState } from 'react';
import style from './Auth.module.css';
import { useDispatch, useSelector } from 'react-redux';
import { changes } from '../../store/userSlice';
import { Link, useNavigate } from 'react-router-dom';

export default function FillingData() {
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [errors, setErrors] = useState({ name: '', surname: '' });
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const user = useSelector(state => state.user.userInfo);

    const validateForm = () => {
        const newErrors = {};

        if (!name.trim()) {
            newErrors.login = 'Поле имени не может быть пустым';
        }
        if (!surname.trim()) {
            newErrors.password = 'Поле фамилии не может быть пустым';
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!validateForm()) return;
        const userChanges = {
            name: name.trim(),
            surname: surname.trim(),
            email: 'mail321@mail.com',
            phone: "+79220045000"
        };

        try {
            const response = await fetch(`${process.env.REACT_APP_API_URL}/users/${user.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userChanges),
            });

            if (!response.ok) {
                const errorData = await response.json();
                console.log(errorData, 'error');
                setErrors({ surname: errorData.message || 'Ошибка сервера' });
            } else {
                // const data = await response.json();
                dispatch(changes(userChanges));
                navigate('/account');
            }
        } catch (error) {
            console.log(error, 'errpr');
            setErrors({ ...errors, passwordRepeat: 'Ошибка сети. Пожалуйста, попробуйте снова.' });
        }
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
                        <label htmlFor="name">Имя</label>
                        <input
                            type="text"
                            placeholder="Введите имя"
                            id="name"
                            className={style.input}
                            aria-label="Имя"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                        />
                        <p className={style.error} id="nameError" aria-live="assertive">{errors.name}</p>
                    </div>
                    <div className={style.containerInput}>
                        <label htmlFor="surname">Фамилия</label>
                        <input
                            type="surname"
                            placeholder="Введите фамилию"
                            id="surname"
                            className={style.input}
                            aria-label="Фамилия"
                            value={surname}
                            onChange={(e) => setSurname(e.target.value)}
                        />
                        <p className={style.error} id="surnameError" aria-live="assertive">{errors.surname}</p>
                    </div>
                    <div>
                        <button type="submit" className={style.button}>Подтвердить</button>
                    </div>
                </form>
            </div>
        </div>
    );
}
