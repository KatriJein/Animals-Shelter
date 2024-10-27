import style from "./UsefulPage.module.css"

export default function UsefulButton(props) {
    const { text, isSelected, onClick } = props;
    return (
        <button className={`${style.button} ${isSelected ? style.selected : ''}`} onClick={onClick}>
            {text}
        </button>
    )
}