import style from './ButtonBack.module.css';
import arrow from '../img/arrow_back.svg';

function ButtonBack(props) {
    const { onClick } = props;
    return (
        <button className={style.container} onClick={onClick}>
            <img src={arrow} alt="back" />
            <span>Назад</span>
        </button>
    )
}

export default ButtonBack;