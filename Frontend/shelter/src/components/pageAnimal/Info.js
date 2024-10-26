import style from "./Info.module.css";

export default function Info(props) {
    const { heading, text } = props;

    return (
        <div className={style.container}>
            <h3 className={style.h3}>{heading}:</h3>
            <p className={style.p}>{text}</p>
        </div>
    )
}