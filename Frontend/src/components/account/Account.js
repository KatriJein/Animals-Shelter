import style from './Account.module.css';
import Favorite from './Favorite';
import { useSelector } from 'react-redux';
import ava from '../../img/base_avatar.png';
import edit from '../../img/edit.svg';
import { useNavigate } from 'react-router-dom';


export default function Account() {
    const navigate = useNavigate();
    const user = useSelector((state) => state.user.userInfo);

    const handleChange = () => {
        navigate('/account/change');
    }

    const favouritesPets = useSelector((state) => state.user.favourites);

    return (
        <div className={style.mainContainer}>
            <h1 className={style.h1}>Личный кабинет</h1>
            <div className={style.containerInfo}>
                <div className={style.container}>
                    <img src={user.avatarSrc || ava} alt="аватарка" />
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
                    <button className={style.button} onClick={handleChange}>
                        <span>Редактировать</span>
                        <img src={edit} alt="edit" />
                    </button>
                </div>
                <Favorite pets={favouritesPets} />

            </div>

        </div>
    )
}