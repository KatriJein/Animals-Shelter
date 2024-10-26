import style from "./Button.module.css";

export default function Button(props) {
    const { text, styleProps } = props;

    return (
        <button className={`${style.button} ${styleProps}`}>{text}</button>
    );
}