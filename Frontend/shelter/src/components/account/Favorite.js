import style from './Account.module.css';
import { Link } from "react-router-dom";
import trash from '../../img/trash.svg';
import favoriteFull from '../../img/favorite_full.svg';
import { getAgeString } from '../../utils/animalInfo';
import { useSelector, useDispatch } from 'react-redux';
import { removeFavourite } from '../../store/userSlice';

export default function Favorite(props) {
    const { pets } = props;
    const user = useSelector((state) => state.user);
    const dispatch = useDispatch();

    const handleFavoriteClick = (id) => {
        dispatch(removeFavourite({ userId: user.id, petId: id }))
            .unwrap()
            .catch((error) => {
                console.error('Error removing from favourites:', error);
            });
    };

    return (
        <div className={style.containerFavorite}>
            <div className={style.containerHeader}>
                <h2>Избранное</h2>
                <button className={style.buttonTrash}>
                    <span>Очистить</span>
                    <img src={trash} alt="Корзина" />
                </button>
            </div>
            <div className={style.containerPets}>
                {pets.map((pet) => (
                    <div key={pet.id} className={style.pet}>
                        <img src={pet.mainImageSrc} alt="Картинка животного" className={style.img} />
                        <div className={style.containerInfoPet}>
                            <div className={style.infoPet}>
                                <span className={style.name}>{pet.name}</span>
                                <span>{pet.breed}, {pet.sex === 'female' ? 'Ж' : 'М'}, {getAgeString(pet.age)}</span>
                            </div>
                            <div className={style.buttons}>
                                <Link to={`/animal/${pet.id}`} className={style.buttonMore}>Смотреть</Link>
                                <button
                                    className={style.buttonFavorite}
                                    onClick={() => handleFavoriteClick(pet.id)}
                                >
                                    <img src={favoriteFull} alt="Избранное" />
                                </button>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};
