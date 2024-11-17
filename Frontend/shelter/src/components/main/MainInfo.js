import style from "./MainInfo.module.css";
import shelter from "../../img/shelter.png";
import Info from "./Info";
import Button from "../button/Button";
import { Link } from "react-router-dom";

export default function MainInfo() {
    return (
        <section className={style.container}>
            <img src={shelter} alt="Приют" className={style.imgShelter} />
            <div className={style.containerInfo}>
                <div className={style.containerText}>
                    <h1 className={style.h1}>Приют «Лапочка»</h1>
                    <p className={style.p}>Приют для бездомных животных "Лапочка" - это место, где каждое животное получает любовь, заботу и безопасность. Мы предоставляем временный приют для животных, оказавшихся бездомными, и стремимся найти им надежные и любящие семьи для дальнейшего ухода и заботы. Наша команда состоит из опытных волонтёров и ветеринаров, которые готовы помочь каждому животному на пути к новой жизни.
                        <br />Добро пожаловать в приют "Лапочка", где каждая лапка покрыта заботой и нежностью!</p>
                </div>
                <div className={style.containerNumbersInfo}>
                    <h2 className={style.h2}>Приют в цифрах<span> *</span></h2>
                    <div className={style.containerNumbers}>
                        <Info number="2011" text="год основания" />
                        <Info className={style.li} number="42" text="собак" />
                        <Info className={style.li} number="23" text="кошек" />
                        <Info className={style.li} number="192" text="пристроенных питомцев" />
                    </div>
                </div>
                <div className={style.containerButtons}>
                    <Link to="/catalog" className={`${style.button} ${style.button1}`}>Приютить питомца</Link>
                    <Link to="/help" className={`${style.button} ${style.button2}`}>Помочь нам</Link>
                </div>
            </div>
        </section>
    );
}