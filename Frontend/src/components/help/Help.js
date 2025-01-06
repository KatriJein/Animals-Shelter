import style from "./Help.module.css";
import copy from "../../img/copy.svg";
import { Helmet } from "react-helmet-async";

function Help() {

    async function writeClipboardText(text) {
        try {
            await navigator.clipboard.writeText(text);
        } catch (error) {
            console.error(error.message);
        }
    }

    return (
        <>
            <Helmet><title>Помощь приюту</title></Helmet>
            <div className={style.mainContainer}>
                <div className={style.container}>
                    <div className={style.containerInfo}>
                        <h1>Помощь приюту</h1>
                        <p>Мы в приюте ежедневно трудимся, чтобы дать нашим подопечным шанс на счастливую жизнь. Но без вашей поддержки нам не справиться. Здесь вы найдете информацию о том, как помочь приюту — каждый ваш шаг важен.
                        </p>
                    </div>

                    <div className={style.containerInfo}>
                        <h2>Почему нужна ваша помощь?</h2>
                        <p>Собаки, попавшие в приют, часто пережили трудные времена: голод, болезни и одиночество. Мы стараемся обеспечить их всем необходимым, но ресурсы приюта ограничены. Только благодаря добрым людям и волонтерам наши питомцы получают еду, медицинскую помощь и любовь.
                        </p>
                    </div>

                    <div className={style.containerInfo}>
                        <h2>Как вы можете помочь?</h2>
                        <div className={style.containerHelp}>
                            <div className={style.containerVol}>
                                <h3>Станьте волонтером</h3>
                                <div>
                                    <p>Волонтеры — это сердце нашего приюта! Вы можете: </p>
                                    <ul>
                                        <li key='1'>Гулять с собаками и обучать их командам.</li>
                                        <li key='2'>Помогать с уборкой, ремонтом вольеров и другими бытовыми задачами.</li>
                                        <li key='3'>Участвовать в мероприятиях по поиску хозяев для наших питомцев. </li>
                                    </ul>
                                    <p>Если у вас есть желание и время, мы будем рады видеть вас в нашей команде.  </p>
                                </div>
                            </div>

                            <div className={style.containerVol}>
                                <h3>Пожертвуйте товары</h3>
                                <div>
                                    <p>Наши питомцы всегда нуждаются в:</p>
                                    <ol>
                                        <li key='11'>Корме (сухом и влажном).</li>
                                        <li key='12'>Лекарствах (глистогонные средства, препараты для лечения кожи и суставов).</li>
                                        <li key='13'>Поводках, ошейниках и намордниках.</li>
                                        <li key='14'>Одеялах, полотенцах и лежанках.</li>
                                        <li key='15'>Моющих средствах и хозяйственном инвентаре.</li>
                                    </ol>
                                </div>
                            </div>

                            <div className={style.containerVol}>
                                <h3>Финансовая помощь</h3>
                                <p>Любая сумма важна. Ваши пожертвования идут на питание, лечение, стерилизацию и другие нужды собак.</p>
                            </div>

                            <div className={style.containerVol}>
                                <h3>Помогите найти хозяев</h3>
                                <p>Расскажите о нас в социальных сетях. Поделитесь историями наших питомцев и помогите им обрести дом!</p>
                            </div>

                            <div className={style.containerVol}>
                                <h3>Другие способы помочь</h3>
                                <div>
                                    <ul>
                                        <li key='1'>Организуйте сбор средств или товаров среди друзей и коллег.</li>
                                        <li key='2'>Помогите с доставкой кормов или ветеринарных препаратов.</li>
                                        <li key='3'>Предложите свои профессиональные услуги (дизайн, фото, ремонт и т.д.).</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <div className={style.donation}>
                    <h1 className={style.donationTitle}>Сделайте пожертвование</h1>
                    <p>Мы очень ценим вашу финансовую поддержку. <br />Вы можете перевести деньги на:</p>
                    <div className={style.donationContainer}>
                        <div className={style.containerRequisites}>
                            <div className={style.containerRequisite}>
                                <span>Карту</span>
                                <button onClick={() => writeClipboardText('200255555555555')}>
                                    <span>200 2555 5555 5555</span>
                                    <img src={copy} alt="Копировать" />
                                </button>
                            </div>

                            <div className={style.containerRequisite}>
                                <span>Реквизиты счета</span>
                                <button onClick={() => writeClipboardText('40703810900560008967')}>
                                    <span>40703810900560008967</span>
                                    <img src={copy} alt="Копировать" />
                                </button>
                            </div>

                            <div className={style.containerRequisite}>
                                <span>СПБ по номеру телефона (Т-Банк)</span>
                                <button onClick={() => writeClipboardText('+71234567890')}>
                                    <span>+7 (123) 456-78-90</span>
                                    <img src={copy} alt="Копировать" />
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Help;