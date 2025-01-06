import style from "./pageAnimal.module.css";
import favorite from "../../img/favorite.svg";
import favoriteFull from "../../img/favorite_full.svg";
import Gallery from "./Gallery";
import Info from "./Info";
import { getAnimalInfo, getHealthConditionsWithGender } from "../../utils/animalInfo";
import HealthCondition from "./HealthCondition";
import { isPetIdInArray } from "../../utils/utils";
import { useSelector, useDispatch } from "react-redux";
import { addFavourite, removeFavourite } from "../../store/userSlice";
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

    const handleClickPet = () => {
        let url = '';
        if (isAuthenticated) {
            url = `https://docs.google.com/forms/d/e/1FAIpQLSfmSos_MUMeIdAaOTB40RF32G8XPhT2pfBSyDDvmMp1nRgqMA/viewform?usp=pp_url&entry.2005620554=${user.userInfo.surname}+${user.userInfo.name}&entry.1166974658=${user.userInfo.phone}&entry.1045781291=${user.userInfo.email}&entry.839337160=${name}`;
        } else {
            url = `https://docs.google.com/forms/d/e/1FAIpQLSfmSos_MUMeIdAaOTB40RF32G8XPhT2pfBSyDDvmMp1nRgqMA/viewform?usp=pp_url&entry.839337160=${name}`;
        }
        window.open(url, '_blank');
    };

    return (
        <section className={style.mainContainer}>
            <div className={style.containerInfo}>
                <Gallery mainImageSrc={mainImageSrc} imagesSrc={imagesSrc} />
                <div className={style.containerDescription}>
                    <h2 className={style.h2}>{name}</h2>
                    <div className={style.containerButtons}>
                        <button className={style.buttonShelter} onClick={handleClickPet}>Приютить</button>
                        <button className={style.buttonFavorite} onClick={handleClick}>{isFavourite ? <img src={favoriteFull} alt="Избранное" className={style.favorite} /> : <img src={favorite} alt="Избранное" className={style.favorite} />}</button>
                    </div>
                    <p className={style.p}>{description}</p>

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