import style from './ContactsQuestion.module.css';
import phoneIcon from '../../img/phone.svg';
import mailIcon from '../../img/email.svg';

function ContactsQuestion() {

    return (
        <div className={style.mainContainer}>
            <div className={style.container}>
                <h2>Не нашли ответ на свой вопрос?</h2>
                <p className={style.p}>Свяжитесь с нами любым удобным Вам способом.</p>
            </div>
            <div className={style.containerContacts}>
                <a className={style.contact} href="tel:+71234567890">
                    <img src={phoneIcon} alt='phone icon' />
                    <div >
                        <a className={`${style.p} ${style.phone}`}>+7 (123) 456-78-90</a>
                        <p className={style.p}>Так получить помощь быстрее.</p>
                    </div>
                </a>
                <a className={style.contact} href="mailto:lapochka@gmail.com">
                    <img src={mailIcon} alt='mail icon' />
                    <div>
                        <a className={`${style.p} ${style.email}`}>lapochka@gmail.com</a>
                        <p className={style.p}>Мы всегда готовы помочь.</p>
                    </div>
                </a>
            </div>
        </div>)

}

export default ContactsQuestion;