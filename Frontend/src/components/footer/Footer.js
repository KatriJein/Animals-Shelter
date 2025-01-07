import style from "./Footer.module.css";
import logo from "../../img/footer_logo.svg";
import { Link } from "react-router-dom";

export default function Footer() {
    return (
        <footer className={style.mainContainer}>
            <div className={style.container}>
                <img src={logo} alt="Логотип приюта" className={style.logo} />
                <nav className={style.nav}>
                    <Link to="/" className={style.navLink}>Главная</Link>
                    <Link to="/catalog" className={style.navLink}>Питомцы</Link>
                    <Link to="/news" className={style.navLink}>Новости</Link>
                    <Link to="/useful" className={style.navLink}>Полезное</Link>
                    <Link to="/help" className={style.navLink}>Помощь</Link>
                </nav>
                <p className={style.copyright}>@ Приют «Лапочка». Все права защищены</p>
            </div>
        </footer>
    );
}