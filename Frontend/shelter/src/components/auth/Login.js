import style from './Auth.module.css';

export default function Login() {
    return (
        <div className={style.container}>
            <div className={style.containerImg} />
            <div className={style.containerForm}>
                <div className={style.form}>
                    <div>
                        <h1 className={style.h1}>Вход</h1>
                        <p className={style.welcome}>Добро пожаловать снова! Пожалуйста, войдите в свой аккаунт.</p>
                    </div>
                    <div className={style.containerInput}>
                        <label for="login">Логин (почта / номер телефона)</label>
                        <input type="text" placeholder="Введите логин" id="login" className={style.input} />
                    </div>
                    <div className={style.containerInput}>
                        <label for="password">Пароль</label>
                        <input type="password" placeholder="Введите пароль" id="password" className={style.input} />
                    </div>
                    <div className={style.containerRemember}>
                        <div className={style.containerCheckbox}>
                            <input type="checkbox" id="myCheckbox" name="myCheckbox" className={style.checkbox} />
                            <label for="myCheckbox" className={style.labelCheckbox}>Запомнить меня</label>
                        </div>
                        <a className={style.forgotPassword}>Забыли пароль?</a>
                    </div>
                    <div>
                        <button href="#" className={style.button}>Войти</button>
                        <p className={style.noAccount}>Нет аккаунта? <a href="#" className={style.noAccountLink}>Зарегистрируйтесь</a></p>
                    </div>

                </div>
            </div>
        </div>
    )
}