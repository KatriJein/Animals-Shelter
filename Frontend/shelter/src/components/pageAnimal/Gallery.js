import animal from "../../img/animal.png";
import style from "./Gallery.module.css";

export default function Gallery() {
    return (
        <div className={style.mainContainer}>
            <img src={animal} alt="Приют" className={style.mainImg}/>
            <div className={style.containerImgs}>
                <img src={animal} alt="Приют" className={style.img} />
                <img src={animal} alt="Приют" className={style.img} />
                <img src={animal} alt="Приют" className={style.img} />
                <img src={animal} alt="Приют" className={style.img} />
            </div>
        </div>
    )
}