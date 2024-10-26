import style from "./Card.module.css";
import favorite from "../../../img/favorite.svg";
import animal from "../../../img/animal.png";

export default function Card(props) {
    const { pet } = props;

    return (
        <div className={style.card}>
            <img src={animal} alt="Картинка животного" className={style.img} />
            <div className={style.containerInfo}>
                <div className={style.containerName}>
                    <p className={`${style.p} ${style.name}`}>{pet.name}</p>
                    <p className={`${style.p} ${style.breed}`}>{pet.breed}</p>
                </div>
                <p className={style.description}>{pet.age} года, {pet.gender}</p>
                <div className={style.containerButtons}>
                    <button className={`${style.button} ${style.buttonMore}`}>Подробнее</button>
                    <button className={`${style.button}`}><img src={favorite} alt="избранное" className={style.favorite} /></button>
                </div>
            </div>
        </div>
    );
}