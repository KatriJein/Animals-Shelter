import style from "./Header.module.css";
import logo from "../../img/logo.svg";
import Button from "../button/Button";

export default function Header() {
    return (
        <header className={style.header}>
            <nav className={style.containerList}>
                <a href="#" className={style.a}>Питомцы</a>
                <a href="#" className={style.a}>Новости</a>
                <a href="#" className={style.a}><img src={logo} alt="logo" className={style.logo} /></a>
                <a href="#" className={style.a}>Полезное</a>
                <a href="#" className={style.a}>Помощь</a>
            </nav>

            {/* <Button text={"Войти"} styleProps={style.button} /> */}
            <button className={style.button}>Войти</button>
        </header>
    );
}