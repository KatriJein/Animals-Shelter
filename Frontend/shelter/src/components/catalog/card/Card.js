import { Link, useNavigate } from 'react-router-dom';
import style from "./Card.module.css";
import favorite from "../../../img/favorite.svg";
import favoriteFull from "../../../img/favorite_full.svg";
import { FilterOptions } from "../../../filterOptions";
import { getAgeString } from '../../../utils/animalInfo';
import { useSelector, useDispatch } from 'react-redux';
import { addFavourite, removeFavourite } from '../../../store/userSlice';

export default function Card(props) {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    
    const { pet, isAuthenticated, isFavourite } = props;
    const { id, name, breed, age, sex } = pet;
    
    const user = useSelector((state) => state.user);

    const sexStr = FilterOptions['sex']['options'][sex].charAt(0).toLowerCase() + FilterOptions['sex']['options'][sex].slice(1);

    const handleClick = () => {
        if (isAuthenticated) {
            if (isFavourite) {
                dispatch(removeFavourite({ userId: user.id, petId: id }))
                    .unwrap()
                    .catch((error) => {
                        console.error('Error removing from favourites:', error);
                    });
            } else {
                dispatch(addFavourite({ userId: user.id, pet }))
                    .unwrap()
                    .catch((error) => {
                        console.error('Error adding to favourites:', error);
                    });
            }
        } else {
            navigate('/login');
        }
    };

    return (
        <div className={style.card}>
            <img src={pet.mainImageSrc} alt="Картинка животного" className={style.img} />
            <div className={style.containerInfo}>
                <div className={style.containerName}>
                    <p className={style.name}>{name}</p>
                    <p className={style.description}>{breed}, {getAgeString(age)}, {sexStr}</p>
                </div>
                <p className={style.text}>Наша самая нежная и спокойная девочка, очень ласковая и любит детей</p>
                <div className={style.containerButtons}>
                    <Link to={`/animal/${id}`} className={style.buttonMore}>
                        Подробнее
                    </Link>
                    <button className={style.buttonFavorite} onClick={handleClick}>
                        {isFavourite 
                            ? <img src={favoriteFull} alt="избранное" className={style.favorite} /> 
                            : <img src={favorite} alt="избранное" className={style.favorite} />}
                    </button>
                </div>
            </div>
        </div>
    );
}
