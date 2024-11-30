import style from "./pageAnimal.module.css";
import favorite from "../../img/favorite.svg";
import favoriteFull from "../../img/favorite_full.svg";
import Gallery from "./Gallery";
import Info from "./Info";
import { getAnimalInfo, getHealthConditionsWithGender } from "../../utils/animalInfo";
import HealthCondition from "./HealthCondition";
import { isPetIdInArray } from "../../utils/utils";
import { useSelector, useDispatch } from "react-redux";
import { addFavouritePet, deleteFavouritePet } from "../../store/userSlice";
import { useNavigate } from 'react-router-dom';

export default function PageAnimal(props) {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const { pet } = props;
    const { mainImageSrc, imagesSrc, name, description, id } = pet;

    const information = getAnimalInfo(pet);
    const healthConditions = getHealthConditionsWithGender(pet.healthConditions, pet.sex);
    const favouritesPets = useSelector((state) => state.user.favourites);
    const isFavourite = isPetIdInArray(favouritesPets, pet.id);
    const isAuthenticated = useSelector((state) => state.user.isAuthenticated);
    const user = useSelector((state) => state.user);

    const handleClick = async () => {
        if (isAuthenticated) {
            try {
                const url = `${process.env.REACT_APP_API_URL}/users/${user.id}/${isFavourite ? 'unfavourite' : 'favourite'}/${id}`;
                const response = await fetch(url, {
                    method: 'PATCH',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                });
                
                if (response.ok) {
                    if (isFavourite) {
                        dispatch(deleteFavouritePet(pet.id));
                        console.log('Pet removed from favourites:', pet.name);
                    } else {
                        dispatch(addFavouritePet(pet));
                        console.log('Pet added to favourites:', pet.name);
                    }
                } else {
                    console.error('Error updating favourites:', await response.text());
                }
            } catch (error) {
                console.error('Error during fetch:', error);
            }
        } else {
            navigate('/login');
        }
    };

    return (
        <section className={style.mainContainer}>
            <div className={style.containerInfo}>
                <Gallery mainImageSrc={mainImageSrc} imagesSrc={imagesSrc} />
                <div className={style.containerDescription}>
                    <h2 className={style.h2}>{name}</h2>
                    <div className={style.containerButtons}>
                        <button className={style.buttonShelter}>Приютить</button>
                        <button className={style.buttonFavorite} onClick={handleClick}>{isFavourite ? <img src={favoriteFull} alt="Избранное" className={style.favorite} /> : <img src={favorite} alt="Избранное" className={style.favorite} />}</button>
                    </div>
                    <p className={style.p}>{description} Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. </p>
                    
                    <div>
                        <div className={style.containerList}>
                            {information.map((info) => (
                                <Info key={info.heading} heading={info.heading} text={info.text} />
                            ))}

                        </div>
                        <div className={style.containerConditions}>
                            {healthConditions.map((condition) => (
                                <HealthCondition key={condition} text={condition} />
                            ))}
                        </div>
                    </div>

                </div>
            </div>
        </section>
    )
}