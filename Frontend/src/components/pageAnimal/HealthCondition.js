import style from "./HealthCondition.module.css";
import check from "../../img/check_mark.svg";

export default function HealthCondition(props) {
    const { text } = props;

    return (
        <div className={style.container}>
            <img src={check} alt="check" className={style.check} />
            <span className={style.span}>{text}</span>
        </div>
    )
}