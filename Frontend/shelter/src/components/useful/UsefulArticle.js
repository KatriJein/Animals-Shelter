import style from "./UsefulPage.module.css"

export default function UsefulArticle(props) {
    const { heading, text } = props;
    return (
        <div className={style.containerArticle}>
            <h3 className={style.h3}>{heading}</h3>
            <p className={style.p}>{text}</p>
        </div>
    )
}