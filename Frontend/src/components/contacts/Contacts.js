import map from "../../img/map.png";
import style from "./Contacts.module.css";
import vk from "../../img/vk.svg";
import twitter from "../../img/twitter.svg";
import instagram from "../../img/instagram.svg";
import facebook from "../../img/facebook.svg";


export default function Contacts() {
    return (
        <div className={style.mainContainer}>
            <img src={map} alt="карта" className={style.map} />
            <div className={style.container}>
                <h2 className={style.h2}>Контакты</h2>
                <div className={style.containerInfo}>
                    <div className={style.containerContact}>
                        <p className={style.p}>Телефон и почта:</p>
                        <div className={style.containerPhone}>
                            <p className={style.p}><a href="tel:+71234567890" className={`${style.a} ${style.phone}`}>+ 7 (123) 456-78-90</a>Прием звонков 10:00 – 18:00</p>
                            <a href="mailto:lapochka@gmail.com" className={style.a}>lapochka@gmail.com</a>
                        </div>
                    </div>
                    <div className={style.containerContact}>
                        <p className={style.p}>Адрес:</p>
                        <p className={`${style.p} ${style.address}`}>г. Екатеринбург, ул. Академика Шварца, д. 14</p>
                    </div>
                    <div className={style.containerContact}>
                        <p className={style.p}>Мы в соцсетях:</p>
                        <ul className={style.ul}>
                            <li><img src={vk} alt="vk" /></li>
                            <li><img src={instagram} alt="instagram" /></li>
                            <li><img src={twitter} alt="twitter" /></li>
                            <li><img src={facebook} alt="facebook" /></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    )
}