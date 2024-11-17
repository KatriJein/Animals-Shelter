import { Link, useNavigate } from 'react-router-dom';
import style from "./Header.module.css";
import logo from "../../img/logo.svg";
import { useSelector } from 'react-redux';

export default function Header() {
    const navigate = useNavigate();
    const user = useSelector(state => state.user);
    console.log(user);

    const handleLoginClick = () => {
        if (!user.isAuthenticated) {
            navigate('/login');
        } else {
            navigate('/account');
        }
    };

    return (
        <header className={style.header}>
            <nav className={style.containerList}>
                <Link to="/catalog" className={style.a}>Питомцы</Link>
                <Link to="/news" className={style.a}>Новости</Link>
                <Link to="/" className={style.a}><img src={logo} alt="logo" className={style.logo} /></Link>
                <Link to="/useful" className={style.a}>Полезное</Link>
                <Link to="/help" className={style.a}>Помощь</Link>
            </nav>
            <button className={style.button} onClick={handleLoginClick}>Войти</button>
        </header>
    );
}
