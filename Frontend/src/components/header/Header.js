import { Link, useNavigate } from 'react-router-dom';
import style from "./Header.module.css";
import logo from "../../img/logo.svg";
import { useSelector } from 'react-redux';
import { capitalizeFirstLetter } from '../../utils/animalInfo';
import { useState } from 'react';

export default function Header() {
    const navigate = useNavigate();
    const user = useSelector(state => state.user);
    const textButton = user.isAuthenticated
        ? `${capitalizeFirstLetter(user.userInfo.name)} ${capitalizeFirstLetter(user.userInfo.surname.slice(0, 1))}.`
        : 'Войти';

    const [menuOpen, setMenuOpen] = useState(false);

    const handleLoginClick = () => {
        if (!user.isAuthenticated) {
            navigate('/login');
        } else {
            navigate('/account');
        }
    };

    const toggleMenu = () => {
        setMenuOpen(!menuOpen);
    };

    return (
        <header className={style.header}>
            <nav className={style.containerList}>
                <Link to="/catalog" className={style.a}>Питомцы</Link>
                <Link to="/news" className={style.a}>Новости</Link>
                <Link to="/" className={style.a}>
                    <img src={logo} alt="logo" className={style.logo} />
                </Link>
                <Link to="/useful" className={style.a}>Полезное</Link>
                <Link to="/help" className={style.a}>Помощь</Link>
            </nav>
            <button className={style.button} onClick={handleLoginClick}>{textButton}</button>
            <button className={style.menuButton} onClick={toggleMenu}>
                ☰
            </button>
            <Link to="/" className={style.main}></Link>
            {menuOpen && (
                <nav className={style.mobileMenu}>
                    <Link to="/catalog" className={style.a} onClick={toggleMenu}>Питомцы</Link>
                    <Link to="/news" className={style.a} onClick={toggleMenu}>Новости</Link>
                    <Link to="/useful" className={style.a} onClick={toggleMenu}>Полезное</Link>
                    <Link to="/help" className={style.a} onClick={toggleMenu}>Помощь</Link>
                </nav>
            )}
        </header>
    );
}
