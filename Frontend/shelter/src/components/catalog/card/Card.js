import { Link, useNavigate } from 'react-router-dom';
import style from "./Card.module.css";
import favorite from "../../../img/favorite.svg";
import favoriteFull from "../../../img/favorite_full.svg";
import { FilterOptions } from "../../../filterOptions";
import { getAgeString } from '../../../utils/animalInfo';
import { useSelector, useDispatch } from 'react-redux';

export default function Card(props) {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { pet, isAuthenticated } = props;
    const { id, name, breed, age, sex } = pet;
    const sexStr = FilterOptions['sex']['options'][sex].charAt(0).toLowerCase() + FilterOptions['sex']['options'][sex].slice(1);

    async function handleClick() {
        if (isAuthenticated) {
            try {
                const user = useSelector((state) => state.user.userInfo);
                const response = await fetch(`${process.env.REACT_APP_API_URL}/users/${user.id}/favourite/${id}`, {
                    method: 'PATCH',
                    headers: {
                        'Content-Type': 'application/json',
                    }
                });
                if (response.ok) {
                    
                } 
            } catch (error) {
                console.log(error);
            }

        } else {
            navigate('/login');
        }
    }

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
                    <button className={style.buttonFavorite}>
                        <img src={favorite} alt="избранное" className={style.favorite} />
                    </button>
                </div>
            </div>
        </div>
    );
}
