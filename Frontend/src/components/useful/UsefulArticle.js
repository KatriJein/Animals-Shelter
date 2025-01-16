import style from "./UsefulPage.module.css";
import point from '../../img/point.svg';
import plus from '../../img/plus.svg';
import arrowTop from '../../img/arrow_top.svg';
import { useState } from "react";

export default function UsefulArticle(props) {
    const [isOpen, setOpen] = useState(false);
    const { heading, text } = props;
    return (
        <div className={style.containerArticle}>
            <div className={style.containerHeader} onClick={() => setOpen(prev => !prev)}>
                <div className={style.containerPoint}>
                    <img src={point} className={style.point} alt="point" />
                    <p className={style.p}>{heading}</p>
                </div>
                <img src={isOpen ? arrowTop : plus} className={style.plus} alt="plus" />
            </div>
            {isOpen && <div className={style.containerText}><p dangerouslySetInnerHTML={{ __html: text }}></p></div>}
        </div>
    )
}