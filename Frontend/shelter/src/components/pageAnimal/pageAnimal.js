import style from "./pageAnimal.module.css";
import favorite from "../../img/favorite.svg";
import Gallery from "./Gallery";
import Info from "./Info";
import { getAnimalInfo, getHealthConditionsWithGender } from "../../utils/animalInfo";
import HealthCondition from "./HealthCondition";

export default function PageAnimal(props) {
    const { pet } = props;
    const { mainImageSrc, imagesSrc, name, description } = pet;

    const information = getAnimalInfo(pet);
    const healthConditions = getHealthConditionsWithGender(pet.healthConditions, pet.sex)

    return (
        <section className={style.mainContainer}>
            <div className={style.containerInfo}>
                <Gallery mainImageSrc={mainImageSrc} imagesSrc={imagesSrc} />
                <div className={style.containerDescription}>
                    <h2 className={style.h2}>{name}</h2>
                    <div className={style.containerButtons}>
                        <button className={style.buttonShelter}>Приютить</button>
                        <button className={style.buttonFavorite}><img src={favorite} alt="Избранное" className={style.favorite} /></button>
                    </div>
                    <p className={style.p}>{description} мимммммммммммммммм ммммммммммммм ммммммммммммммм мммммммм мммммммммм ммммммм м lorem ipsum dolor sit amet consectetur adipisicing elit. Possimus, quidem.</p>
                    
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