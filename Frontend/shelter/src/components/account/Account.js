import style from './Account.module.css';
import Favorite from './Favorite';
import { useSelector } from 'react-redux';
import ava from '../../img/base_avatar.png';
import { useDispatch } from 'react-redux';
import { logout } from '../../store/userSlice';

export default function Account() {
    const dispatch = useDispatch();
    const user = useSelector((state) => state.user.userInfo);
    const isAuthenticated = useSelector((state) => state.user.isAuthenticated);

    const logoutAcc = () => {
        dispatch(logout());
    }

    if (!isAuthenticated) {
        return
    }

    const favouritesPets = useSelector((state) => state.user.favourites);
    console.log(user)

    return (
        <div className={style.mainContainer}>
            <h1 className={style.h1}>Личный кабинет</h1>
            <div className={style.containerInfo}>
                <div className={style.container}>
                    <div className={style.avatar}>
                        <img src={ava} alt="аватарка" />
                        <a href="#" className={style.change}>Изменить</a>
                    </div>
                    <div className={style.info}>
                        <div className={style.infoName}>
                            <p>Имя</p>
                            <p>Фамилия</p>
                            <p>Телефон</p>
                            <p>Почта</p>
                        </div>
                        <div className={style.infoName}>
                            <p>{user.name}</p>
                            <p>{user.surname}</p>
                            <p>{user.phone || 'не указан'}</p>
                            <p>{user.email || 'не указан'}</p>
                        </div>
                    </div>

                </div>
                <button className={style.button}>Редактировать</button>

            </div>
            <Favorite pets={favouritesPets} />
            <button onClick={logoutAcc}>Выйти</button>
        </div>
    )
}