import style from "./MainInfo.module.css";

export default function Info(props) {
    const { number, text } = props;

    return (
        <div className={style.li}>
            <p className={style.numberLi}>{number}</p>
            <p className={style.textLi}>{text}</p>
        </div>
    );

}