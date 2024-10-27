import style from "./Card.module.css";
import favorite from "../../../img/favorite.svg";
import animal from "../../../img/animal.png";

export default function Card(props) {
    const { pet } = props;
    const { name, breed, age, sex } = pet;

    return (
        <div className={style.card}>
            <img src={pet.mainImageSrc} alt="Картинка животного" className={style.img} />
            <div className={style.containerInfo}>
                <div className={style.containerName}>
                    <p className={`${style.p} ${style.name}`}>{name}</p>
                    <p className={`${style.p} ${style.breed}`}>{breed}</p>
                </div>
                <p className={style.description}>{age} года, {sex}</p>
                <div className={style.containerButtons}>
                    <button className={`${style.button} ${style.buttonMore}`}>Подробнее</button>
                    <button className={`${style.button}`}><img src={favorite} alt="избранное" className={style.favorite} /></button>
                </div>
            </div>
        </div>
    );
}