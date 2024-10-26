import style from "./MainInfo.module.css";
import shelter from "../../img/shelter.png";
import Info from "./Info";
import Button from "../button/Button";

export default function MainInfo() {
    return (
        <section className={style.container}>
            <img src={shelter} alt="Приют" />
            <div className={style.containerInfo}>
                <h1 className={style.h1}>Название приюта</h1>
                <p className={style.p}>Lorem ipsum dolor sit amet consectetur adipisicing elit. Possimus, quidem. Lorem ipsum dolor sit amet consectetur adipisicing elit. Possimus, quidem. Lorem ipsum dolor sit amet consectetur adipisicing elit. Possimus, quidem. Lorem ipsum dolor sit amet consectetur adipisicing elit. Possimus, quidem.</p>
                <div>
                    <h2 className={style.h2}>Приют в цифрах</h2>
                    <div className={style.containerNumbers}>
                        <Info  number="2011" text="год основания" />
                        <Info className={style.li} number="42" text="собак" />
                        <Info className={style.li} number="23" text="кошек" />
                        <Info className={style.li} number="192" text="пристроенных питомцев" />
                    </div>
                </div>
                <div className={style.containerButtons}>
                    <Button text={"Приютить питомца"} styleProps={style.button1} />
                    <Button text={"Помочь нам"} styleProps={style.button2} />
                </div>
            </div>
        </section>
    );
}