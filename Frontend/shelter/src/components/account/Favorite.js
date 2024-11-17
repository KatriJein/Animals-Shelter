import style from './Account.module.css';
import { Link } from "react-router-dom";

export default function Favorite(props) {
    const { pets } = props;

    return (
        <div className={style.containerFavorite}>
            <h2 className={style.h2}>Ваше избранное</h2>
            <div className={style.containerPets}>
                {pets.map((pet) => (
                    <Link to={`/animal/${pet.id}`} key={pet.id} className={style.pet}>
                        <img src={pet.mainImageSrc} alt="Картинка животного" className={style.img} />
                        <p>{pet.name}</p>
                    </Link>))}
            </div>
        </div>
    )
}