import style from './Auth.module.css';

export default function Registration() {
    return (
        <div className={style.container}>
            <div className={style.containerImg} />
            <div className={style.containerForm}>
                <div className={style.form}>
                    <div>
                        <h1 className={style.h1}>Регистрация</h1>
                        <p className={style.welcome}>Добро пожаловать! Пожалуйста, введите свой данные для создания аккаунта.</p>
                    </div>
                    <div className={style.containerInput}>
                        <label for="login">Логин (почта / номер телефона)</label>
                        <input type="text" placeholder="Введите логин" id="login" className={style.input} />
                    </div>
                    <div className={style.containerInput}>
                        <label for="password">Пароль</label>
                        <input type="password" placeholder="Введите пароль" id="password" className={style.input} />
                    </div>
                    <div className={style.containerInput}>
                        <label for="password">Повторите пароль</label>
                        <input type="password" placeholder="Введите пароль" id="password" className={style.input} />
                    </div>
                    <div>
                        <button href="#" className={style.button}>Зарегистрироваться</button>
                        <p className={style.noAccount}>Уже есть аккаунт? <a href="#" className={style.noAccountLink}>Войдите</a></p>
                    </div>
                </div>
            </div>
        </div>
    )
}