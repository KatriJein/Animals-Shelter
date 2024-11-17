import style from './Account.module.css';
import Favorite from './Favorite';
import { useSelector } from 'react-redux';
import ava from '../../img/base_avatar.png';

export default function Account() {

    const animals = useSelector((state) => state.animals.animals);
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
                            <p>Иван</p>
                            <p>Иванов</p>
                            <p>+7 (123) 456-78-90</p>
                            <p>Ivan@mail.ru</p>
                        </div>
                    </div>

                </div>
                <button className={style.button}>Редактировать</button>

            </div>
            <Favorite pets={animals}/>
        </div>
    )
}