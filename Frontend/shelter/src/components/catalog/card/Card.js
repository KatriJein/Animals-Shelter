import style from "./Card.module.css";
import favorite from "../../../img/favorite.svg";
import animal from "../../../img/animal.png";
import { FilterOptions } from "../../../filterOptions";

export default function Card(props) {
    const { pet } = props;
    const { name, breed, age, sex } = pet;
    const sexStr = FilterOptions['sex']['options'][sex].charAt(0).toLowerCase() + FilterOptions['sex']['options'][sex].slice(1);

    return (
        <div className={style.card}>
            <img src={pet.mainImageSrc} alt="Картинка животного" className={style.img} />
            <div className={style.containerInfo}>
                <div className={style.containerName}>
                    <p className={style.name}>{name}</p>
                    <p className={style.description}>{breed}, {age} года, {sexStr}</p>
                </div>
                <p className={style.text}>Наша самая нежная и спокойная девочка, очень ласковая и любит детей</p>
                <div className={style.containerButtons}>
                    <button className={style.buttonMore}>Подробнее</button>
                    <button className={style.buttonFavorite}><img src={favorite} alt="избранное" className={style.favorite} /></button>
                </div>
            </div>
        </div>
    );
}