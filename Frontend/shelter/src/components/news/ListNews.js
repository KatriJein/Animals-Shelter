import style from "./ListNews.module.css"

export default function ListNews(props) {
    const { news } = props;

    return (
        <ul className={style.ul}>
            {news.map((item) => (
                <li key={item.id} className={style.containerItem}>
                    <img src={item.mainImageSrc} alt={item.title} className={style.img} />
                    <div className={style.containerNew}>
                        <p>Новые поступления</p>
                        <h3>item.title</h3>
                        <p>item.description</p>
                        <span>item.lastUpdatedAt</span>
                    </div>
                </li>
            ))}
        </ul>
    )
}