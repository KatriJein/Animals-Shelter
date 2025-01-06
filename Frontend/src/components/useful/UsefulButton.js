import style from "./UsefulPage.module.css"

export default function UsefulButton(props) {
    const { text, icon, color, onClick } = props;
    return (
        <button className={style.categories} onClick={onClick}>
            <img src={icon} alt={text} className={style.img} />
            <span style={{ color: color }}>{text}</span>
        </button>
    )
}