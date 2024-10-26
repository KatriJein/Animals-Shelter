import style from "./pageAnimal.module.css";
import favorite from "../../img/favorite.svg";
import Gallery from "./Gallery";
import Info from "./Info";

const Information = [{ heading: "Информация", text: "" }, { heading: "Порода", text: "Корги" }, { heading: "Возраст", text: "2 года" }, { heading: "Размер", text: "средний" }];

export default function PageAnimal() {
    return (
        <section className={style.mainContainer}>
            <div className={style.containerInfo}>
                <Gallery />
                <div>
                    <h2 className={style.h2}>Слипнотик</h2>
                    <p className={style.p}>Слипнотик попал в наш приют в возрасте 6 месяцев. Его нашли холодной зимой в коробке на вокзале добрые люди. Он очень ласковый мальчик, озорной и умный.</p>
                    <div className={style.containerList}>
                        {Information.map((info) => (
                            <Info key={info.heading} heading={info.heading} text={info.text} />
                        ))}
                    </div>
                </div>
            </div>
            <div className={style.containerButtons}>
                <button className={style.buttonShelter}>Приютить</button>
                <button className={style.buttonFavorite}><img src={favorite} alt="Избранное" className={style.favorite}/></button>
            </div>

        </section>
    )
}