import style from './Account.module.css';
import Favorite from './Favorite';
import { useSelector } from 'react-redux';
import ava from '../../img/base_avatar.png';
import { useDispatch } from 'react-redux';
import { logout } from '../../store/userSlice';
import edit from '../../img/edit.svg';


export default function Account() {
    const dispatch = useDispatch();
    const user = useSelector((state) => state.user.userInfo);
    const isAuthenticated = useSelector((state) => state.user.isAuthenticated);

    const logoutAcc = () => {
        dispatch(logout());
    }

    const favouritesPets = useSelector((state) => state.user.favourites);

    return (
        <div className={style.mainContainer}>
            <h1 className={style.h1}>Личный кабинет</h1>
            <div className={style.containerInfo}>
                <div className={style.container}>
                    <img src={ava} alt="аватарка" />
                    <div className={style.info}>
                        <div className={style.contact}>
                            <span className={style.name}>Имя</span>
                            <span className={style.desc}>{user.name}</span>
                        </div>
                        <div className={style.contact}>
                            <span className={style.name}>Фамилия</span>
                            <span className={style.desc}>{user.surname}</span>
                        </div>
                        <div className={style.contact}>
                            <span className={style.name}>Телефон</span>
                            <span className={style.desc}>{user.phone ? user.phone : 'Не указан'}</span>
                        </div>
                        <div className={style.contact}>
                            <span className={style.name}>Почта</span>
                            <span className={style.desc}>{user.email ? user.email : 'Не указана'}</span>
                        </div>
                    </div>
                    <button className={style.button} onClick={logoutAcc}>
                        <span>Редактировать</span>
                        <img src={edit} alt="edit" />
                    </button>
                </div>
                <Favorite pets={favouritesPets} />

            </div>

        </div>
    )
}